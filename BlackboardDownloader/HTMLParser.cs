using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BlackboardDownloader
{
    public static class HTMLParser
    {
        public static List<HtmlNode> GetAllLinks(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNodeCollection allLinks = doc.DocumentNode.SelectNodes("//a[@href]");
            return allLinks.ToList();
        }

        public static List<HtmlNode> GetModuleLinks(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNodeCollection allLinks = doc.DocumentNode.SelectNodes("//a[@href]");
            List<HtmlNode> moduleLinks = allLinks.Where(item => item.Attributes["href"].Value.Contains("type=Course")).ToList();
            return moduleLinks;
        }

        public static List<HtmlNode> GetContentLinks(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNodeCollection contentLinks = doc.DocumentNode.SelectNodes("//li[contains(@id, 'contentListItem')]//a[not(contains(@href,'uploadAssignment?'))]");
            return contentLinks.ToList();
        }

        public static List<HtmlNode> GetLearningUnitLinks(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNodeCollection contentLinks = doc.DocumentNode.SelectNodes("//a[contains(@href,'displayLearningUnit')]");
            return contentLinks.ToList();
        }

        public static HtmlNode GetNextLearningUnitContent(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNode nextLink = doc.DocumentNode.SelectSingleNode("//a[img[contains(@src,'arrow_next_li.gif')]]");
            return nextLink;
        }

        public static List<HtmlNode> GetLearningUnitContent(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNodeCollection contentLinks = doc.DocumentNode.SelectNodes("//div[@class='vtbegenerated']//a[@href]");
            HtmlNodeCollection contentLinks2 = doc.DocumentNode.SelectNodes("//span[@class='fnt3']//a[@href]");
            List<HtmlNode> allLinks = new List<HtmlNode>();
            if (contentLinks != null) { allLinks.AddRange(contentLinks.ToList()); }
            if (contentLinks2 != null) { allLinks.AddRange(contentLinks2.ToList()); }
            return allLinks;
        }

        public static string GetLearningUnitIFrame(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNode iframe = doc.DocumentNode.SelectSingleNode("//div[@class='vtbegenerated']//iframe[@src]");
            if (iframe != null)
            {
                return iframe.Attributes["src"].Value;
            }
            else { return null; }
        }

        public static HtmlNode GetMainContentLink(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNode contentLink = doc.DocumentNode.SelectSingleNode("//li[contains(@id, 'paletteItem')]//a[contains(@href,'listContent.jsp')]");
            return contentLink;
        }

        public static string GetPageTitle(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNode title = doc.DocumentNode.SelectSingleNode("//title");
            return title.InnerText;
        }

        public static bool IsSubFolder(HtmlNode link)
        {
            if (link.Attributes["href"].Value.Contains("listContent.jsp")) { return true; }
            else { return false; }
        }

        public static bool IsLearningUnit(HtmlNode link)
        {
            if (link.Attributes["href"].Value.Contains("displayLearningUnit?")) { return true; }
            else { return false; }
        }

        public static string GetLinkType(Uri linkURL)
        {
            string linkType;
            if (linkURL.AbsoluteUri.StartsWith("https://dit-bb") || linkURL.AbsoluteUri.StartsWith("http://dit-bb"))
            {
                linkType = "local";
            }
            else if (linkURL.AbsoluteUri.StartsWith("http://1drv.ms") || linkURL.AbsoluteUri.Contains("onedrive.live.com"))
            {
                linkType = "onedrive";
            }
            else if (linkURL.AbsoluteUri.Contains("dropbox.com"))
            {
                linkType = "dropbox";
            }
            else if (linkURL.AbsoluteUri.Contains("docs.google.com"))
            {
                linkType = "googledocs";
            }
            else if (linkURL.AbsoluteUri.Contains("drive.google.com"))
            {
                linkType = "googledrive";
            }
            else if (linkURL.AbsoluteUri.StartsWith("mailto"))
            {
                linkType = "email";
            }
            else if (linkURL.AbsoluteUri.EndsWith(".pdf") || 
                linkURL.AbsoluteUri.EndsWith(".docx") || 
                linkURL.AbsoluteUri.EndsWith(".pptx"))
            {
                linkType = "directlink";
            }

            else
            {
                linkType = "website";   //Default to website
            }
            return linkType;
        }

        // Attempts to get inner text of a link. For badly formed HTML, may have to look in next sibling node.
        public static string GetLinkText(HtmlNode link)
        {
            string linkText = link.InnerText;
            if (string.IsNullOrWhiteSpace(linkText))
            {
                linkText = link.NextSibling.InnerText;
                if (string.IsNullOrWhiteSpace(linkText))
                {
                    linkText = "DefaultText";
                }
            }
            return linkText;
        }
    }
}
