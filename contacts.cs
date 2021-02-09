using Mnemonic_keys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Booknemonic
{
    public partial class contacts : Form
    {
        public contacts()
        {
            InitializeComponent();
            Start();
        }

        void Start()
        {
            toolTip1.SetToolTip(addContact, "Add a new contact");
            
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/conf/contacts.txt") == false)
            {
                if(Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/conf/") == false)
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/conf/");
                }
                File.Create(AppDomain.CurrentDomain.BaseDirectory + "/conf/contacts.txt").Dispose();
                return;
            }
            string text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/conf/contacts.txt");
            string[] content = text.Split("|");

            if(content.Length == 1)
            {
                return;
            }
            else
            {
                label3.Text = "";
              
            }

            List<TextBox> boxes = new List<TextBox>();
            List<Label> labels = new List<Label>();
            List<Button> copyButton = new List<Button>();
            List<Button> encryptButton = new List<Button>();
            List<Button> verifyButton = new List<Button>();
            List<Button> deleteButton = new List<Button>();
            for (int i=0; i<content.Length-1; i++)
            {
                
                boxes.Add(new TextBox());
                boxes[i].Name = "contactBox" + i.ToString();
                boxes[i].Location = new Point(181, 58 + i*45);
                boxes[i].Size = new Size(410, 23);
                boxes[i].Text = content[i].Split(",")[1];
                boxes[i].ReadOnly = true;

                labels.Add(new Label());
                labels[i].Name = "contactText" + i.ToString();
                labels[i].Location = new Point(24, 58 + i*45);
                labels[i].Text = content[i].Split(",")[0];

                copyButton.Add(new Button());
                copyButton[i].Name = "copyButton" + i.ToString();
                copyButton[i].Location = new Point(599, 58 + i*45);
                copyButton[i].Text = "Copy";
                copyButton[i].Size = new Size(77, 23);
                toolTip1.SetToolTip(copyButton[i], "Copy the public words");
                copyButton[i].Click += (sender, args) =>
                {
                    var button = sender as Button;
                    if (button != null)
                    {
                        int pos = int.Parse(button.Name.Split("n")[1]);
                        Clipboard.SetText(boxes[pos].Text);
                        boxes[pos].SelectAll();
                        boxes[pos].Focus();
                    }
                };

                encryptButton.Add(new Button());
                encryptButton[i].Name = "encryptButtonT" + i.ToString();
                encryptButton[i].Location = new Point(682, 58 + i * 45);
                encryptButton[i].Text = "Encrypt";
                encryptButton[i].Size = new Size(77, 23);
                toolTip1.SetToolTip(encryptButton[i], "Encrypt a message to that person.");
                encryptButton[i].Click += (sender, args) =>
                {
                    var button = sender as Button;
                    if (button != null)
                    {
                        int pos = int.Parse(button.Name.Split("T")[1].ToString());
                        Encrypt form = new Encrypt(boxes[pos].Text);
                        form.Show();
                        form.Location = new Point(
                            this.Location.X + this.Location.X / (8 / 7),
                            this.Location.Y - this.Location.Y / 18
                        );
                        this.Hide();
                    }
                };

                verifyButton.Add(new Button());
                verifyButton[i].Name = "verifyButton" + i.ToString();
                verifyButton[i].Location = new Point(765, 58 + i * 45);
                verifyButton[i].Text = "Verify";
                verifyButton[i].Size = new Size(77, 23);
                toolTip1.SetToolTip(verifyButton[i], "Verify a message from that person.");
                verifyButton[i].Click += (sender, args) =>
                {
                    var button = sender as Button;
                    if (button != null)
                    {
                        int pos = int.Parse(button.Name.Split("n")[1].ToString());
                        Sign form = new Sign(boxes[pos].Text);
                        form.Show();
                        form.Location = new Point(
                            this.Location.X + this.Location.X / (8 / 7),
                            this.Location.Y - this.Location.Y / 18
                        );
                        this.Hide();
                    }
                };

                deleteButton.Add(new Button());
                deleteButton[i].Name = "deleteButton" + i.ToString();
                deleteButton[i].Location = new Point(857, 58 + i * 45);
                deleteButton[i].Size = new Size(23, 23);
                toolTip1.SetToolTip(deleteButton[i], "Delete this contact.");
                deleteButton[i].Click += (sender, args) =>
                {
                    var button = sender as Button;
                    if (button != null)
                    {
                       

                        int pos = int.Parse(button.Name.Split("n")[1].ToString());
                        string path = AppDomain.CurrentDomain.BaseDirectory + "/conf/contacts.txt";
                        string content = File.ReadAllText(path);
                        File.WriteAllText(path, content.Replace(labels[pos].Text + "," + boxes[pos].Text + "|", ""));
                        encryptButton[pos].Enabled = false;
                        verifyButton[pos].Enabled = false;
                        copyButton[pos].Enabled = false;
                        boxes[pos].Enabled = false;
                        labels[pos].Text = "Deleted.";
                        deleteButton[pos].Enabled = false;

                        

                    }
                };



                deleteButton[i].Image = new Bitmap(Booknemonic.Properties.Resources.x1);

                this.Controls.Add(boxes[i]);
                this.Controls.Add(labels[i]);
                this.Controls.Add(copyButton[i]);
                this.Controls.Add(encryptButton[i]);
                this.Controls.Add(verifyButton[i]);
                this.Controls.Add(deleteButton[i]);
            }

            Label expand = new Label();
            expand.Text = " ";
            expand.Location = new Point(765, 58 + content.Length * 45);
            this.Controls.Add(expand);
            
            

            
        }

        public static Bitmap GetImageByName(string imageName)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);
            return (Bitmap)rm.GetObject(imageName);

        }

        public string GetEmbeddedResource(string namespacename, string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = namespacename + "." + filename;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void contacts_Load(object sender, EventArgs e)
        {

        }

        private void addContact_Click(object sender, EventArgs e)
        {
            AddContact form = new AddContact();
            form.Show();
            form.Location = this.Location;
            this.Hide();
        }
    }
}
