﻿using System;
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

        // Counters to show status of download
        private int currentFile;
        private int totalFiles;

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
                foreach (BbContentDirectory subFolder in module.Content.SubFolders)
                {
                    PopulateTreeFolder(moduleNode, subFolder);
                }
                foreach (BbContentItem file in module.Content.Files)
                {
                    AddTreeFile(moduleNode, file);
                }
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

            // File selected
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

            // Folder selected
            else if (contentTree.SelectedNode.Tag.GetType() == typeof(BbContentDirectory))
            {
                BbContentDirectory folder = contentTree.SelectedNode.Tag as BbContentDirectory;
                infoLabel1.Text = "Name";
                infoText1.Text = folder.Name;
                infoLabel2.Text = "Subfolders";
                infoText2.Text = folder.SubFolders.Count.ToString();
                infoLabel3.Text = "Files";
                infoText3.Text = folder.CountAllFiles().ToString();
                infoLabel4.Text = "URL";
                infoText4.Text = folder.Url.AbsoluteUri;
            }

            // Module selected
            else if (contentTree.SelectedNode.Tag.GetType() == typeof(BbModule))
            {
                BbModule module = contentTree.SelectedNode.Tag as BbModule;
                infoLabel1.Text = "Name";
                infoText1.Text = module.Name;
                infoLabel2.Text = "Subfolders";
                infoText2.Text = module.Content.SubFolders.Count.ToString();
                infoLabel3.Text = "Files";
                infoText3.Text = module.Content.CountAllFiles().ToString();
                infoLabel4.Text = "URL";
                infoText4.Text = module.Url.AbsoluteUri;
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (contentTree.SelectedNode == null)
            {
                statusLabel.Text = "Nothing selected to download";
            }
            else if (contentTree.SelectedNode.Tag.GetType() == typeof(BbModule))
            {
                BbModule module = contentTree.SelectedNode.Tag as BbModule;
                StartFileCounter(module.Content);
                DownloadFolder(module.Content, scraper.OutputDirectory);
            }
            // Single folder selected
            else if (contentTree.SelectedNode.Tag.GetType() == typeof(BbContentDirectory))
            {
                BbContentDirectory folder = contentTree.SelectedNode.Tag as BbContentDirectory;
                StartFileCounter(folder);
                DownloadFolder(folder, scraper.OutputDirectory);
            }
            // Single file selected
            else if (contentTree.SelectedNode.Tag.GetType() == typeof(BbContentItem))
            {
                BbContentItem file = contentTree.SelectedNode.Tag as BbContentItem;
                statusLabel.Text = "Downloading file (" + file.LinkType + "): " + file.Name;
                scraper.DownloadFile(file);
            }
        }


        public void DownloadFolder(BbContentDirectory folder, string directory)
        {
            string shortDir = directory.Substring(scraper.outputDirectory.Length, directory.Length - scraper.outputDirectory.Length);
            foreach (BbContentItem file in folder.Files)
            {
                statusLabel.Text = "Downloading file (" + file.LinkType + "): " + shortDir + file.Name;
                UpdateFileCounter();    // Add file count to statusLabel
                statusLabel.Refresh();
                scraper.DownloadFile(file, directory);
            }
            foreach (BbContentDirectory subFolder in folder.SubFolders)
            {
                DownloadFolder(subFolder, directory + BbUtils.CleanDirectory(subFolder.Name) + "\\");   //Add subfolder name to directory
            }
        }

        private void StartFileCounter(BbContentDirectory folder)
        {
            currentFile = 0;
            totalFiles = folder.CountAllFiles();
        }

        private void UpdateFileCounter()
        {
            currentFile++;
            statusLabel.Text += "( " + currentFile + " of " + totalFiles + " )";
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
