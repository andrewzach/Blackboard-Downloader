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

namespace BlackboardDownloader
{
    public class Scraper
    {
        public static string PORTAL = "https://dit-bb.blackboard.com";
        public static string MODID = "_25_1";
        private WebClientEx http;
        private string outputDirectory;
        private BbData webData;
        private bool initialized;
        private string cookieHeader;
        private Logger log;

        public Scraper()
        {
            webData = new BbData();
            log = new Logger("##### Blackboard Downloader Starting #####", "BlackboardDownloader-log.txt");
            outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\BBDL-Output\\";
            initialized = false;
        }

        public string OutputDirectory
        {
            get { return outputDirectory; }
            set
            {
                try
                {
                    if (!value.EndsWith("\\") && value != "")
                    {
                        value = value + "\\";   // add \ if directory doesn't end with one
                    }
                    Directory.CreateDirectory(value);
                    outputDirectory = value;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Invalid input, could not create directory.");
                }
            } 
        }

        public List<string> GetModuleNames()
        {
            return webData.GetModuleNames();
        }

        public BbModule GetModuleByName(string name)
        {
            return webData.GetModuleByName(name);
        }


        // ### LOGIN AND SETUP ###
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

        // instantiate http web client with cookie header from login
        private void InitWebClient(string cookieHeader)
        {
            CookieContainer cookieJar = new CookieContainer();
            cookieJar.SetCookies(new Uri(PORTAL + "/webapps/login/"), cookieHeader);
            http = new WebClientEx(cookieJar);
            initialized = true;
        }

        
        // Log in to webcourses with username and password, returns the Set-cookie header string
        private string GetLoginCookieHeader(string username, string password)
        {
            string formUrl = PORTAL +"/webapps/login/"; 
            string formParams = string.Format("user_id={0}&password={1}&login=Login&action=login&newloc=", username, password);
            string cookieHeader;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(formUrl);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            req.ContentLength = bytes.Length;
            using (System.IO.Stream os = req.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
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

        // ### POPULATING CONTENT ###
        public void PopulateAllData()
        {
            PopulateModules();  // scrapes list of modules available
            foreach(BbModule m in webData.Modules)
            {
                PopulateModuleContent(m);
            }
        }

        // Create BbModule objects for each module found and add them to webData
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
                webData.AddModule(new BbModule(link.InnerHtml, moduleURL));
            }
        }

        // Searches for content within module m and adding it.
        public void PopulateModuleContent(BbModule m)
        {
            Console.Write("\nPopulating content for " + m.Name);
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
                Console.Write(" Error populating content for module.");
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
            foreach (HtmlNode link in contentLinks)
            {
                //Console.WriteLine("Adding " + folder.Name + ": " + link.InnerText);
                Console.Write(".");
                Uri linkURL = new Uri(folder.Url, link.Attributes["href"].Value);
                if (HTMLParser.IsSubFolder(link))   // content is a subfolder
                {
                    BbContentDirectory subFolder = new BbContentDirectory(link.InnerText, linkURL, folder);
                    folder.AddSubFolder(subFolder);
                    PopulateContentDirectory(subFolder);
                }
                else if (HTMLParser.IsLearningUnit(link)) //content is a learning unit
                {
                    BbContentDirectory subFolder = new BbContentDirectory(link.InnerText, linkURL, folder);
                    folder.AddSubFolder(subFolder);
                    PopulateLearningUnit(subFolder);
                }
                else        // content is a file
                {
                    string linkType = HTMLParser.GetLinkType(linkURL);
                    folder.AddFile(new BbContentItem(link.InnerText, linkURL, folder, linkType));
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
                Console.Write(".");
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


    // ### DOWNLOADING CONTENT ###

        // Downloads all files in a module
        public void DownloadModuleFiles(string moduleName)
        {
            BbModule m = webData.GetModuleByName(moduleName);
            Console.WriteLine("Downloading files for module " + m.Name);
            Console.WriteLine("Output directory = " + outputDirectory);
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
        private void DownloadFile(BbContentItem file, string directory)
        {
            string shortDir = directory.Substring(outputDirectory.Length, directory.Length - outputDirectory.Length);
            try
            {
                if (file.LinkType == "onedrive")
                {
                    file.Url = OneDriveURL(file.Url);
                }
                else if (file.LinkType == "website")
                {
                    log.Write("Linking to website " + file.Url.AbsoluteUri);
                    Console.WriteLine("Creating shortcut: " + shortDir + file.Name + "\n\tURL = " + file.Url.AbsoluteUri);
                    CreateUrlShortcut(file, directory);
                    return;
                }
                else if  (file.LinkType == "email")
                {
                    return; // Don't try to download e-mail
                }
                Console.WriteLine("Downloading file (" + file.LinkType + "): " + shortDir + file.Name);
                Directory.CreateDirectory(directory); //Create directory if it doesn't exist already
                DetectFileName(file);
                http.DownloadFile(file.Url.AbsoluteUri, directory + file.Filename);
            }
            catch (WebException e)
            {
                log.WriteException(e);
                log.Write("ERROR: Cannot download file " + file);
                log.Write(shortDir);
                Console.WriteLine("ERROR: Cannot download file " + file);
            }
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

        // Determines filename 
        public void DetectFileName(BbContentItem file)
        {
            if (file.LinkType == "directlink")  // if direct link to file, get filename from path
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
                    Console.WriteLine("ERROR: Could not detect filename for " + file.Name + " - No location header");
                }
                catch (WebException e)
                {
                    log.Write("ERROR: Could not detect filename for " + file);
                    log.WriteException(e);
                    Console.WriteLine("ERROR: Could not detect filename for " + file.Name + " - WebException");
                }
                catch (NotSupportedException e)
                {
                    log.Write("ERROR: Could not detect filename for " + file);
                    log.WriteException(e);
                    Console.WriteLine("ERROR: Could not detect filename for " + file.Name + " - URL format issue");
                }
            }
        }
    }
}
