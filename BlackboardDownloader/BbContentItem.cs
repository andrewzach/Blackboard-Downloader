using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
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

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Uri Url
        {
            get { return url; }
            set { url = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public string LinkType
        {
            get { return linkType; }
            set { linkType = value; }
        }

        public BbContentDirectory Folder
        {
            get { return folder; }
            set { folder = value; }
        }

        public override string ToString()
        {
            return "Name[" + name + "] Filename[" + filename + "] Type[" + linkType + "]" +
                Environment.NewLine + "URL[" + url.AbsoluteUri + "]";
        }

        public override bool Equals(object other)
        {
            return Equals(other as BbContentItem);
        }

        public bool Equals(BbContentItem other)
        { 
            if (other == null)
            {
                return false;
            }
            return this.url.Equals(other.Url);
        }
    }
}
