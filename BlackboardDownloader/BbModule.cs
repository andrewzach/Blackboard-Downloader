using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    public class BbModule
    {
        private string name;
        private string url;

        public BbModule(string name, string url)
        {
            this.name = name;
            this.url = url;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string URL {
            get { return url; }
            set { url = value; }
        }

    }
}
