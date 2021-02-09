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
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO.Compression;
using System.Drawing;

namespace Booknemonic
{
    public partial class EncryptFile : Form
    {
        public EncryptFile(string publicWords)
        {
            InitializeComponent();
            toolTip1.SetToolTip(button3, "Encrypt/Decrypt a message");
            publicWordsBox.Text = publicWords;
        }

        private void button1_Click(object sender, EventArgs e)
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
                MessageBox.Show("Enter the 24 public words of the person you want to encrypt your file. Then he can decrypt it using his private words.", "Invalid public words", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FilterIndex = 0;
                progressText.Text = "Encrypting...";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    progressBar1.Value = 25;

                    byte[] fileBytes = File.ReadAllBytes(openFileDialog1.FileName);
                    
                    string content = Convert.ToBase64String(fileBytes);

                    string public_key = MnToHex(publicWordsBox.Text);
                    bool success = PublicKey.TryRead(Base16.Decode(public_key), out PublicKey pubkey);
                    string encrypted = pubkey.Encrypt(openFileDialog1.SafeFileName + "|" + content);
                  
                    File.WriteAllText(openFileDialog1.FileName + ".encrypted", CompressString(encrypted));
                    progressBar1.Value = 100;
                    progressText.Text = "Done.";
                }
                else
                {
                    progressText.Text = "";

                }
            }
            catch (Exception)
            {
                MessageBox.Show("File could not be encrypted. (Invalid public words or memory limit reached)", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Value = 100;
                progressText.Text = " Done. (Failure)";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (privateWordsBox.Text.Any(Char.IsLetter))
            {
                //it gets rid of any useless spaces of the mnemonicbox
                while (privateWordsBox.Text.Substring(privateWordsBox.Text.Length - 1) == " ")
                {
                    privateWordsBox.Text = privateWordsBox.Text.Remove(privateWordsBox.Text.Length - 1, 1);
                }

                while (privateWordsBox.Text.Substring(0, 1) == " ")
                {
                    privateWordsBox.Text = privateWordsBox.Text.Substring(1);
                }

                while (privateWordsBox.Text.Contains("  "))
                {
                    privateWordsBox.Text = privateWordsBox.Text.Replace("  ", " ");
                }
            }
            else
            {
                privateWordsBox.Text = "";
            }

            if (privateWordsBox.Text.Split(" ").Length != 12)
            {
                MessageBox.Show("Enter your 12 private words if you want to decrypt a file.", "Invalid private words", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            progressText.Text = "Decrypting...";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FilterIndex = 0;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                progressBar1.Value = 25;

                try
                {
                    Mnemonic private_mnemonic = new Mnemonic(privateWordsBox.Text, Wordlist.English);
                    ExtKey private_root = private_mnemonic.DeriveExtKey();
                    var privatekey = private_root.Derive(new KeyPath("m/44'/0'/0'/0/0")).PrivateKey;
                    BitcoinSecret WIF = privatekey.GetWif(Network.Main);

                    string str = File.ReadAllText(openFileDialog1.FileName);
                    using PrivateKey key = new PrivateKey(WIF.ToString());

                    string decompressed = DecompressString(str);
                    string decrypted = key.Decrypt(decompressed);
                    string file_name = decrypted.Split("|")[0];
                    byte[] arr = Convert.FromBase64String(decrypted.Split("|")[1]);

                    Stream stream = new FileStream(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, file_name), FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(stream);

                    foreach (var b in arr)
                    {
                        bw.Write(b);
                    }

                    bw.Flush();
                    bw.Close();

                    progressBar1.Value = 100;
                    progressText.Text = "Done.";
                }
                catch (Exception)
                {
                    MessageBox.Show("File could not be decrypted. (Invalid private words or memory limit reached)", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar1.Value = 100;
                    progressText.Text = "Done. (failure)";
                }


            }
            else
            {
                progressText.Text = "";

            }

        }

        public static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        public static string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

            public static string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);


            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }

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

        private void pastePublicWords_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText() != null)
            {
                publicWordsBox.Text = Clipboard.GetText();
            }
        }

        private void pastePrivateWords_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText() != null)
            {
                privateWordsBox.Text = Clipboard.GetText();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Encrypt form = new Encrypt(null);
            form.Show();
            form.Location = new Point(
             this.Location.X,
             this.Location.Y
            );
            this.Close();
        }
    }
}
