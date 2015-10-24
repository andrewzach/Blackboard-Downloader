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
    }
}
