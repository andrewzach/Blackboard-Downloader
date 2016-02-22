using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace BlackboardDownloader
{
    public class Scraper
    {
        // Environment variables - specific to DIT's Webcourses.
        // Change these for other Blackboard sites
        public static string PORTAL = "https://dit-bb.blackboard.com";
        public static string MODID = "_25_1";

        private WebClientEx http;
        private string outputDirectory;
        private BbData webData;
        private bool initialized;
        private string cookieHeader;
        private ScraperProgressReporter downloadProgress;
        private ScraperProgressReporter populateProgress;
        private Logger log;

        public Scraper()
        {
            webData = new BbData();
            log = new Logger("##### Blackboard Downloader Starting #####", "BlackboardDownloader-log.txt");
            outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\BBDL-Output\\";
            downloadProgress = new ScraperProgressReporter();
            populateProgress = new ScraperProgressReporter();
            initialized = false;
        }

////////// ### ATTRIBUTES ###
        // A logger that writes diagnostic information to a .txt file when requested
        public Logger Log
        {
            get { return log; }
        }

        // Communicates download progress and status messages to the UI
        public ScraperProgressReporter DownloadProgress
        {
            get { return downloadProgress; }
        }

        // Communicates content population progress and status messages to the UI
        public ScraperProgressReporter PopulateProgress
        {
            get { return populateProgress; }
        }

        // Return true if Scraper has been initalized
        // True after successful login and creation of WebClientEx with appropriate cookies
        public bool Initialized
        {
            get { return initialized; }
        }

        // Contains all Blackboard modules, folders, and files found by Scraper 
        public BbData WebData
        {
            get { return webData; }
        }

        // Windows directory where downloaded files will be saved
        public string OutputDirectory
        {
            get { return outputDirectory; }
            set
            {
                try
                {
                    // If user enters nothing, don't change the directory (Console UI)
                    if (value == "")
                    {
                        value = outputDirectory;
                    }
                    else if (!value.EndsWith("\\"))
                    {
                        value = value + "\\";   // add \ if directory doesn't end with one
                    }
                    // Creates the folder in Windows if it doesn't already exist
                    Directory.CreateDirectory(value);
                    outputDirectory = value;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Invalid input, could not create directory.");
                }
            } 
        }

        // Returns a list of all module names. Used in Console UI
        public List<string> GetModuleNames()
        {
            return webData.GetModuleNames();
        }

        // Returns the BbModule with the given name (Console UI)
        public BbModule GetModuleByName(string name)
        {
            return webData.GetModuleByName(name);
        }

////////// ### LOGIN AND SETUP ###
        // Logs into Blackboard with supplied user information
        // Retrieves session cookie after login and initializes the WebClientEx with this cookie
        public bool Login(string username, string password)
        {
            cookieHeader = GetLoginCookieHeader(username, password);
            if (cookieHeader == null)
            {
                return false;
            }
            else
            { 
                InitWebClient(cookieHeader);
                return true; 
            }
        }

        // Instantiates http web client with cookie header from login
        private void InitWebClient(string cookieHeader)
        {
            CookieContainer cookieJar = new CookieContainer();
            cookieJar.SetCookies(new Uri(PORTAL + "/webapps/login/"), cookieHeader);
            http = new WebClientEx(cookieJar);
            initialized = true;
        }

        
        // Log in to webcourses with username and password, returns the Set-cookie header string
        // Returns null if login not successful
        private string GetLoginCookieHeader(string username, string password)
        {
            string formUrl = PORTAL +"/webapps/login/"; 
            // Format the parameter portion of the url to post
            string formParams = string.Format("user_id={0}&password={1}&login=Login&action=login&newloc=", username, password);
            string cookieHeader;
            // Create WebRequest to POST login form
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(formUrl);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            // Writes the form parameters to the request using an ASCII stream
            // Ensures the request is properly formatted
            byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            req.ContentLength = bytes.Length;
            using (System.IO.Stream os = req.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }

            // Get the response from the web server and check headers for successful login cookie
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                cookieHeader = resp.Headers["Set-cookie"];
                if (resp.ContentLength == -1)    // Login invalid
                {
                    cookieHeader = null;
                }
            }
            return cookieHeader;
        }

////////// ### POPULATING CONTENT ###
        // Clears all existing data and begins populating content for all modules
        public void PopulateAllData()
        {
            webData.ClearAll();
            PopulateModules();  // scrapes list of modules available
            populateProgress.TotalWork = webData.Modules.Count;
            foreach (BbModule m in webData.Modules)
            {
                PopulateModuleContent(m);
                populateProgress.IncWorkCounter();
                populateProgress.ReportObject(m); // Pass module to GUI for updating the treeview
            }
        }

        // Create BbModule objects for each module found and add them to webData
        // Does not populate content within the module
        public void PopulateModules()
        {
            NameValueCollection reqParams = new NameValueCollection();
            reqParams.Add("action", "refreshAjaxModule");
            reqParams.Add("modId", MODID);
            reqParams.Add("tabId", "_1_1");
            reqParams.Add("tab_tab_group_id", "_1_1");
            byte[] pageSourceBytes = http.UploadValues(PORTAL + "/webapps/portal/execute/tabs/tabAction", "POST", reqParams);
            string pageSource = Encoding.UTF8.GetString(pageSourceBytes);
            List<HtmlNode> moduleLinks = HTMLParser.GetModuleLinks(pageSource);
            foreach (HtmlNode link in moduleLinks)  
            {
                // for each module link found, create and add a new module
                Uri moduleURL = new Uri(new Uri(PORTAL), link.Attributes["href"].Value);
                BbModule newModule = new BbModule(link.InnerHtml, moduleURL);
                if (!webData.Modules.Contains(newModule))
                {
                    webData.AddModule(newModule);
                }
            }
        }

        // Searches for content within module m and adding it.
        public void PopulateModuleContent(BbModule m)
        {
            populateProgress.ReportStatus("Populating content for " + m.Name);
            try
            {
                if (!m.Initialized)
                {
                    CreateMainContentDirectory(m);
                }
                PopulateContentDirectory(m.Content);
            }
            catch(Exception e)
            {
                populateProgress.ReportStatus("Error populating content for module: " + m.Name);
                populateProgress.ReportError("Error populating content for module: " + m.Name);
                log.Write("Content population error for: " + m.Name);
                log.WriteException(e);
                log.Write(e.StackTrace);
            }
        }

        // Used recursively to populate all subfolders of a module
        public void PopulateContentDirectory(BbContentDirectory folder)
        {
            string pageSource = http.DownloadString(folder.Url.AbsoluteUri);
            List<HtmlNode> contentLinks = HTMLParser.GetContentLinks(pageSource);
            if (contentLinks == null) return;
            foreach (HtmlNode link in contentLinks)
            {
                //Console.WriteLine("Adding " + folder.Name + ": " + link.InnerText);
                populateProgress.AppendStatus(".");
                Uri linkURL = new Uri(folder.Url, link.Attributes["href"].Value);
                if (HTMLParser.IsSubFolder(link))   // content is a subfolder
                {
                    BbContentDirectory subFolder = new BbContentDirectory(link.InnerText, linkURL, folder);
                    if (!folder.SubFolders.Contains(subFolder))
                    {
                        folder.AddSubFolder(subFolder);
                    }
                    PopulateContentDirectory(subFolder);
                }
                else if (HTMLParser.IsLearningUnit(link)) //content is a learning unit
                {
                    BbContentDirectory subFolder = new BbContentDirectory(link.InnerText, linkURL, folder);
                    if (!folder.SubFolders.Contains(subFolder))
                    {
                        folder.AddSubFolder(subFolder);
                    }
                    PopulateLearningUnit(subFolder);
                }
                else        // content is a file
                {
                    string linkType = HTMLParser.GetLinkType(linkURL);
                    BbContentItem newFile = new BbContentItem(link.InnerText, linkURL, folder, linkType);
                    if (!folder.Files.Contains(newFile))
                    {
                        folder.AddFile(newFile);
                    }
                }
            }
        }

        // Populates content in a learning unit, which is like a folder but with a tree-like navigation
        // and content is generally displayed in iframes to the right. 
        public void PopulateLearningUnit(BbContentDirectory folder)
        {
            string pageSource = http.DownloadString(folder.Url.AbsoluteUri);
            // Get the link to the next content item in a learning unit by following the next arrow link
            // until there are no more left. 
            Uri nextURL = folder.Url;
            HtmlNode nextLink;
            while (nextURL != null)
            {
                populateProgress.AppendStatus(".");
                string contentSource = http.DownloadString(nextURL);
                List<HtmlNode> contentLinks = HTMLParser.GetLearningUnitContent(contentSource);
                // for each content link found, add a file. Usually only one
                foreach (HtmlNode link in contentLinks)
                {
                    Uri contentURL = new Uri(folder.Url, link.Attributes["href"].Value);
                    string linkType = HTMLParser.GetLinkType(contentURL);
                    string linkName = HTMLParser.GetLinkText(link);
                    if (linkName == "DefaultText") { linkName = HTMLParser.GetPageTitle(contentSource); }
                    folder.AddFile(new BbContentItem(linkName, contentURL, folder, linkType));
                }
                if (contentLinks.Count == 0) // if no content links found, look for content source in iFrame
                {
                    string iFrameLink = HTMLParser.GetLearningUnitIFrame(contentSource);
                    if (iFrameLink != null)  // if iframe found
                    {
                        Uri contentURL = new Uri(folder.Url, iFrameLink);
                        string linkType = HTMLParser.GetLinkType(contentURL);
                        string linkName = HTMLParser.GetPageTitle(contentSource);
                        folder.AddFile(new BbContentItem(linkName, contentURL, folder, linkType));
                    }
                }
                nextLink = HTMLParser.GetNextLearningUnitContent(contentSource);
                if (nextLink == null) { nextURL = null; }
                else { nextURL = new Uri(folder.Url, nextLink.Attributes["href"].Value); }
            }
        }

        // Finds and adds the main content directory to module m. 
        public void CreateMainContentDirectory(BbModule m)
        {
            string pageSource = http.DownloadString(m.Url.AbsoluteUri);
            HtmlNode mainContentLink = HTMLParser.GetMainContentLink(pageSource);
            Uri linkURL = new Uri(new Uri(PORTAL), mainContentLink.Attributes["href"].Value);
            m.InitContentDirectory(linkURL);
        }


////////// ### DOWNLOADING CONTENT ###

        //Used in ConsoleUI
        public void DownloadModuleFiles(string moduleName)
        {
            BbModule m = webData.GetModuleByName(moduleName);
            DownloadFolder(m.Content, outputDirectory + BbUtils.CleanDirectory(m.Name) + "\\");
        }

        // Downloads all files in a module
        public void DownloadModuleFiles(BbModule m)
        {
            DownloadFolder(m.Content, outputDirectory + BbUtils.CleanDirectory(m.Name) + "\\");
        }
        // Downloads all files in folders. Used recursively for each subfolder found.
        public void DownloadFolder(BbContentDirectory folder, string directory)
        {
            foreach(BbContentItem file in folder.Files)
            {
                DownloadFile(file, directory);
            }
            foreach(BbContentDirectory subFolder in folder.SubFolders)
            {
                DownloadFolder(subFolder, directory + BbUtils.CleanDirectory(subFolder.Name) + "\\");   //Add subfolder name to directory
            }
        }

        // Downloads a BbContentItem file and saves it to directory.
        public void DownloadFile(BbContentItem file, string directory)
        {
            string shortDir = directory.Substring(outputDirectory.Length, directory.Length - outputDirectory.Length);
            try
            {
                if (file.LinkType == "onedrive")
                {
                    file.Url = OneDriveURL(file.Url);
                }
                if (file.LinkType == "dropbox")
                {
                    file.Url = DropboxURL(file.Url);
                }
                else if (file.LinkType == "website")
                {
                    log.Write("Linking to website " + file.Url.AbsoluteUri);
                    downloadProgress.ReportStatus("Creating shortcut: " + shortDir + file.Name + file.Url.AbsoluteUri);
                    CreateUrlShortcut(file, directory);
                    return;
                }
                else if  (file.LinkType == "email")
                {
                    return; // Don't try to download e-mail
                }
                downloadProgress.ReportStatus("Downloading file (" + file.LinkType + "): " + shortDir + file.Name);
                Directory.CreateDirectory(directory); //Create directory if it doesn't exist already
                DetectFileName(file);
                http.DownloadFile(file.Url.AbsoluteUri, directory + file.Filename);
            }
            catch (WebException e)
            {
                log.WriteException(e);
                log.Write("ERROR: Cannot download file " + file);
                log.Write(shortDir);
                downloadProgress.ReportError("ERROR: Cannot download file " + file);
            }
            downloadProgress.IncWorkCounter();
        }

        public void DownloadFile(BbContentItem file)
        {
            DownloadFile(file, outputDirectory);
        }

        public void CreateUrlShortcut(BbContentItem file, string directory)
        {
            Directory.CreateDirectory(directory);
            using (StreamWriter w = new StreamWriter(directory + "\\" + file.Filename + ".url"))
            {
                w.WriteLine("[InternetShortcut]");
                w.WriteLine("URL=" + file.Url.AbsoluteUri);
            }
        }
        // Returns a URL to direct download a OneDrive file
        public Uri OneDriveURL(Uri link)
        {
            Uri oneDriveURL;
            string urlString;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            request.AllowAutoRedirect = false;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.Headers["Location"] != null)
                {
                    urlString = response.Headers["Location"];
                }
                else
                {
                    urlString = link.AbsoluteUri;
                }
                //TODO: Check for OneDrive folder
                StringBuilder newURL = new StringBuilder(urlString);
                newURL.Replace("redir?", "download?");    // replace redir with download to get direct download link
                newURL.Replace("embed?", "download?");
                oneDriveURL = new Uri(newURL.ToString());
            }
            return oneDriveURL;
        }

        // Returns a URL to direct download a OneDrive file
        public Uri DropboxURL(Uri link)
        {
            return new Uri(link.AbsoluteUri.Replace("www.dropbox.com", "dl.dropboxusercontent.com"));
        }

        // Determines filename 
        public void DetectFileName(BbContentItem file)
        {
            // if direct link to file or dropbox link, get filename from path
            if (file.LinkType == "directlink" || file.LinkType == "dropbox")  
            {
                string filename = Path.GetFileName(file.Url.AbsoluteUri);
                file.Filename = filename;
            }
            else
            {
                try
                {
                    // Sends a request to file URL and inspects Location header of response for filename 
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(file.Url);
                    request.AllowAutoRedirect = false;
                    request.Headers.Add("Cookie", cookieHeader);
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        Uri fileURL = new Uri(response.Headers["Location"]);
                        string filename = Path.GetFileName(fileURL.LocalPath);
                        file.Filename = filename;
                    }
                }
                catch (System.ArgumentNullException e)
                {
                    log.Write("ERROR: Could not detect filename for " + file);
                    log.WriteException(e);
                    downloadProgress.ReportError("ERROR: Could not detect filename for " + file.Name + " - No location header");
                }
                catch (WebException e)
                {
                    log.Write("ERROR: Could not detect filename for " + file);
                    log.WriteException(e);
                    downloadProgress.ReportError("ERROR: Could not detect filename for " + file.Name + " - WebException");
                }
                catch (NotSupportedException e)
                {
                    log.Write("ERROR: Could not detect filename for " + file);
                    log.WriteException(e);
                    downloadProgress.ReportError("ERROR: Could not detect filename for " + file.Name + " - URL format issue");
                }
            }
        }

        // Serializes all data (webData) and writes it to a savedata.bin file. 
        public void SaveData()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\savedata.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, webData);
            stream.Close();
        }

        // Loads serialized data from the savedata.bin file into webData. 
        // Returns true if successfully loaded, false otherwise
        public bool LoadData()
        {
            bool success;
            IFormatter formatter = new BinaryFormatter();
            Stream stream;
            try
            {
                stream = new FileStream(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\savedata.bin", FileMode.Open, FileAccess.Read, FileShare.None);
                webData = (BbData)formatter.Deserialize(stream);
                success = true;
                stream.Close();
            }
            catch (FileNotFoundException)
            {
                success = false;
            }
            catch (SerializationException)
            {
                success = false;
            }
            return success;
        }
    }

    // A class used by the Scraper to report progress and status messages to the User Interface
    // Communicates to UI through the BackgroundWorker that is passed when BeginJob is called
    public class ScraperProgressReporter
    {
        private BackgroundWorker worker;
        private string currentStatus;
        private bool processing;     
        private List<string> statusMessages;
        private List<string> errorMessages;

        // Progress
        private int totalWork;
        private int workCounter;

        public ScraperProgressReporter()
        {
            processing = false;
            statusMessages = new List<string>();
            errorMessages = new List<string>();
        }

////////// ## ATTRIBUTES ##
        // Current status message - displayed in bottom of GUI
        public string CurrentStatus
        {
            get { return currentStatus; }
        }

        // True if a job is currently reporting progress
        public bool Processing
        {
            get { return processing; }
        }

        // A list of all past status messages for this job
        public List<string> StatusMessages
        {
            get { return statusMessages; }
        }

        // A list of all past error messages for this job
        public List<string> ErrorMessages
        {
            get { return errorMessages; }
        }

        // Total number of tasks to be performed
        public int TotalWork
        {
            get { return totalWork; }
            set { totalWork = value; }
        }
        
        // Current task number
        public int WorkCounter
        {
            get { return workCounter; }
        }

        // Current percent complete for job
        public int CurrentPercentage
        {
            get { return (workCounter * 100) / totalWork; }
        }

////////// ## METHODS ##
        // Called when a new job is starting. Resets all necessary attributes in preparation.
        public void BeginJob(BackgroundWorker worker)
        {
            processing = true;
            workCounter = 0;
            statusMessages.Clear();
            errorMessages.Clear();
            this.worker = worker;
        }

        // Called when a job is finished. 
        public void EndJob()
        {
            this.worker = null;
            processing = false;
        }

        // Reports a status message to the UI. 
        public void ReportStatus(string newStatus)
        {
            currentStatus = newStatus;
            statusMessages.Add(newStatus);
            if (processing)
            {
                WorkerUpdate();
            }
        }

        // Increments the current work counter when a task has been completed
        public void IncWorkCounter()
        {
            if (workCounter < totalWork) workCounter++;
            else workCounter = totalWork;
        }

        // Records error messages for later retrieval by the UI
        public void ReportError(string errorMessage)
        {
            errorMessages.Add(errorMessage);
        }

        // Appends a string to the current status message
        // Used to provide updates when populating modules by adding periods.......
        public void AppendStatus(string appendText)
        {
            currentStatus += appendText;
            if (processing)
            {
                WorkerUpdate();
            }
        }

        // Reports an object to the UI.
        // Mainly used to send BbModules to the UI when populating content so they can be displayed
        public void ReportObject(object reportItem)
        {
            worker.ReportProgress(CurrentPercentage, reportItem);
        }

        // Calls the BackgroundWorker's ReportProgress message with current percent and status.
        private void WorkerUpdate()
        {
            worker.ReportProgress(CurrentPercentage, currentStatus);
        }
    }
}
