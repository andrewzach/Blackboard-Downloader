using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    [Serializable]
    public class BbModule
    {
        private string name;
        private Uri url;
        private bool initialized;
        private BbContentDirectory content;

        public BbModule(string name, Uri url)
        {
            this.name = name;
            this.url = url;
            initialized = false;
        }
        public void InitContentDirectory(Uri url)
        {
            content = new BbContentDirectory(name + " Content", url, null);
            initialized = true;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Uri Url {
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

        public override bool Equals(object other)
        {
            return Equals(other as BbModule);
        }

        public bool Equals(BbModule other)
        {
            if (other == null)
            {
                return false;
            }
            return this.url.Equals(other.Url);
        }
    }
}
