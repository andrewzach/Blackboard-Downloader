using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackboardDownloader
{
    // Class containing all data on a Blackboard Module
    // Each module has a main content BbContentDirectory where all files and folders can be found
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

////////// ### ATTRIBUTES ###
        // Module name
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        // Url of Module homepage
        public Uri Url
        {
            get { return url; }
            set { url = value; }
        }
        // True if main content directory has been created, false otherwise
        public bool Initialized
        {
            get { return initialized; }
        }
        // Main content directory
        public BbContentDirectory Content
        {
            get { return content; }
            set { content = value; }
        }

        // Creates the main content directory. Url is the address of the main directory on Blackboard.
        public void InitContentDirectory(Uri url)
        {
            content = new BbContentDirectory(name + " Content", url, null);
            initialized = true;
        }

////////// ### SERIALIZE METHODS ### 
        // The below are required for this object to be serialized and saved to a file
        // Return true if other object is a BbModule with the same Url
        public override bool Equals(object other)
        {
            return Equals(other as BbModule);
        }

        // Return true if other BbModule has the same Url
        public bool Equals(BbModule other)
        {
            if (other == null)
            {
                return false;
            }
            return this.url.Equals(other.Url);
        }
        // Returns hashcode of the module's Url
        public override int GetHashCode()
        {
            return url.GetHashCode();
        }
    }
}
