using System;
using NBitcoin;
using Autarkysoft.Bitcoin;
using Autarkysoft.Bitcoin.Cryptography;
using Autarkysoft.Bitcoin.Cryptography.Hashing;
using Autarkysoft.Bitcoin.Cryptography.KeyDerivationFunctions;
using Autarkysoft.Bitcoin.ImprovementProposals;
using Autarkysoft.Bitcoin.Encoders;
using System.Text;
using System.Reflection;
using System.Linq;
using Autarkysoft.Bitcoin.Cryptography.Asymmetric.KeyPairs;

namespace Booknemonic_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Program call = new Program();
            /*if (args[0] == "--help")
            {
                Console.WriteLine("\n*** Booknemonic command line program. ***");
                Console.WriteLine("\nArguments:");
                Console.WriteLine("\n --generate");
                Console.WriteLine("\n --import <public words>");
                Console.WriteLine("\n --sign <private words> <message>");
                Console.WriteLine("\n --verify <public words> <message> <signature>");
                Console.WriteLine("\n --encrypt <public words> <message>");
                Console.WriteLine("\n --decrypt <private words> <encrypted message>");
                Console.WriteLine("---------------------");
                return;
            }
            if(args[0] == "--generate")
            {
                call.mainFunc(false, 1);
            }*/
            Console.WriteLine("*** Booknemonic command line program. ***");
            Console.WriteLine("Enter one of the following numbers to continue:");
            Console.WriteLine("(1) Generate new words.");
            Console.WriteLine("(2) Import private words.");
            Console.WriteLine("(3) Sign a message.");
            Console.WriteLine("(4) Verify a message.");
            Console.WriteLine("(5) Encrypt a message.");
            Console.WriteLine("(6) Decrypt a message.");
            Console.WriteLine("---------------------");
            
            call.mainFunc(false, 0);

        }

        public void mainFunc(bool reEnter, int optionalInt)
        {
            Program call = new Program();
            string privateWords = "";
            string publicWords = "";

            if (reEnter == true)
            {
                Console.WriteLine("\n---------------------\nRe-enter a number: ");
            }
            else
            {
                Console.WriteLine("Enter a number: ");
            }

            int intToSwitch = 0;
            if(optionalInt != 0)
            {
                intToSwitch = optionalInt;
            }
            else
            {
                intToSwitch = int.Parse(Console.ReadLine());
                
            }
            switch (intToSwitch.ToString())
            {
                case "1":
                    Mnemonic private_mnemonic = new Mnemonic(Wordlist.English, WordCount.Twelve);
                    privateWords = private_mnemonic.ToString();
                    ExtKey private_root = private_mnemonic.DeriveExtKey();
                    var firstprivkey = private_root.Derive(new KeyPath("m/44'/0'/0'/0/0"));
                    string firstpublickey = firstprivkey.GetPublicKey().ToString();
                    publicWords = HexToMn(firstpublickey);
                    Console.WriteLine("\nPrivate words: " + privateWords);
                    Console.WriteLine("\nPublic words: " + publicWords);
                    break;
                case "2":
                    Console.WriteLine("\nEnter your private words to get your public ones:");

                    string line = Console.ReadLine();
                    try
                    {
                        if (line.Split(" ").Length != 12)
                        {
                            Console.WriteLine("\nYou have to enter your 12 private words if you want to get your public words.");
                            call.mainFunc(true, 0);
                        }

                        private_mnemonic = new Mnemonic(line, Wordlist.English);

                        private_root = private_mnemonic.DeriveExtKey();
                        firstprivkey = private_root.Derive(new KeyPath("m/44'/0'/0'/0/0"));
                        firstpublickey = firstprivkey.GetPublicKey().ToString();

                        Console.WriteLine("\nPublic words: " + HexToMn(firstpublickey));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nThese words are not valid. One or more of these words do not exist in the wordlist.");
                    }
                    break;
                case "3":
                            Console.WriteLine("\nEnter your private words:");
                            string _privateWords = Console.ReadLine();
                            Console.WriteLine("\nEnter a message:");
                            string _message = Console.ReadLine();
                            signAmessage(_privateWords, _message);
                    break;

                case "4":
                    Console.WriteLine("\nEnter the public words:");
                    string _publicWords = Console.ReadLine();
                    Console.WriteLine("\nEnter the message:");
                    _message = Console.ReadLine();
                    Console.WriteLine("\nEnter the signature:");
                    string signature_ = Console.ReadLine();
                    verifyAmessage(_publicWords, _message, signature_);
                    break;
                case "5":
                    Console.WriteLine("\nEnter the public words:");
                    _publicWords = Console.ReadLine();
                    Console.WriteLine("\nEnter the message:");
                    _message = Console.ReadLine();
                    encryptAmessage(_publicWords, _message);
                    break;
                case "6":
                    Console.WriteLine("\nEnter private words:");
                    _privateWords = Console.ReadLine();
                    Console.WriteLine("\nEnter the encrypted message:");
                    _message = Console.ReadLine();
                    decryptAmessage(_privateWords, _message);
                    break;

            }
            
            call.mainFunc(true, 0);
        }

        //It converts any hexadecimal to mnemonic (words)
#pragma warning disable CS0436 // Type conflicts with imported type

        public static string HexToMn(string hex)
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

        public void signAmessage(string mnemonic, string message_)
        {
#pragma warning disable CS0642 // Possible mistaken empty statement
            if (mnemonic.Any(Char.IsLetter))
            {
                {
                    //it gets rid of any useless spaces of the mnemonicTextbox
                    while (mnemonic.Substring(mnemonic.Length - 1) == " ")
                    {
                        mnemonic = mnemonic.Remove(mnemonic.Length - 1, 1);
                    }

                    while (mnemonic.Substring(0, 1) == " ")
                    {
                        mnemonic = mnemonic.Substring(1);
                    }

                    while (mnemonic.Contains("  "))
                    {
                        mnemonic = mnemonic.Replace("  ", " ");
                    }
                }
            }
            else
            {
                mnemonic = "";
            }
#pragma warning restore CS0642 // Possible mistaken empty statement




            try
            {
                if (mnemonic.Split(" ").Length != 12)
                {
                    Console.WriteLine("\nEnter your 12 private words if you want to sign a message.", "Invalid private words");
                    return;
                }
                Mnemonic private_mnemonic = new Mnemonic(mnemonic, Wordlist.English);
                mnemonic = private_mnemonic.ToString();
                ExtKey private_root = private_mnemonic.DeriveExtKey();
                var firstprivkey = private_root.Derive(new KeyPath("m/44'/0'/0'/0/0"));
                var signature = firstprivkey.PrivateKey.SignMessage(message_);
                Console.WriteLine("\nSignature: " + signature.ToString());
            }
            catch (Exception)
            {
                Console.WriteLine("\nThese words are not valid. One or more of these words do not exist in the wordlist.", "Invalid words");
            }

        }

        private void verifyAmessage(string mnemonic, string message_, string signature_)
        {
            if (mnemonic.Any(Char.IsLetter))
            {
                {
                    //it gets rid of any useless spaces of the mnemonicTextbox
                    while (mnemonic.Substring(mnemonic.Length - 1) == " ")
                    {
                        mnemonic = mnemonic.Remove(mnemonic.Length - 1, 1);
                    }

                    while (mnemonic.Substring(0, 1) == " ")
                    {
                        mnemonic = mnemonic.Substring(1);
                    }

                    while (mnemonic.Contains("  "))
                    {
                        mnemonic = mnemonic.Replace("  ", " ");
                    }
                }
            }
            else
            {
                mnemonic = "";
            }

            if (mnemonic.Split(" ").Length != 24)
            {
                Console.WriteLine("\nEnter the 24 public words of the person that signed the message.", "Invalid public words");
                return;
            }
            string public_key = MnToHex(mnemonic);
            String strPubKey = public_key;
            var pubKey = new PubKey(strPubKey);
            var addr = pubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main);
            try
            {
                bool verify_ = ((BitcoinPubKeyAddress)Network.Main.CreateBitcoinAddress(addr.ToString())).VerifyMessage(message_, signature_);
                if (verify_ == true)
                {
                    Console.WriteLine("\nSignature is verified!");
                }
                else
                {
                    Console.WriteLine("\nSignature could not be verified.");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("\nSignature could not be verified.");
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

        //Encrypts the message
        private void encryptAmessage(string mnemonic, string message_)
        {
            if (mnemonic.Any(Char.IsLetter))
            {
                //it gets rid of any useless spaces of the mnemonicbox
                while (mnemonic.Substring(mnemonic.Length - 1) == " ")
                {
                    mnemonic = mnemonic.Remove(mnemonic.Length - 1, 1);
                }

                while (mnemonic.Substring(0, 1) == " ")
                {
                    mnemonic = mnemonic.Substring(1);
                }

                while (mnemonic.Contains("  "))
                {
                    mnemonic = mnemonic.Replace("  ", " ");
                }
            }
            else
            {
                mnemonic = "";
            }

            try
            {
                if (mnemonic.Split(" ").Length != 24)
                {
                    Console.WriteLine("\nEnter the 24 public words of the person you want to encrypt your message. Then he can decrypt it using his private words.");
                    return;
                }
                string public_key = MnToHex(mnemonic);
                bool success = PublicKey.TryRead(Base16.Decode(public_key), out PublicKey pubkey);
                string encrypted = pubkey.Encrypt(message_);
                Console.WriteLine("\nEncrypted message: " + encrypted);
            }
            catch (Exception)
            {
                Console.WriteLine("\nMessage could not be encrypted. (Recheck the public words)");
            }

        }

        private void decryptAmessage(string mnemonic, string encryptedMessage)
        {
            if (mnemonic.Any(Char.IsLetter))
            {
                //it gets rid of any useless spaces of the mnemonicbox
                while (mnemonic.Substring(mnemonic.Length - 1) == " ")
                {
                    mnemonic = mnemonic.Remove(mnemonic.Length - 1, 1);
                }

                while (mnemonic.Substring(0, 1) == " ")
                {
                    mnemonic = mnemonic.Substring(1);
                }

                while (mnemonic.Contains("  "))
                {
                    mnemonic = mnemonic.Replace("  ", " ");
                }
            }
            else
            {
                mnemonic = "";
            }

            try
            {
                if (mnemonic.Split(" ").Length != 12)
                {
                    Console.WriteLine("Enter your 12 private words if you want to decrypt a message.", "Invalid private words");
                    return;
                }
                Mnemonic private_mnemonic = new Mnemonic(mnemonic, Wordlist.English);
                ExtKey private_root = private_mnemonic.DeriveExtKey();
                var privatekey = private_root.Derive(new KeyPath("m/44'/0'/0'/0/0")).PrivateKey;
                BitcoinSecret WIF = privatekey.GetWif(Network.Main);

                using PrivateKey key = new PrivateKey(WIF.ToString());
                string decrypted = key.Decrypt(encryptedMessage);
                Console.WriteLine("\nDecrypted message: " + decrypted);
            }
            catch (Exception)
            {
                Console.WriteLine("\nMessage could not be decrypted.");
            }

        }
    }
}
