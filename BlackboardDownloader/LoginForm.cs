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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
    }
}
