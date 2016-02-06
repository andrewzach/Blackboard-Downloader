using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackboardDownloader
{
    public partial class MainForm : Form
    {
        private Scraper scraper;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(Scraper scraper)
        {
            InitializeComponent();
            this.scraper = scraper;
            Initialize();
        }

        private void Initialize()
        {
            ShowLoginForm();

            //Populate Content
            if (scraper.LoadData())
            {
                statusLabel.Text = "Loading data from previous session...";
            }
            else
            {
                statusLabel.Text = "Populating content data from webcourses. Please wait...";
                scraper.PopulateAllData();
            }
            PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            foreach (BbModule module in scraper.webData.Modules)
            {
                TreeNode moduleNode = new TreeNode(module.Name);
                moduleNode.Tag = module;
                PopulateTreeFolder(moduleNode, module.Content);
                contentTree.Nodes.Add(moduleNode);
            }
        }

        private void PopulateTreeFolder(TreeNode parent, BbContentDirectory folder)
        {
            TreeNode folderNode = new TreeNode(folder.Name);
            folderNode.Tag = folder;
            foreach(BbContentDirectory subFolder in folder.SubFolders)
            {
                PopulateTreeFolder(folderNode, subFolder);
            }
            foreach(BbContentItem file in folder.Files)
            {
                AddTreeFile(folderNode, file);
            }
            parent.Nodes.Add(folderNode);
        }

        private void AddTreeFile(TreeNode parent, BbContentItem file)
        {
            TreeNode fileNode = new TreeNode(file.Name);
            fileNode.Tag = file;
            parent.Nodes.Add(fileNode);
        }

        private void ClearInfoBox()
        {
            infoLabel1.ResetText();
            infoLabel2.ResetText();
            infoLabel3.ResetText();
            infoLabel4.ResetText();
            infoText1.ResetText();
            infoText2.ResetText();
            infoText3.ResetText();
            infoText4.ResetText();
        }

        private void contentTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ClearInfoBox();
            if (contentTree.SelectedNode.Tag.GetType() == typeof(BbContentItem))
            {
                BbContentItem file = contentTree.SelectedNode.Tag as BbContentItem;
                infoLabel1.Text = "Name";
                infoText1.Text = file.Name;
                infoLabel2.Text = "Filename";
                infoText2.Text = file.Filename;
                infoLabel3.Text = "Link Type";
                infoText3.Text = file.LinkType;
                infoLabel4.Text = "URL";
                infoText4.Text = file.Url.AbsoluteUri;
            }
        }

        private void ShowLoginForm()
        {
            statusLabel.Text = "Waiting for login...";
            Application.Run(new LoginForm(scraper));

            // While login has not been successful, keep opening the LoginForm
            while (!scraper.initialized)     
            {
                statusLabel.Text = "Login not successful. Re-opening login form.";
                Application.Run(new LoginForm(scraper));
            }
            statusLabel.ResetText();
        }
    }
}
