namespace BlackboardDownloader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.commandsMenuItem = new System.Windows.Forms.MenuItem();
            this.loginMenuItem = new System.Windows.Forms.MenuItem();
            this.outputMenuItem = new System.Windows.Forms.MenuItem();
            this.refreshMenuItem = new System.Windows.Forms.MenuItem();
            this.viewLogMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.rightTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.downloadButton = new System.Windows.Forms.Button();
            this.infoGroupBox = new System.Windows.Forms.GroupBox();
            this.informationTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.infoText3 = new System.Windows.Forms.Label();
            this.infoText2 = new System.Windows.Forms.Label();
            this.infoLabel4 = new System.Windows.Forms.Label();
            this.infoLabel3 = new System.Windows.Forms.Label();
            this.infoLabel2 = new System.Windows.Forms.Label();
            this.infoLabel1 = new System.Windows.Forms.Label();
            this.infoText1 = new System.Windows.Forms.Label();
            this.infoTextLink = new System.Windows.Forms.LinkLabel();
            this.contentTree = new System.Windows.Forms.TreeView();
            this.yourModulesLabel = new System.Windows.Forms.Label();
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.horizontalLineLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.rightTableLayoutPanel.SuspendLayout();
            this.infoGroupBox.SuspendLayout();
            this.informationTableLayout.SuspendLayout();
            this.mainTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.commandsMenuItem,
            this.menuItem1});
            // 
            // commandsMenuItem
            // 
            this.commandsMenuItem.Index = 0;
            this.commandsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.loginMenuItem,
            this.outputMenuItem,
            this.refreshMenuItem,
            this.viewLogMenuItem});
            this.commandsMenuItem.Text = "Commands";
            // 
            // loginMenuItem
            // 
            this.loginMenuItem.Index = 0;
            this.loginMenuItem.Text = "Login";
            this.loginMenuItem.Click += new System.EventHandler(this.loginMenuItem_Click);
            // 
            // outputMenuItem
            // 
            this.outputMenuItem.Index = 1;
            this.outputMenuItem.Text = "Output Directory...";
            this.outputMenuItem.Click += new System.EventHandler(this.outputMenuItem_Click);
            // 
            // refreshMenuItem
            // 
            this.refreshMenuItem.Index = 2;
            this.refreshMenuItem.Text = "Refresh Content";
            this.refreshMenuItem.Click += new System.EventHandler(this.refreshMenuItem_Click);
            // 
            // viewLogMenuItem
            // 
            this.viewLogMenuItem.Index = 3;
            this.viewLogMenuItem.Text = "View Log";
            this.viewLogMenuItem.Click += new System.EventHandler(this.viewLogMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.aboutMenuItem});
            this.menuItem1.Text = "Help";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 0;
            this.aboutMenuItem.Text = "About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // rightTableLayoutPanel
            // 
            this.rightTableLayoutPanel.ColumnCount = 1;
            this.rightTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightTableLayoutPanel.Controls.Add(this.downloadButton, 0, 0);
            this.rightTableLayoutPanel.Controls.Add(this.infoGroupBox, 0, 1);
            this.rightTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightTableLayoutPanel.Location = new System.Drawing.Point(495, 19);
            this.rightTableLayoutPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rightTableLayoutPanel.Name = "rightTableLayoutPanel";
            this.rightTableLayoutPanel.RowCount = 2;
            this.rightTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.78947F));
            this.rightTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.21053F));
            this.rightTableLayoutPanel.Size = new System.Drawing.Size(239, 326);
            this.rightTableLayoutPanel.TabIndex = 3;
            // 
            // downloadButton
            // 
            this.downloadButton.BackColor = System.Drawing.SystemColors.Control;
            this.downloadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadButton.ForeColor = System.Drawing.Color.DarkCyan;
            this.downloadButton.Location = new System.Drawing.Point(2, 2);
            this.downloadButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(235, 47);
            this.downloadButton.TabIndex = 1;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = false;
            this.downloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // infoGroupBox
            // 
            this.infoGroupBox.Controls.Add(this.informationTableLayout);
            this.infoGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoGroupBox.Location = new System.Drawing.Point(2, 53);
            this.infoGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.infoGroupBox.Name = "infoGroupBox";
            this.infoGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.infoGroupBox.Size = new System.Drawing.Size(235, 271);
            this.infoGroupBox.TabIndex = 2;
            this.infoGroupBox.TabStop = false;
            this.infoGroupBox.Text = "Information";
            // 
            // informationTableLayout
            // 
            this.informationTableLayout.ColumnCount = 1;
            this.informationTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.informationTableLayout.Controls.Add(this.infoText3, 0, 5);
            this.informationTableLayout.Controls.Add(this.infoText2, 0, 3);
            this.informationTableLayout.Controls.Add(this.infoLabel4, 0, 6);
            this.informationTableLayout.Controls.Add(this.infoLabel3, 0, 4);
            this.informationTableLayout.Controls.Add(this.infoLabel2, 0, 2);
            this.informationTableLayout.Controls.Add(this.infoLabel1, 0, 0);
            this.informationTableLayout.Controls.Add(this.infoText1, 0, 1);
            this.informationTableLayout.Controls.Add(this.infoTextLink, 0, 7);
            this.informationTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.informationTableLayout.Location = new System.Drawing.Point(2, 21);
            this.informationTableLayout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.informationTableLayout.Name = "informationTableLayout";
            this.informationTableLayout.RowCount = 8;
            this.informationTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.informationTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.informationTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.informationTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.informationTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.informationTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.informationTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.informationTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.informationTableLayout.Size = new System.Drawing.Size(231, 248);
            this.informationTableLayout.TabIndex = 1;
            // 
            // infoText3
            // 
            this.infoText3.AutoSize = true;
            this.infoText3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoText3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoText3.Location = new System.Drawing.Point(2, 143);
            this.infoText3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoText3.Name = "infoText3";
            this.infoText3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.infoText3.Size = new System.Drawing.Size(227, 33);
            this.infoText3.TabIndex = 9;
            // 
            // infoText2
            // 
            this.infoText2.AutoSize = true;
            this.infoText2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoText2.Location = new System.Drawing.Point(2, 90);
            this.infoText2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoText2.Name = "infoText2";
            this.infoText2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.infoText2.Size = new System.Drawing.Size(227, 33);
            this.infoText2.TabIndex = 8;
            // 
            // infoLabel4
            // 
            this.infoLabel4.AutoSize = true;
            this.infoLabel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.infoLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel4.Location = new System.Drawing.Point(2, 176);
            this.infoLabel4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoLabel4.Name = "infoLabel4";
            this.infoLabel4.Size = new System.Drawing.Size(227, 20);
            this.infoLabel4.TabIndex = 6;
            this.infoLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // infoLabel3
            // 
            this.infoLabel3.AutoSize = true;
            this.infoLabel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.infoLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel3.Location = new System.Drawing.Point(2, 123);
            this.infoLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoLabel3.Name = "infoLabel3";
            this.infoLabel3.Size = new System.Drawing.Size(227, 20);
            this.infoLabel3.TabIndex = 4;
            this.infoLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // infoLabel2
            // 
            this.infoLabel2.AutoSize = true;
            this.infoLabel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.infoLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel2.Location = new System.Drawing.Point(2, 70);
            this.infoLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoLabel2.Name = "infoLabel2";
            this.infoLabel2.Size = new System.Drawing.Size(227, 20);
            this.infoLabel2.TabIndex = 2;
            this.infoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // infoLabel1
            // 
            this.infoLabel1.AutoSize = true;
            this.infoLabel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.infoLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel1.Location = new System.Drawing.Point(2, 0);
            this.infoLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoLabel1.Name = "infoLabel1";
            this.infoLabel1.Size = new System.Drawing.Size(227, 20);
            this.infoLabel1.TabIndex = 0;
            this.infoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // infoText1
            // 
            this.infoText1.AutoSize = true;
            this.infoText1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoText1.Location = new System.Drawing.Point(2, 20);
            this.infoText1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoText1.Name = "infoText1";
            this.infoText1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.infoText1.Size = new System.Drawing.Size(227, 50);
            this.infoText1.TabIndex = 7;
            // 
            // infoTextLink
            // 
            this.infoTextLink.AutoSize = true;
            this.infoTextLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoTextLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoTextLink.Location = new System.Drawing.Point(2, 196);
            this.infoTextLink.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.infoTextLink.Name = "infoTextLink";
            this.infoTextLink.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.infoTextLink.Size = new System.Drawing.Size(227, 52);
            this.infoTextLink.TabIndex = 10;
            this.infoTextLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.infoTextLink_LinkClicked);
            // 
            // contentTree
            // 
            this.contentTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentTree.FullRowSelect = true;
            this.contentTree.ItemHeight = 24;
            this.contentTree.Location = new System.Drawing.Point(2, 19);
            this.contentTree.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.contentTree.Name = "contentTree";
            this.contentTree.ShowLines = false;
            this.contentTree.Size = new System.Drawing.Size(489, 326);
            this.contentTree.TabIndex = 2;
            this.contentTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.contentTree_AfterSelect);
            // 
            // yourModulesLabel
            // 
            this.yourModulesLabel.AutoSize = true;
            this.mainTableLayout.SetColumnSpan(this.yourModulesLabel, 2);
            this.yourModulesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yourModulesLabel.Location = new System.Drawing.Point(2, 1);
            this.yourModulesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.yourModulesLabel.Name = "yourModulesLabel";
            this.yourModulesLabel.Size = new System.Drawing.Size(732, 16);
            this.yourModulesLabel.TabIndex = 1;
            this.yourModulesLabel.Text = "Your Modules";
            this.yourModulesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.AutoSize = true;
            this.mainTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainTableLayout.ColumnCount = 2;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.mainTableLayout.Controls.Add(this.yourModulesLabel, 0, 1);
            this.mainTableLayout.Controls.Add(this.contentTree, 0, 2);
            this.mainTableLayout.Controls.Add(this.rightTableLayoutPanel, 1, 2);
            this.mainTableLayout.Controls.Add(this.statusLabel, 0, 3);
            this.mainTableLayout.Controls.Add(this.horizontalLineLabel, 0, 0);
            this.mainTableLayout.Controls.Add(this.progressBar, 1, 3);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 4;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.mainTableLayout.Size = new System.Drawing.Size(736, 363);
            this.mainTableLayout.TabIndex = 0;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoEllipsis = true;
            this.statusLabel.AutoSize = true;
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Location = new System.Drawing.Point(2, 347);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(489, 16);
            this.statusLabel.TabIndex = 4;
            // 
            // horizontalLineLabel
            // 
            this.horizontalLineLabel.AutoSize = true;
            this.horizontalLineLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainTableLayout.SetColumnSpan(this.horizontalLineLabel, 2);
            this.horizontalLineLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.horizontalLineLabel.Location = new System.Drawing.Point(0, 0);
            this.horizontalLineLabel.Margin = new System.Windows.Forms.Padding(0);
            this.horizontalLineLabel.Name = "horizontalLineLabel";
            this.horizontalLineLabel.Size = new System.Drawing.Size(736, 1);
            this.horizontalLineLabel.TabIndex = 5;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(495, 349);
            this.progressBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(239, 12);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 363);
            this.Controls.Add(this.mainTableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "Blackboard Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.rightTableLayoutPanel.ResumeLayout(false);
            this.infoGroupBox.ResumeLayout(false);
            this.informationTableLayout.ResumeLayout(false);
            this.informationTableLayout.PerformLayout();
            this.mainTableLayout.ResumeLayout(false);
            this.mainTableLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem commandsMenuItem;
        private System.Windows.Forms.MenuItem loginMenuItem;
        private System.Windows.Forms.MenuItem outputMenuItem;
        private System.Windows.Forms.MenuItem refreshMenuItem;
        private System.Windows.Forms.MenuItem viewLogMenuItem;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.Label yourModulesLabel;
        private System.Windows.Forms.TreeView contentTree;
        private System.Windows.Forms.TableLayoutPanel rightTableLayoutPanel;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.GroupBox infoGroupBox;
        private System.Windows.Forms.TableLayoutPanel informationTableLayout;
        private System.Windows.Forms.Label infoText3;
        private System.Windows.Forms.Label infoText2;
        private System.Windows.Forms.Label infoLabel4;
        private System.Windows.Forms.Label infoLabel3;
        private System.Windows.Forms.Label infoLabel2;
        private System.Windows.Forms.Label infoLabel1;
        private System.Windows.Forms.Label infoText1;
        private System.Windows.Forms.LinkLabel infoTextLink;
        private System.Windows.Forms.Label horizontalLineLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}