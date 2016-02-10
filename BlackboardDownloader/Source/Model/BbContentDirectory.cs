using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    // Class representing a folder/directory on Blackboard
    // Contains a list of files and subfolders
    [Serializable]
    public class BbContentDirectory
    {
        private List<BbContentDirectory> subFolders;
        private List<BbContentItem> files;
        private BbContentDirectory folder;
        private Uri url;
        private string name;

        public BbContentDirectory(string name, Uri url, BbContentDirectory parentFolder)
        {
            this.url = url;
            this.name = name;
            this.folder = parentFolder;
            subFolders = new List<BbContentDirectory>();
            files = new List<BbContentItem>();
        }

////////// ### ATTRIBUTES ###
        // Directory name (link text that appears on Blackboard)
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Http address
        public Uri Url
        {
            get { return url; }
            set { url = value; }
        }

        // List of all files in this directory
        public List<BbContentItem> Files
        {
            get { return files; }
        }

        // List of all directories within this one
        public List<BbContentDirectory> SubFolders
        {
            get { return subFolders; }
        }

        // Parent folder that contains this one. Null for the main content directory of a module
        public BbContentDirectory ParentFolder
        {
            get { return folder; }
            set { folder = value; }
        }

        // Add a BbContentItem to the directory
        public void AddFile(BbContentItem f)
        {
            files.Add(f);
        }

        // Add a sub-folder to this directory
        public void AddSubFolder(BbContentDirectory s)
        {
            subFolders.Add(s);
        }

        // Counts all files in folder and all subfolders
        // Used to display information in the GUI
        public int CountAllFiles()
        {
            int fileCount = 0;
            foreach (BbContentDirectory subFolder in SubFolders)
            {
                fileCount += subFolder.CountAllFiles();
            }
            foreach (BbContentItem file in Files)
            {
                fileCount++;
            }
            return fileCount;
        }

////////// ### SERIALIZE METHODS ### 
        // The below are required for this object to be serialized and saved to a file
        // Return true if other object is a BbContentDirectory with the same url
        public override bool Equals(object other)
        {
            return Equals(other as BbContentDirectory);
        }

        // Return true if other BbContentDirectory has the same url
        public bool Equals(BbContentDirectory other)
        {
            if (other == null)
            {
                return false;
            }
            return this.url.Equals(other.Url);
        }
        // Return hashcode of the directory's url
        public override int GetHashCode()
        {
            return url.GetHashCode();
        }
    }
}
