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

namespace BlackboardDownloader
{
    public class Scraper
    {
        public static string PORTAL = "https://dit-bb.blackboard.com";
        public static string MODID = "_25_1";
        private WebClientEx http;
        private string outputDirectory = @"C:\Output\";
        private BbData webData;
        private bool initialized;
        private string cookieHeader;

        public Scraper()
        {
            webData = new BbData();
            initialized = false;
        }

        public string OutputDirectory
        {
            get { return outputDirectory; }
            set { outputDirectory = value; }    //TODO: Test directory is valid by creating folder
        }

        public List<string> GetModuleNames()
        {
            return webData.GetModuleNames();
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
            PopulateModules();
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
                //Console.WriteLine("Adding module " + link.InnerHtml);
                string linkString = PORTAL + link.Attributes["href"].Value;
                linkString = linkString.Replace(" ", string.Empty);     // Some Blackboard Hrefs have spaces. Strip them.
                //string trueLink = RedirectURL(linkString);              // Determine the real URL of the module after redirect
                webData.AddModule(new BbModule(link.InnerHtml, linkString));
            }
        }

        // Searches for content within module m and adding it.
        public void PopulateModuleContent(BbModule m)
        {
            if(!m.Initialized)
            {
                CreateMainContentDirectory(m);
            }
            PopulateContentDirectory(m.Content);
        }

        // Used recursively to populate all subfolders of a module
        public void PopulateContentDirectory(BbContentDirectory folder)
        {
            string pageSource = http.DownloadString(folder.Url);
            List<HtmlNode> contentLinks = HTMLParser.GetContentLinks(pageSource);
            foreach (HtmlNode link in contentLinks)
            {
                Console.WriteLine("Adding " + folder.Name + ": " + link.InnerText);
                string linkString;
                if (link.Attributes["href"].Value.StartsWith("http"))   
                {
                    linkString = link.Attributes["href"].Value;     // if external link, take as given
                }
                else
                {
                    linkString = PORTAL + link.Attributes["href"].Value; // if local link, add http blackboard portal in front
                }
                if (HTMLParser.IsSubFolder(link))   // content is a subfolder
                {
                    BbContentDirectory subFolder = new BbContentDirectory(link.InnerText, linkString);
                    folder.AddSubFolder(subFolder);
                    PopulateContentDirectory(subFolder);
                }
                else        // content is a file
                {
                    folder.AddFile(new BbContentItem(link.InnerText, linkString));
                }
            }
        }

        // Finds and adds the main content directory to module m. 
        public void CreateMainContentDirectory(BbModule m)
        {
            string pageSource = http.DownloadString(m.Url);
            HtmlNode mainContentLink = HTMLParser.GetMainContentLink(pageSource);
            string linkString = PORTAL + mainContentLink.Attributes["href"].Value;
            m.InitContentDirectory(linkString);
        }


    // ### DOWNLOADING CONTENT ###

        // Downloads all files in a module
        public void DownloadModuleFiles(string moduleName)
        {
            BbModule m = webData.GetModuleByName(moduleName);
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
            try
            {
                Console.WriteLine("Downloading file " + directory + file.Name);
                Directory.CreateDirectory(directory); //Create directory if it doesn't exist already
                DetectFileName(file);
                http.DownloadFile(file.Url, directory + file.Filename);
            }
            catch (WebException e)
            {
                Console.WriteLine("ERROR: Cannot download file " + file.Name);
            }
        }

        // Determines filename 
        public void DetectFileName(BbContentItem file)
        {
            try
            {
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
                Console.WriteLine("ERROR: Could not detect filename for " + file.Name + " - No location header");
            }
            catch (WebException e)
            {
                Console.WriteLine("ERROR: Could not detect filename for " + file.Name + " - WebException");
            }
        }
    }
}
