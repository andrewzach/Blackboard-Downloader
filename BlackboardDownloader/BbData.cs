using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
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
            set { modules = value; }
        }

        public List<string> GetModuleNames()
        {
            List<string> moduleNames = new List<string>();
            foreach (BbModule m in modules)
            {
                moduleNames.Add(m.Name);
            }
            return moduleNames;
        }

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
