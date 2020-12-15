using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autarkysoft.Bitcoin;
using Autarkysoft.Bitcoin.Encoders;
using Autarkysoft.Bitcoin.ImprovementProposals;
using Mnemonic_keys;
using NBitcoin;

namespace Booknemonic
{
    public partial class ImportPrivate : Form
    {
        public ImportPrivate()
        {
            InitializeComponent();
        }

        public void ImportPrivateMnemonic()
        {
            if (textBox1.Text.Any(Char.IsLetter))
            {
                //it gets rid of any useless spaces of the textBox1
                while (textBox1.Text.Substring(textBox1.Text.Length - 1) == " ")
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                }

                while (textBox1.Text.Substring(0, 1) == " ")
                {
                    textBox1.Text = textBox1.Text.Substring(1);
                }

                while (textBox1.Text.Contains("  "))
                {
                    textBox1.Text = textBox1.Text.Replace("  ", " ");
                }
            }
            else
            {
                textBox1.Text = "";
            }

            try
            {
                if (textBox1.Text.Split(" ").Length != 12)
                {
                    MessageBox.Show("You have to enter your 12 private words if you want to get your public words.", "Invalid private words", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Mnemonic private_mnemonic = new Mnemonic(textBox1.Text, Wordlist.English);

                ExtKey private_root = private_mnemonic.DeriveExtKey();
                var firstprivkey = private_root.Derive(new KeyPath("m/44'/0'/0'/0/0"));
                string firstpublickey = firstprivkey.GetPublicKey().ToString();

                textBox2.Text = HexToMn(firstpublickey);
            }
            catch (Exception)
            {
                MessageBox.Show("These words are not valid. One or more of these words do not exist in the wordlist.", "Invalid words", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


#pragma warning disable CS0436 // Type conflicts with imported type
        public string HexToMn(string hex)
#pragma warning restore CS0436 // Type conflicts with imported type
        {
            byte[] entropy = Base16.Decode(hex);
            if (entropy.Length != 33)
                throw new ArgumentOutOfRangeException();

            byte[] ba = entropy.ConcatFast(new byte[3]);

            uint[] bits = new uint[9];
            for (int i = 0, j = 0; i < ba.Length - 1; i += 4, j++)
            {
                bits[j] = (uint)(ba[i + 3] | (ba[i + 2] << 8) | (ba[i + 1] << 16) | (ba[i] << 24));
            }

            int itemIndex = 0;
            int bitIndex = 0;
            uint[] wordIndexes = new uint[24];
            for (int i = 0; i < 24; i++)
            {
                if (bitIndex + 11 <= 32)
                {
                    wordIndexes[i] = (bits[itemIndex] << bitIndex) >> 21;
                }
                else
                {
                    wordIndexes[i] = ((bits[itemIndex] << bitIndex) >> 21) |
                                        (bits[itemIndex + 1] >> (53 - bitIndex));
                }

                bitIndex += 11;
                if (bitIndex >= 32)
                {
                    bitIndex -= 32;
                    itemIndex++;
                }
            }

            StringBuilder sb = new StringBuilder(wordIndexes.Length * 8);
            string[] allWords = BIP0039.GetAllWords(BIP0039.WordLists.English);
            for (int i = 0; i < wordIndexes.Length; i++)
            {
                sb.Append($"{allWords[wordIndexes[i]]} ");
            }

            sb.Length--;
            return sb.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ImportPrivateMnemonic();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void signVerifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sign form = new Sign();
            form.Show();
            form.Location = new Point(
             this.Location.X + this.Location.X / (8 / 7),
             this.Location.Y - this.Location.Y / 18
            );
        }

        private void encryptDecryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encrypt form = new Encrypt();
            form.Show();
            form.Location = new Point(
             this.Location.X + this.Location.X / (8 / 7),
             this.Location.Y - this.Location.Y / 18
            );

        }

        private void generateNewKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewKeys form = new NewKeys();
            form.Show();
            form.Location = this.Location;
            this.Hide();
            
        }

        private void helpStripMenuItem_Click(object sender, EventArgs e)
        {
            techBackground form = new techBackground();
            form.Show();
            form.Location = this.Location;
            this.Hide();
        }

        private void aboutThisSoftware_Click(object sender, EventArgs e)
        {
            aboutThis form = new aboutThis();
            form.Show();
            form.Location = this.Location;
            this.Hide();
            
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText() != null)
            {
               textBox1.Text = Clipboard.GetText();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                textBox2.SelectAll();
                textBox2.Focus();
                Clipboard.SetText(textBox2.Text);
            }
        
        }
    }
}
