using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autarkysoft.Bitcoin.Cryptography.Hashing;
using Autarkysoft.Bitcoin.Cryptography;
using Autarkysoft.Bitcoin.Encoders;
using NBitcoin;
using Autarkysoft.Bitcoin.Cryptography.Asymmetric.KeyPairs;
using System.Security.Cryptography;
using Autarkysoft.Bitcoin.ImprovementProposals;
using Autarkysoft.Bitcoin;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Mnemonic_keys;

namespace Booknemonic
{
    public partial class Encrypt : Form
    {
        [DllImport("user32")]
        private extern static int GetCaretPos(out Point p);

        public Encrypt(string publicwords)
        {
            InitializeComponent();
            toolTip1.SetToolTip(button1, "Encrypt/Decrypt a file");
            if(publicwords != null)
            {
                mnemonicbox.Text = publicwords;
            }
            

        }

        //Encrypts the message
        private void encryptbutton_Click(object sender, EventArgs e)
        {
            if (mnemonicbox.Text.Any(Char.IsLetter))
            {
                //it gets rid of any useless spaces of the mnemonicbox
                while (mnemonicbox.Text.Substring(mnemonicbox.Text.Length - 1) == " ")
                {
                    mnemonicbox.Text = mnemonicbox.Text.Remove(mnemonicbox.Text.Length - 1, 1);
                }

                while (mnemonicbox.Text.Substring(0, 1) == " ")
                {
                    mnemonicbox.Text = mnemonicbox.Text.Substring(1);
                }

                while (mnemonicbox.Text.Contains("  "))
                {
                    mnemonicbox.Text = mnemonicbox.Text.Replace("  ", " ");
                }
            }
            else
            {
                mnemonicbox.Text = "";
            }

            try
            {
                if (mnemonicbox.Text.Split(" ").Length != 24)
                {
                    MessageBox.Show("Enter the 24 public words of the person you want to encrypt your message. Then he can decrypt it using his private words.", "Invalid public words", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string public_key = MnToHex(mnemonicbox.Text);
                bool success = PublicKey.TryRead(Base16.Decode(public_key), out PublicKey pubkey);
                string encrypted = pubkey.Encrypt(messagebox.Text);
                encryptedbox.Text = encrypted;
            }
            catch (Exception)
            {
                MessageBox.Show("Message could not be encrypted. (Recheck the public words)", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void decryptbutton_Click(object sender, EventArgs e)
        {
            if (mnemonicbox.Text.Any(Char.IsLetter))
            {
                //it gets rid of any useless spaces of the mnemonicbox
                while (mnemonicbox.Text.Substring(mnemonicbox.Text.Length - 1) == " ")
                {
                    mnemonicbox.Text = mnemonicbox.Text.Remove(mnemonicbox.Text.Length - 1, 1);
                }

                while (mnemonicbox.Text.Substring(0, 1) == " ")
                {
                    mnemonicbox.Text = mnemonicbox.Text.Substring(1);
                }

                while (mnemonicbox.Text.Contains("  "))
                {
                    mnemonicbox.Text = mnemonicbox.Text.Replace("  ", " ");
                }
            }
            else
            {
                mnemonicbox.Text = "";
            }

            try
            {
                if (mnemonicbox.Text.Split(" ").Length != 12)
                {
                    MessageBox.Show("Enter your 12 private words if you want to decrypt a message.", "Invalid private words", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Mnemonic private_mnemonic = new Mnemonic(mnemonicbox.Text, Wordlist.English);
                ExtKey private_root = private_mnemonic.DeriveExtKey();
                var privatekey = private_root.Derive(new KeyPath("m/44'/0'/0'/0/0")).PrivateKey;
                BitcoinSecret WIF = privatekey.GetWif(Network.Main);

                using PrivateKey key = new PrivateKey(WIF.ToString());
                string decrypted = key.Decrypt(encryptedbox.Text);
                messagebox.Text = decrypted;
            }
            catch (Exception)
            {
                MessageBox.Show("Message could not be decrypted.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //It converts words to hexadecimal
        public string MnToHex(string mnemonic)
        {
            if (string.IsNullOrWhiteSpace(mnemonic))
                throw new ArgumentNullException(nameof(mnemonic), "Seed can not be null or empty!");
            string[] allWords = BIP0039.GetAllWords(BIP0039.WordLists.English);

            string[] words = mnemonic.Normalize(NormalizationForm.FormKD)
                                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (!words.All(x => allWords.Contains(x)))
            {
                throw new ArgumentException(nameof(mnemonic), "Seed has invalid words.");
            }
            if (!new int[] { 12, 15, 18, 21, 24 }.Contains(words.Length))
            {
                throw new FormatException("Invalid seed length. It should be ∈{12, 15, 18, 21, 24}");
            }

            uint[] wordIndexes = new uint[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                wordIndexes[i] = (uint)Array.IndexOf(allWords, words[i]);
            }

            int ENTCS = words.Length * 11;
            int CS = ENTCS % 32;
            int ENT = ENTCS - CS;

            byte[] entropy = new byte[(ENT / 8) + 1];

            int itemIndex = 0;
            int bitIndex = 0;
            for (int i = 0; i < entropy.Length - 1; i++)
            {
                if (bitIndex + 8 <= 11)
                {
                    entropy[i] = (byte)(wordIndexes[itemIndex] >> (3 - bitIndex));
                }
                else
                {
                    entropy[i] = (byte)(((wordIndexes[itemIndex] << (bitIndex - 3)) & 0xff) |
                                            (wordIndexes[itemIndex + 1] >> (14 - bitIndex)));
                }

                bitIndex += 8;
                if (bitIndex >= 11)
                {
                    bitIndex -= 11;
                    itemIndex++;
                }
            }

            uint mask = (1U << CS) - 1;
            entropy[^1] = (byte)(wordIndexes[itemIndex] & mask);

            return entropy.ToBase16();
        }



        private void pasteButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText() != null)
            {
                mnemonicbox.Text = Clipboard.GetText();
            }
        }


        private void mnemonicbox_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void mnemonicbox_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            EncryptFile form = new EncryptFile(mnemonicbox.Text);
            form.Show();
            form.Location = new Point(
             this.Location.X,
             this.Location.Y
            );
            this.Close();
        }
    }
}
