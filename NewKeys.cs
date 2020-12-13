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
using NBitcoin;
using System.Reflection;
using System.Security.Cryptography;
using Autarkysoft.Bitcoin.Cryptography;
using Autarkysoft.Bitcoin.Cryptography.Hashing;
using Autarkysoft.Bitcoin.Cryptography.KeyDerivationFunctions;
using Autarkysoft.Bitcoin.ImprovementProposals;
using Autarkysoft.Bitcoin;
using Autarkysoft.Bitcoin.Encoders;
using System.Text.RegularExpressions;
using Mnemonic_keys;

namespace Booknemonic
{
    public partial class NewKeys : Form
    {
        //This form is the main form of the software.
        public NewKeys()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createNewMnemonic();

        }

        //Generate private and public words
        public void createNewMnemonic()
        {
            Mnemonic private_mnemonic = new Mnemonic(Wordlist.English, WordCount.Twelve);
            textBox1.Text = private_mnemonic.ToString();
            ExtKey private_root = private_mnemonic.DeriveExtKey();
            var firstprivkey = private_root.Derive(new KeyPath("m/44'/0'/0'/0/0"));
            string firstpublickey = firstprivkey.GetPublicKey().ToString();
            textBox2.Text = HexToMn(firstpublickey);


        }

        //It converts any hexadecimal to mnemonic (words)
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
            createNewMnemonic();
        }



        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void importAPrivateMnemonicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportPrivate form = new ImportPrivate();
            form.Show();
            form.Location = this.Location;
            this.Hide();
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

        private void aboutThis_Click(object sender, EventArgs e)
        {
            aboutThis form = new aboutThis();
            form.Show();
            form.Location = this.Location;
            this.Hide();
        }

        private void technicalBackground_Click(object sender, EventArgs e)
        {
            techBackground form = new techBackground();
            form.Show();
            form.Location = this.Location;
            this.Hide();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
            textBox1.Focus();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
            textBox2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
            textBox1.Focus();
            Clipboard.SetText(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
            textBox2.Focus();
            Clipboard.SetText(textBox2.Text);
        }

        //Save words on a text file
        private void saveas_Click(object sender, EventArgs e)
        {
            string textContent = "----- PRIVATE WORDS -----\n" + textBox1.Text + "\n" + "-------------------------\n\n----- PUBLIC WORDS -----\n" + textBox2.Text + "\n-------------------------";

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save text Files";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.FileName = "words";
            saveFileDialog1.Filter = "Text files only (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textContent);
            }
        }


    }

}
