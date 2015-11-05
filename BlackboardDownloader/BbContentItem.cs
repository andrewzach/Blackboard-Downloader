using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    public class BbContentItem
    {
        private string name;
        private string url;
        private string filename;
        private string fileType;

        public BbContentItem(string name, string url)
        {
            this.name = name;
            this.url = url;
            filename = BbUtils.CleanFileName(name).Truncate(20);
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

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        //not my code. Need a better place to put this function.

    }

    //not my code. Need a better place to put this class.

}
