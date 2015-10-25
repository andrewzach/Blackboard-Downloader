using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    public class BbContentItem
    {
        private string name;
        private string url;
        private string fileType;

        public BbContentItem(string name, string url)
        {
            this.name = name;
            this.url = url;
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
    }
}
