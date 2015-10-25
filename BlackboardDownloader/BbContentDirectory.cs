using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    public class BbContentDirectory
    {
        private List<BbContentDirectory> subFolders;
        private List<BbContentItem> files;
        private string url;
        private string name;

        public BbContentDirectory(string name, string url)
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

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public void AddFile(BbContentItem newFile)
        {
            files.Add(newFile);
        }

    }
}
