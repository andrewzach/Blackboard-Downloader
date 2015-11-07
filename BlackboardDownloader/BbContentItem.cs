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
        private Uri url;
        private string filename;
        private string location;

        public BbContentItem(string name, Uri url, string location="blackboard")
        {
            this.name = name;
            this.url = url;
            this.location = location;
            filename = BbUtils.CleanFileName(name).Truncate(20);
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

        //not my code. Need a better place to put this function.

    }

    //not my code. Need a better place to put this class.

}
