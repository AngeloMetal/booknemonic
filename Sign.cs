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

namespace Mnemonic_keys
{
    public partial class Sign : Form
    {
        public Sign()
        {
            InitializeComponent();
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
#pragma warning disable CS0642 // Possible mistaken empty statement
            if (mnemonicTextbox.Text.Any(Char.IsLetter))
            {
                {
                    //it gets rid of any useless spaces of the mnemonicTextbox
                    while (mnemonicTextbox.Text.Substring(mnemonicTextbox.Text.Length - 1) == " ")
                    {
                        mnemonicTextbox.Text = mnemonicTextbox.Text.Remove(mnemonicTextbox.Text.Length - 1, 1);
                    }

                    while (mnemonicTextbox.Text.Substring(0, 1) == " ")
                    {
                        mnemonicTextbox.Text = mnemonicTextbox.Text.Substring(1);
                    }

                    while (mnemonicTextbox.Text.Contains("  "))
                    {
                        mnemonicTextbox.Text = mnemonicTextbox.Text.Replace("  ", " ");
                    }
                }
            }
            else
            {
                mnemonicTextbox.Text = "";
            }
#pragma warning restore CS0642 // Possible mistaken empty statement




            try
            {
                if (mnemonicTextbox.Text.Split(" ").Length != 12)
                {
                    MessageBox.Show("Enter your 12 private words if you want to sign a message.", "Invalid private words", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Mnemonic private_mnemonic = new Mnemonic(mnemonicTextbox.Text, Wordlist.English);
                mnemonicTextbox.Text = private_mnemonic.ToString();
                ExtKey private_root = private_mnemonic.DeriveExtKey();
                var firstprivkey = private_root.Derive(new KeyPath("m/44'/0'/0'/0/0"));
                var signature = firstprivkey.PrivateKey.SignMessage(message_box.Text);
                signature_box.Text = signature.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("These words are not valid. One or more of these words do not exist in the wordlist.", "Invalid words", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void verifymessage_Click(object sender, EventArgs e)
        {
            if (mnemonicTextbox.Text.Any(Char.IsLetter))
            {
                {
                    //it gets rid of any useless spaces of the mnemonicTextbox
                    while (mnemonicTextbox.Text.Substring(mnemonicTextbox.Text.Length - 1) == " ")
                    {
                        mnemonicTextbox.Text = mnemonicTextbox.Text.Remove(mnemonicTextbox.Text.Length - 1, 1);
                    }

                    while (mnemonicTextbox.Text.Substring(0, 1) == " ")
                    {
                        mnemonicTextbox.Text = mnemonicTextbox.Text.Substring(1);
                    }

                    while (mnemonicTextbox.Text.Contains("  "))
                    {
                        mnemonicTextbox.Text = mnemonicTextbox.Text.Replace("  ", " ");
                    }
                }
            }
            else
            {
                mnemonicTextbox.Text = "";
            }

            if (mnemonicTextbox.Text.Split(" ").Length != 24)
            {
                MessageBox.Show("Enter the 24 public words of the person that signed the message.", "Invalid public words", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string public_key = MnToHex(mnemonicTextbox.Text);
            String strPubKey = public_key;
            var pubKey = new PubKey(strPubKey);
            var addr = pubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main);
            try
            {
                bool verify_ = ((BitcoinPubKeyAddress)Network.Main.CreateBitcoinAddress(addr.ToString())).VerifyMessage(message_box.Text, signature_box.Text);
                if (verify_ == true)
                {
                    MessageBox.Show("Signature is verified!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Signature could not be verified.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Signature could not be verified.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

#pragma warning disable CS0436 // Type conflicts with imported type
        public string MnToHex(string mnemonic)
#pragma warning restore CS0436 // Type conflicts with imported type
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
            if(Clipboard.GetText() != null)
            {
                mnemonicTextbox.Text = Clipboard.GetText();
            }
          
        }
    }
}
