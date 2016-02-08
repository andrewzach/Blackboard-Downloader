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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.quitButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.warningLabel = new System.Windows.Forms.Label();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usernameText = new System.Windows.Forms.TextBox();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.welcomeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(382, 183);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.80883F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.93628F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.2549F));
            this.tableLayoutPanel2.Controls.Add(this.quitButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.submitButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.warningLabel, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 146);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(376, 34);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // quitButton
            // 
            this.quitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quitButton.Location = new System.Drawing.Point(301, 3);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(72, 28);
            this.quitButton.TabIndex = 12;
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
            this.submitButton.TabIndex = 10;
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
            this.warningLabel.Click += new System.EventHandler(this.label4_Click);
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLabel.Location = new System.Drawing.Point(3, 0);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(359, 20);
            this.welcomeLabel.TabIndex = 0;
            this.welcomeLabel.Text = "Welcome! Please enter your Webcourses login";
            this.welcomeLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(8, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 107);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.Controls.Add(this.usernameLabel, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.usernameText, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.passwordText, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(360, 86);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(73, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(81, 43);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username";
            this.usernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(73, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 43);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // usernameText
            // 
            this.usernameText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameText.Location = new System.Drawing.Point(160, 9);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(197, 24);
            this.usernameText.TabIndex = 2;
            // 
            // passwordText
            // 
            this.passwordText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordText.Location = new System.Drawing.Point(160, 52);
            this.passwordText.Name = "passwordText";
            this.passwordText.PasswordChar = '*';
            this.passwordText.Size = new System.Drawing.Size(197, 24);
            this.passwordText.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanel3.SetRowSpan(this.pictureBox1, 2);
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.submitButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(382, 183);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LoginForm";
            this.Text = "Webcourses Login";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label warningLabel;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usernameText;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}