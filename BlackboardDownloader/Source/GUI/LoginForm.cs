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
    // Displays a login form for Blackboard
    // Shown at start of program as a dialog
    public partial class LoginForm : Form
    {
        private Scraper scraper;

        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(Scraper scraper)
        {
            InitializeComponent();
            this.scraper = scraper;
        }

        public string GetUsername()
        {
            return usernameText.Text;
        }

        public string GetPassword()
        {
            return passwordText.Text;
        }

        // Tells the Scraper to login using the information entered by the user
        // Display invalid login warning if login is not successful.
        // If login successful, close the dialog
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            bool success = scraper.Login(usernameText.Text, passwordText.Text);
            if (success)
            {
                this.Close();
            }
            else
            {
                warningLabel.Text = "Invalid login, please try again.";
                passwordText.ResetText();
                passwordText.Focus();
            }
        }

        // Exits the program
        private void quitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            this.Close();
        }
    }
}
