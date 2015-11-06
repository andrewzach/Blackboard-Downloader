using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    public class BbContentDirectory
    {
        private List<BbContentDirectory> subFolders;
        private List<BbContentItem> files;
        private Uri url;
        private string name;

        public BbContentDirectory(string name, Uri url)
        {
            this.url = url;
            this.name = name;
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

        public void AddFile(BbContentItem f)
        {
            files.Add(f);
        }

        public void AddSubFolder(BbContentDirectory s)
        {
            subFolders.Add(s);
        }

    }
}
