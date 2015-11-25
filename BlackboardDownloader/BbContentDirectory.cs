using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    [Serializable]
    public class BbContentDirectory
    {
        private List<BbContentDirectory> subFolders;
        private List<BbContentItem> files;
        private BbContentDirectory folder;
        private Uri url;
        private string name;

        public BbContentDirectory(string name, Uri url, BbContentDirectory folder)
        {
            this.url = url;
            this.name = name;
            this.folder = folder;
            subFolders = new List<BbContentDirectory>();
            files = new List<BbContentItem>();
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

        public List<BbContentItem> Files
        {
            get { return files; }
        }

        public List<BbContentDirectory> SubFolders
        {
            get { return subFolders; }
        }

        public BbContentDirectory Folder
        {
            get { return folder; }
            set { folder = value; }
        }

        public void AddFile(BbContentItem f)
        {
            files.Add(f);
        }

        public void AddSubFolder(BbContentDirectory s)
        {
            subFolders.Add(s);
        }

        public override bool Equals(object other)
        {
            return Equals(other as BbContentDirectory);
        }

        public bool Equals(BbContentDirectory other)
        {
            if (other == null)
            {
                return false;
            }
            return this.url.Equals(other.Url);
        }

    }
}
