using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net;
using HtmlAgilityPack;

namespace BlackboardDownloader
{
    public class Scraper
    {
        public static string PORTAL = "https://dit-bb.blackboard.com";
        public static string MODID = "_25_1";
        private WebClientEx http;
        public BbData bbData;
        private bool initialized;

        public Scraper()
        {
            bbData = new BbData();
            initialized = false;
        }

        public bool Login(string username, string password)
        {
            string cookieHeader = GetLoginCookieHeader(username, password);
            InitWebClient(cookieHeader);
            return true; // TODO: check login success
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
            WebRequest req = WebRequest.Create(formUrl);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            req.ContentLength = bytes.Length;
            using (System.IO.Stream os = req.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
            WebResponse resp = req.GetResponse();
            cookieHeader = resp.Headers["Set-cookie"];
            return cookieHeader;
        }

        public void PopulateAllData()
        {
            PopulateModules();
            foreach(BbModule m in bbData.Modules)
            {
                PopulateModuleContent(m);
            }
        }

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
                bbData.AddModule(new BbModule(link.InnerHtml, linkString));
            }
        }

        public void CreateMainContentDirectory(BbModule m)
        {
            string pageSource = http.DownloadString(m.Url);
            HtmlNode mainContentLink = HTMLParser.GetMainContentLink(pageSource);
            string linkString = PORTAL + mainContentLink.Attributes["href"].Value;
            m.InitContentDirectory(linkString);
        }
        public void PopulateModuleContent(BbModule m)
        {
            if(!m.Initialized)
            {
                CreateMainContentDirectory(m);
            }
            string pageSource = http.DownloadString(m.Content.Url);
            List<HtmlNode> contentLinks = HTMLParser.GetContentLinks(pageSource);
            if(contentLinks != null)
            {
                CreateMainContentDirectory(m);
            }
            foreach (HtmlNode link in contentLinks)
            {
                Console.WriteLine("Adding " + m.Name + ": " + link.InnerText);
                m.Content.AddFile(new BbContentItem(link.InnerText, link.Attributes["href"].Value));
            }
            //Console.WriteLine(pageSource);
        }
        public List<string> GetModuleNames()
        {
            return bbData.GetModuleNames();
        }
    }
}
