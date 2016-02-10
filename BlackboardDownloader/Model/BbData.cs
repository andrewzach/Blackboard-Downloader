using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    // Class containing all data found for a user's Blackboard courses.
    // Contains a list of BbModules, each of which has folders and files. 
    [Serializable]
    public class BbData
    {
        private List<BbModule> modules;

        public BbData()
        {
            modules = new List<BbModule>();
        }

        public List<BbModule> Modules
        {
            get { return modules; }
        }

        // Add a new module
        public void AddModule(BbModule m)
        {
            modules.Add(m);
        }

        // Returns a list of module names. Used in Console UI
        public List<string> GetModuleNames()
        {
            List<string> moduleNames = new List<string>();
            foreach (BbModule m in modules)
            {
                moduleNames.Add(m.Name);
            }
            return moduleNames;
        }

        // Clears all data
        public void ClearAll()
        {
            modules = new List<BbModule>();
        }

        // Returns BbModule object matching the name string. Used in Console UI
        public BbModule GetModuleByName(string name)
        {
            foreach (BbModule m in modules)
            {
                if(m.Name == name) { return m; }
            }
            return null;
        }
    }
}
