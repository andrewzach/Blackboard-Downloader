using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BlackboardDownloader
{
    // A utility class used by Scraper to help parse HTML content
    // Most methods will take an HTML page source (in a string) as the parameter 
    // Uses XPath to find the required HtmlNodes.
    // Relies on HtmlAgilityPack to parse Html efficiently. 
    public static class HTMLParser
    {
        // Return's a list of HtmlNodes representing every <a> tag in the html page source.
        public static List<HtmlNode> GetAllLinks(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNodeCollection allLinks = doc.DocumentNode.SelectNodes("//a[@href]");
            return allLinks.ToList();
        }

        // Return's a list of HtmlNodes representing every <a> tag linking to a Module
        // Used to determine all modules the user has access to and populate their content
        public static List<HtmlNode> GetModuleLinks(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNodeCollection allLinks = doc.DocumentNode.SelectNodes("//a[@href]");
            // Module links have "type=Course" in the href tag
            List<HtmlNode> moduleLinks = allLinks.Where(item => item.Attributes["href"].Value.Contains("type=Course")).ToList();
            return moduleLinks;
        }

        // Returns a list of all content links in the page source. Content links could either be folders or files
        // To be used on a BbContentDirectory's page
        public static List<HtmlNode> GetContentLinks(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNodeCollection contentLinks = doc.DocumentNode.SelectNodes("//li[contains(@id, 'contentListItem')]//a[not(contains(@href,'uploadAssignment?'))]");
            if (contentLinks != null) return contentLinks.ToList();
            else return null;
        }

        // Returns a list of all links to Blackboard LearningUnits
        // To be used on a BbContentDirectory's page
        // Learning units are similar to folders/directories, but structured differently
        public static List<HtmlNode> GetLearningUnitLinks(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNodeCollection contentLinks = doc.DocumentNode.SelectNodes("//a[contains(@href,'displayLearningUnit')]");
            if (contentLinks != null) return contentLinks.ToList();
            else return null;
        }

        // Navigates LearningUnits by following the next arrow. 
        // LearningUnits display one contentItem at a time, and must be navigated sequentially to see all items
        // Returns a link to the next LearningUnit content page, or null if no more exist
        public static HtmlNode GetNextLearningUnitContent(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNode nextLink = doc.DocumentNode.SelectSingleNode("//a[img[contains(@src,'arrow_next_li.gif')]]");
            return nextLink;
        }

        // Retrieves all links to actual files or content items on a LearningUnit content page.
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

        // For LearningUnit content pages where no links can be found using GetLearningUnitContent
        // These pages often have only an iFrame that contains content.
        // Returns this iFrame's src attribute (the url of the file being displayed) or null if no Iframes on page
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

        // Returns a link to the main content directory for a module
        // To be used on the page source of a module's homepage (BbModule.Url)
        public static HtmlNode GetMainContentLink(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNode contentLink = doc.DocumentNode.SelectSingleNode("//li[contains(@id, 'paletteItem')]//a[contains(@href,'listContent.jsp')]");
            return contentLink;
        }

        // Return the HTML title of a page
        // Used to set names for content with no link text (LearningUnit iFrames)
        public static string GetPageTitle(string pageSource)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageSource);
            HtmlNode title = doc.DocumentNode.SelectSingleNode("//title");
            return title.InnerText;
        }

        // Return true if link points to a Blackboard folder/directory
        public static bool IsSubFolder(HtmlNode link)
        {
            if (link.Attributes["href"].Value.Contains("listContent.jsp")) { return true; }
            else { return false; }
        }

        // Return true if link points to a Blackboard LearningUnit
        public static bool IsLearningUnit(HtmlNode link)
        {
            if (link.Attributes["href"].Value.Contains("displayLearningUnit?")) { return true; }
            else { return false; }
        }

        // Returns the link type. Takes a file's Url as the parameter.
        // 
        public static string GetLinkType(Uri linkURL)
        {
            string linkType;
            // local = A file contained directly on the Blackboard server
            if (linkURL.AbsoluteUri.StartsWith("https://dit-bb") || linkURL.AbsoluteUri.StartsWith("http://dit-bb"))
            {
                linkType = "local";
            }
            // onedrive = A file contained on Microsoft's One Drive website
            else if (linkURL.AbsoluteUri.StartsWith("http://1drv.ms") || linkURL.AbsoluteUri.Contains("onedrive.live.com"))
            {
                linkType = "onedrive";
            }
            // dropbox = A file on dropbox.com
            else if (linkURL.AbsoluteUri.Contains("dropbox.com"))
            {
                linkType = "dropbox";
            }
            // googledocs = A file on docs.google.com
            else if (linkURL.AbsoluteUri.Contains("docs.google.com"))
            {
                linkType = "googledocs";
            }
            // googledrive = A file on drive.google.com
            else if (linkURL.AbsoluteUri.Contains("drive.google.com"))
            {
                linkType = "googledrive";
            }
            // email = An email link (mailto:)
            else if (linkURL.AbsoluteUri.StartsWith("mailto"))
            {
                linkType = "email";
            }
            // directlink = A direct link to a file (not html page) on an external site
            // Checks for most common formats. Should consider adding more
            else if (linkURL.AbsoluteUri.EndsWith(".pdf") || 
                linkURL.AbsoluteUri.EndsWith(".docx") || 
                linkURL.AbsoluteUri.EndsWith(".pptx"))
            {
                linkType = "directlink";
            }

            // website = link to external webpage. The default if it matches none of the above
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
