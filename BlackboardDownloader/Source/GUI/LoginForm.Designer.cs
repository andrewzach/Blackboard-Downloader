namespace BlackboardDownloader
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.bottomTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.quitButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.warningLabel = new System.Windows.Forms.Label();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.loginGroupBox = new System.Windows.Forms.GroupBox();
            this.loginGroupTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameText = new System.Windows.Forms.TextBox();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.loginIcon = new System.Windows.Forms.PictureBox();
            this.mainTableLayout.SuspendLayout();
            this.bottomTableLayout.SuspendLayout();
            this.loginGroupBox.SuspendLayout();
            this.loginGroupTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 1;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Controls.Add(this.bottomTableLayout, 0, 2);
            this.mainTableLayout.Controls.Add(this.welcomeLabel, 0, 0);
            this.mainTableLayout.Controls.Add(this.loginGroupBox, 0, 1);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 3;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayout.Size = new System.Drawing.Size(382, 183);
            this.mainTableLayout.TabIndex = 0;
            // 
            // bottomTableLayout
            // 
            this.bottomTableLayout.ColumnCount = 3;
            this.bottomTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.80883F));
            this.bottomTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.93628F));
            this.bottomTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.2549F));
            this.bottomTableLayout.Controls.Add(this.quitButton, 0, 0);
            this.bottomTableLayout.Controls.Add(this.submitButton, 0, 0);
            this.bottomTableLayout.Controls.Add(this.warningLabel, 0, 0);
            this.bottomTableLayout.Location = new System.Drawing.Point(3, 146);
            this.bottomTableLayout.Name = "bottomTableLayout";
            this.bottomTableLayout.RowCount = 1;
            this.bottomTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottomTableLayout.Size = new System.Drawing.Size(376, 34);
            this.bottomTableLayout.TabIndex = 1;
            // 
            // quitButton
            // 
            this.quitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quitButton.Location = new System.Drawing.Point(301, 3);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(72, 28);
            this.quitButton.TabIndex = 1;
            this.quitButton.Text = "Cancel";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.submitButton.Location = new System.Drawing.Point(227, 3);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(68, 28);
            this.submitButton.TabIndex = 0;
            this.submitButton.Text = "OK";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // warningLabel
            // 
            this.warningLabel.AutoSize = true;
            this.warningLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.warningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.warningLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.warningLabel.Location = new System.Drawing.Point(3, 0);
            this.warningLabel.Name = "warningLabel";
            this.warningLabel.Size = new System.Drawing.Size(218, 34);
            this.warningLabel.TabIndex = 7;
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLabel.Location = new System.Drawing.Point(3, 0);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(302, 17);
            this.welcomeLabel.TabIndex = 10;
            this.welcomeLabel.Text = "Welcome! Please enter your Webcourses login";
            // 
            // loginGroupBox
            // 
            this.loginGroupBox.Controls.Add(this.loginGroupTableLayout);
            this.loginGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginGroupBox.Location = new System.Drawing.Point(8, 28);
            this.loginGroupBox.Margin = new System.Windows.Forms.Padding(8);
            this.loginGroupBox.Name = "loginGroupBox";
            this.loginGroupBox.Size = new System.Drawing.Size(366, 107);
            this.loginGroupBox.TabIndex = 0;
            this.loginGroupBox.TabStop = false;
            this.loginGroupBox.Text = "Login";
            // 
            // loginGroupTableLayout
            // 
            this.loginGroupTableLayout.ColumnCount = 3;
            this.loginGroupTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.loginGroupTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.loginGroupTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.loginGroupTableLayout.Controls.Add(this.usernameLabel, 1, 0);
            this.loginGroupTableLayout.Controls.Add(this.passwordLabel, 1, 1);
            this.loginGroupTableLayout.Controls.Add(this.usernameText, 2, 0);
            this.loginGroupTableLayout.Controls.Add(this.passwordText, 2, 1);
            this.loginGroupTableLayout.Controls.Add(this.loginIcon, 0, 0);
            this.loginGroupTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginGroupTableLayout.Location = new System.Drawing.Point(3, 16);
            this.loginGroupTableLayout.Name = "loginGroupTableLayout";
            this.loginGroupTableLayout.RowCount = 2;
            this.loginGroupTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.loginGroupTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.loginGroupTableLayout.Size = new System.Drawing.Size(360, 88);
            this.loginGroupTableLayout.TabIndex = 0;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(73, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(81, 44);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username";
            this.usernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.Location = new System.Drawing.Point(73, 44);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(81, 44);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "Password";
            this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // usernameText
            // 
            this.usernameText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameText.Location = new System.Drawing.Point(160, 11);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(197, 21);
            this.usernameText.TabIndex = 0;
            // 
            // passwordText
            // 
            this.passwordText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordText.Location = new System.Drawing.Point(160, 55);
            this.passwordText.Name = "passwordText";
            this.passwordText.PasswordChar = '*';
            this.passwordText.Size = new System.Drawing.Size(197, 21);
            this.passwordText.TabIndex = 1;
            // 
            // loginIcon
            // 
            this.loginIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginIcon.Image = ((System.Drawing.Image)(resources.GetObject("loginIcon.Image")));
            this.loginIcon.Location = new System.Drawing.Point(3, 12);
            this.loginIcon.Name = "loginIcon";
            this.loginGroupTableLayout.SetRowSpan(this.loginIcon, 2);
            this.loginIcon.Size = new System.Drawing.Size(64, 64);
            this.loginIcon.TabIndex = 4;
            this.loginIcon.TabStop = false;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.submitButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(382, 183);
            this.Controls.Add(this.mainTableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.Text = "Webcourses Login";
            this.mainTableLayout.ResumeLayout(false);
            this.mainTableLayout.PerformLayout();
            this.bottomTableLayout.ResumeLayout(false);
            this.bottomTableLayout.PerformLayout();
            this.loginGroupBox.ResumeLayout(false);
            this.loginGroupTableLayout.ResumeLayout(false);
            this.loginGroupTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.TableLayoutPanel bottomTableLayout;
        private System.Windows.Forms.Label warningLabel;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.GroupBox loginGroupBox;
        private System.Windows.Forms.TableLayoutPanel loginGroupTableLayout;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox usernameText;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.PictureBox loginIcon;
    }
}