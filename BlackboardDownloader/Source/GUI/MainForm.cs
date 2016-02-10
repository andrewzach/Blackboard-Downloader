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
    // The main GUI window for the program
    // Displays a treeview showing all modules, folders, and files.
    // Contains an Information box on the bottom right showing info on selected module/folder/file
    // Has a statusLabel on the bottom to display status messages
    // Main menu contains other commands. 
    public partial class MainForm : Form
    {
        private Scraper scraper;

        public MainForm()
        {
            InitializeComponent();
        }

        // Load Event - called after form is first created
        private void MainForm_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        // Get user to login and initialize the Scraper.
        // Loads previously saved data or populate all content if none available
        // Populates the contentTree
        private void Initialize()
        {
            scraper = new Scraper();

            // Login
            ShowLoginFormDialog();

            //Populate Content
            bool dataLoaded = scraper.LoadData();
            if (dataLoaded)
            {
                statusLabel.Text = "Content loaded from previous session.";
                PopulateTreeView();
            }
            else
            {
                statusLabel.Text = "Populating content data from webcourses. Please wait...";
                PopulateContent();
            }
        }

////////// ### POPULATE CONTENT IN TREEVIEW ###

        // Populates the TreeView with all data in the scraper's BbData (webData)
        private void PopulateTreeView()
        {
            contentTree.Nodes.Clear(); // Clear any existing tree nodes
            foreach (BbModule module in scraper.WebData.Modules)
            {
                PopulateTreeModule(module);   
            }
        }

        private void PopulateTreeModule(BbModule module)
        {
            // If module has not been populated with content, don't attempt to create tree.
            if (!module.Initialized) return;

            // Create a root node for the module
            TreeNode moduleNode = new TreeNode(module.Name);
            moduleNode.Tag = module;
            Font moduleFont = new Font(new FontFamily("Segoe UI Semibold"), 16, FontStyle.Regular, GraphicsUnit.Pixel);
            moduleNode.NodeFont = moduleFont;
            moduleNode.BackColor = Color.AliceBlue;

            // Populate module subfolders
            foreach (BbContentDirectory subFolder in module.Content.SubFolders)
            {
                PopulateTreeFolder(moduleNode, subFolder);
            }
            // Populate files in module's main content folder
            foreach (BbContentItem file in module.Content.Files)
            {
                AddTreeFile(moduleNode, file);
            }
            // Add module nodes to tree
            contentTree.Nodes.Add(moduleNode);
            moduleNode.Text = moduleNode.Text; // Forces re-draw to fix text truncating issue
        }

        // Adds given folder to the content TreeView. Adds folder's node to parent node.
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

        // Adds given BbContentItem file to the content treeview.
        private void AddTreeFile(TreeNode parent, BbContentItem file)
        {
            TreeNode fileNode = new TreeNode(file.Name);
            fileNode.Tag = file;
            parent.Nodes.Add(fileNode);
        }

        // Clears all labels in the Information Box
        private void ClearInfoBox()
        {
            infoLabel1.ResetText();
            infoLabel2.ResetText();
            infoLabel3.ResetText();
            infoLabel4.ResetText();
            infoText1.ResetText();
            infoText2.ResetText();
            infoText3.ResetText();
            infoTextLink.ResetText();
        }

        // Event Handler for the content treeview. Changes information displayed 
        private void contentTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ClearInfoBox();

            // Change what's displayed in the Information Box depending on type of node selected
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
                infoTextLink.Text = file.Url.AbsoluteUri;
            }
            // Folder selected
            else if (contentTree.SelectedNode.Tag.GetType() == typeof(BbContentDirectory))
            {
                BbContentDirectory folder = contentTree.SelectedNode.Tag as BbContentDirectory;
                infoLabel1.Text = "Name";
                infoText1.Text = folder.Name;
                infoLabel2.Text = "Files";
                infoText2.Text = folder.CountAllFiles().ToString();
                infoLabel3.Text = "Subfolders";
                infoText3.Text = folder.SubFolders.Count.ToString();
                infoLabel4.Text = "URL";
                infoTextLink.Text = folder.Url.AbsoluteUri;
            }

            // Module selected
            else if (contentTree.SelectedNode.Tag.GetType() == typeof(BbModule))
            {
                BbModule module = contentTree.SelectedNode.Tag as BbModule;
                infoLabel1.Text = "Name";
                infoText1.Text = module.Name;
                infoLabel2.Text = "Files";
                infoText2.Text = module.Content.CountAllFiles().ToString();
                infoLabel3.Text = "Subfolders";
                infoText3.Text = module.Content.SubFolders.Count.ToString();
                infoLabel4.Text = "URL";
                infoTextLink.Text = module.Url.AbsoluteUri;
            }
        }

        // Opens the LoginForm dialog prompting the user to log in. 
        // Continues opening the dialog until login is successful or user opts to "Quit"
        private void ShowLoginFormDialog()
        {
            statusLabel.Text = "Waiting for login...";
            LoginForm loginForm = new LoginForm(scraper);
            DialogResult result = loginForm.ShowDialog();
            // While login has not been successful, keep opening the LoginForm
            while (!scraper.Initialized)     
            {
                // If user chose to quit the program
                if (result == DialogResult.Abort || result == DialogResult.Cancel)
                {
                    this.Close();
                    return;
                }
                statusLabel.Text = "Login not successful. Re-opening login form.";
                result = loginForm.ShowDialog();
            }
            statusLabel.ResetText();
        }

////////// ### MAIN MENU - COMMANDS MENU ITEMS ###

        // Login
        // Allows user to re-login with a different username/password. Re-initializes the program.
        private void loginMenuItem_Click(object sender, EventArgs e)
        {
            Initialize();
        }

        // View Log
        // Opens the porgram's log file for user to browse. 
        private void viewLogMenuItem_Click(object sender, EventArgs e)
        {
            string logFilePath = scraper.Log.FilePath;
            System.Diagnostics.Process.Start(logFilePath);
        }

        // Output Directory
        // Allows user to change the output directory. Opens a FileBrowserDialog for user to select new directory.
        private void outputMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = scraper.OutputDirectory; // Start in current output directory
            DialogResult result = fbd.ShowDialog();

            // If user successfully selected a new path, change the OutputDirectory.
            if (result == DialogResult.OK)  
            {
                scraper.OutputDirectory = fbd.SelectedPath;
            }
        }

        // About
        // Opens the About dialog, displaying information on the program
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutDialog = new AboutBox();
            aboutDialog.ShowDialog();
        }

////////// ### DOWNLOAD FILES ###

        // EventHandler for Download button
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            TreeNode selected = contentTree.SelectedNode;

            // If nothing is selected to download, display status message and do nothing.
            if (selected == null)
            {
                statusLabel.Text = "Nothing selected to download";
            }
            else
            {
                // Creates a BackgroundWorker to download the files on a separate CPU thread
                // Allows the GUI to still run and respond to input while large downloads are occuring.
                BackgroundWorker bgw = new BackgroundWorker();
                bgw.WorkerSupportsCancellation = true;  // TODO: implement cancel button
                bgw.WorkerReportsProgress = true;

                // Add event handlers
                bgw.DoWork += new DoWorkEventHandler(DownloadBW_DoWork);
                bgw.ProgressChanged += new ProgressChangedEventHandler(DownloadBW_ProgressChanged);
                bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DownloadBW_RunWorkerCompleted);

                // Start the BackgroundWorker downloading the selected module/folder/files.
                bgw.RunWorkerAsync(selected);
            }
        }

        // DoWork event handler for the file download BackgroundWorker. 
        // The selected TreeNode (either a module, folder, or file) is passed in the DoWorkEventArgs e
        private void DownloadBW_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            TreeNode selectedNode = e.Argument as TreeNode;
            scraper.DownloadProgress.BeginJob(worker);

            // -- DETERMINE TYPE OF NODE SELECTED --
            // Module selected
            if (selectedNode.Tag.GetType() == typeof(BbModule))
            {
                BbModule module = selectedNode.Tag as BbModule;
                scraper.DownloadProgress.TotalWork = module.Content.CountAllFiles();
                scraper.DownloadModuleFiles(module);
            }
            // Single folder selected
            else if (selectedNode.Tag.GetType() == typeof(BbContentDirectory))
            {
                BbContentDirectory folder = selectedNode.Tag as BbContentDirectory;
                scraper.DownloadProgress.TotalWork = folder.CountAllFiles();
                scraper.DownloadFolder(folder, scraper.OutputDirectory);
            }
            // Single file selected
            else if (selectedNode.Tag.GetType() == typeof(BbContentItem))
            {
                BbContentItem file = selectedNode.Tag as BbContentItem;
                scraper.DownloadProgress.TotalWork = 1;
                scraper.DownloadFile(file);
            }
        }

        // Reports download progress to the GUI from the download BackgroundWorker.
        // A string to display on the statusLabel is passed in the ProgressChangedEventArgs UserState attribute.  
        private void DownloadBW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusLabel.Text = e.UserState as string;
            progressBar.Value = e.ProgressPercentage;
        }

        // Event handler for the download BackgroundWorker that runs after all downloads are completed
        private void DownloadBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                statusLabel.Text = "Download cancelled.";
            }
            else if (!(e.Error == null))
            {
                statusLabel.Text = "Error downloading files.";
            }
            else
            {
                progressBar.Value = 100;
                statusLabel.Text = "Done downloading " + scraper.DownloadProgress.TotalWork + " files.";
                if (scraper.DownloadProgress.ErrorMessages.Count > 0)
                {
                    statusLabel.Text += " ( " + scraper.DownloadProgress.ErrorMessages.Count + " errors )";
                    statusLabel.Text += " View the log for more information.";
                }
            }
            scraper.DownloadProgress.EndJob();
        }

////////// ### POPULATE CONTENT ###

        // Searches for content on Blackboard and populates modules/folders/files
        // Happens asynchronously using a BackgroundWorker.
        private void PopulateContent()
        {
            // Creates a BackgroundWorker to search for blackboard content on a separate CPU thread
            // Allows the GUI to still run and respond to input while content is populating.
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;  // TODO: implement cancel button
            bgw.WorkerReportsProgress = true;

            // Add event handlers
            bgw.DoWork += new DoWorkEventHandler(PopulateContentBW_DoWork);
            bgw.ProgressChanged += new ProgressChangedEventHandler(PopulateContentBW_ProgressChanged);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PopulateContentBW_RunWorkerCompleted);

            bgw.RunWorkerAsync();
        }

        // DoWork event handler for PopulateContent BackgroundWorker.
        private void PopulateContentBW_DoWork(object sender, DoWorkEventArgs e)
        {
            scraper.PopulateProgress.BeginJob(sender as BackgroundWorker);
            scraper.PopulateAllData();
        }

        // ProgressChanged event handler for PopulateContent BackgroundWorker
        // A string to be displayed in the statusLabel is passed in as the event args UserState
        private void PopulateContentBW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;   // Update progress bar

            if (e.UserState.GetType() == typeof(BbModule))
            {
                PopulateTreeModule(e.UserState as BbModule);
            }
            else if (e.UserState.GetType() == typeof(string))
            {
                statusLabel.Text = e.UserState as string;
            }
            
        }

        // RunWorkerCompleted event handler for PopulateContent Backgroundworker
        // Updates the status label and populates the content treeview if work successful.
        private void PopulateContentBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                statusLabel.Text = "Content search cancelled.";
            }
            else if (!(e.Error == null))
            {
                statusLabel.Text = "Error searching for content on Blackboard.";
            }
            else
            {
                progressBar.Value = 100;
                statusLabel.Text = "Done searching for content.  " + scraper.WebData.Modules.Count + " modules found.";
                if (scraper.PopulateProgress.ErrorMessages.Count > 0)
                {
                    statusLabel.Text += " ( " + scraper.PopulateProgress.ErrorMessages.Count + " errors )";
                    statusLabel.Text += " View the log for more information.";
                }
                PopulateTreeView();
            }
            scraper.SaveData();
            scraper.PopulateProgress.EndJob();
        }

        // Saves all content to a serialized file on program exit.
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Only save data if the program isn't actively populating content
            // This prevents errors that occur when saving incomplete data
            if (!scraper.PopulateProgress.Processing && scraper.Initialized)
            {
                scraper.SaveData();
            }
        }

        // Refresh Content
        // Re-populate all content from Blackboard. This can take several minutes.
        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            contentTree.Nodes.Clear(); // Clear all existing nodes
            PopulateContent();
        }

        // URL info box - opens hyperlinks in default browser when clicked
        private void infoTextLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.infoTextLink.LinkVisited = true;
            System.Diagnostics.Process.Start(infoTextLink.Text);
        }
    }
}
