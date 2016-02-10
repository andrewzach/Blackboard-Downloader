using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    // Class representing a single file or link on Blackboard
    [Serializable]
    public class BbContentItem
    {
        private string name;
        private Uri url;
        private string filename;
        private string linkType;
        private BbContentDirectory folder;

        public BbContentItem(string name, Uri url, BbContentDirectory folder, string linkType="local")
        {
            this.name = name;
            this.url = url;
            this.linkType = linkType;
            this.folder = folder;
            filename = BbUtils.CleanFileName(name).Truncate(40);
        }

        // -- ATTRIBUTES --
        // Name of the file - the link text that appears on Blackboard
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Url for this file. 
        // Represents the href value that appears on Blackboard. Not necessarily the actual file's url
        // Direct url to the file is only determined in the Scraper class when downloading.
        public Uri Url
        {
            get { return url; }
            set { url = value; }
        }

        // Name of the file to be downloaded, eg. syllabus.docx
        // By default this is equal to Name. Actual file name is only determined when attempting to download.
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        // The LinkType describes where the file resides. Used to determine how to download the file
        // The default is "local", meaning a file directly on the Blackboard server
        // Other options: onedrive, dropbox, googledrive, googledocs, email, directlink, website
        public string LinkType
        {
            get { return linkType; }
            set { linkType = value; }
        }

        // Directory that contains this item
        public BbContentDirectory Folder
        {
            get { return folder; }
            set { folder = value; }
        }

        // Returns a string representation of the filename designed for output in the Console UI.
        public override string ToString()
        {
            return "Name[" + name + "] Filename[" + filename + "] Type[" + linkType + "]" +
                Environment.NewLine + "URL[" + url.AbsoluteUri + "]";
        }

        // -- SERIALIZE METHODS -- 
        // Returns true if other object is a BbContentItem with the same url
        public override bool Equals(object other)
        {
            return Equals(other as BbContentItem);
        }

        // Returns true if other BbContentItem has the same url
        public bool Equals(BbContentItem other)
        { 
            if (other == null)
            {
                return false;
            }
            return this.url.Equals(other.Url);
        }

        // Returns the hashcode for the item's url
        public override int GetHashCode()
        {
            return url.GetHashCode();
        }
    }
}
