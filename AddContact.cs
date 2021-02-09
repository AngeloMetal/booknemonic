using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Booknemonic
{
    public partial class AddContact : Form
    {
        public AddContact()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (publicWordsBox.Text.Any(Char.IsLetter))
            {
                //it gets rid of any useless spaces of the mnemonicbox
                while (publicWordsBox.Text.Substring(publicWordsBox.Text.Length - 1) == " ")
                {
                    publicWordsBox.Text = publicWordsBox.Text.Remove(publicWordsBox.Text.Length - 1, 1);
                }

                while (publicWordsBox.Text.Substring(0, 1) == " ")
                {
                    publicWordsBox.Text = publicWordsBox.Text.Substring(1);
                }

                while (publicWordsBox.Text.Contains("  "))
                {
                    publicWordsBox.Text = publicWordsBox.Text.Replace("  ", " ");
                }
            }
            else
            {
                publicWordsBox.Text = "";
            }

            if (publicWordsBox.Text.Split(" ").Length != 24)
            {
                MessageBox.Show("Enter the 24 public words.", "Invalid public words", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            

            string text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/conf/contacts.txt");
            if(text.Contains(publicWordsBox.Text) == false)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/conf/contacts.txt", text + nameBox.Text + "," + publicWordsBox.Text + "|");
                label4.Text = nameBox.Text + " was added!";
                label4.ForeColor = Color.Green;
            }
            else
            {
                label4.Text = "These public words are already added.";
                label4.ForeColor = Color.Black;
            }

        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText() != null)
            {
                publicWordsBox.Text = Clipboard.GetText();
            }
        }
    }
}
