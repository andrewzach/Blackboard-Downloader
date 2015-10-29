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
        private bool initialized;
        private BbContentDirectory content;

        public BbModule(string name, string url)
        {
            this.name = name;
            this.url = url;
            initialized = false;
        }
        public void InitContentDirectory(string url)
        {
            content = new BbContentDirectory(name + " Content", url);
            initialized = true;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Url {
            get { return url; }
            set { url = value; }
        }
        public bool Initialized
        {
            get { return initialized; }
        }

        public BbContentDirectory Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
