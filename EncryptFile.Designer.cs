
namespace Booknemonic
{
    partial class EncryptFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncryptFile));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.publicWordsBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.privateWordsBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressText = new System.Windows.Forms.Label();
            this.pastePublicWords = new System.Windows.Forms.Button();
            this.pastePrivateWords = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(70, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Encrypt a file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(70, 305);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Decrypt a file";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // publicWordsBox
            // 
            this.publicWordsBox.Location = new System.Drawing.Point(70, 86);
            this.publicWordsBox.Name = "publicWordsBox";
            this.publicWordsBox.Size = new System.Drawing.Size(473, 23);
            this.publicWordsBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter the public words of the receiver:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enter your private words:";
            // 
            // privateWordsBox
            // 
            this.privateWordsBox.Location = new System.Drawing.Point(70, 263);
            this.privateWordsBox.Name = "privateWordsBox";
            this.privateWordsBox.Size = new System.Drawing.Size(473, 23);
            this.privateWordsBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 388);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(453, 30);
            this.label3.TabIndex = 6;
            this.label3.Text = "It is not recommended to encrypt/decrypt large data since ECC progress is very sl" +
    "ow.\r\nConsider using symmetric encryption instead. (Like AES)";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 506);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(646, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // progressText
            // 
            this.progressText.AutoSize = true;
            this.progressText.Location = new System.Drawing.Point(12, 485);
            this.progressText.Name = "progressText";
            this.progressText.Size = new System.Drawing.Size(0, 15);
            this.progressText.TabIndex = 8;
            // 
            // pastePublicWords
            // 
            this.pastePublicWords.Image = ((System.Drawing.Image)(resources.GetObject("pastePublicWords.Image")));
            this.pastePublicWords.Location = new System.Drawing.Point(550, 86);
            this.pastePublicWords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pastePublicWords.Name = "pastePublicWords";
            this.pastePublicWords.Size = new System.Drawing.Size(49, 23);
            this.pastePublicWords.TabIndex = 21;
            this.pastePublicWords.UseVisualStyleBackColor = true;
            this.pastePublicWords.Click += new System.EventHandler(this.pastePublicWords_Click);
            // 
            // pastePrivateWords
            // 
            this.pastePrivateWords.Image = ((System.Drawing.Image)(resources.GetObject("pastePrivateWords.Image")));
            this.pastePrivateWords.Location = new System.Drawing.Point(550, 263);
            this.pastePrivateWords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pastePrivateWords.Name = "pastePrivateWords";
            this.pastePrivateWords.Size = new System.Drawing.Size(49, 23);
            this.pastePrivateWords.TabIndex = 22;
            this.pastePrivateWords.UseVisualStyleBackColor = true;
            this.pastePrivateWords.Click += new System.EventHandler(this.pastePrivateWords_Click);
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(14, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(49, 22);
            this.button3.TabIndex = 23;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // EncryptFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 541);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pastePrivateWords);
            this.Controls.Add(this.pastePublicWords);
            this.Controls.Add(this.progressText);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.privateWordsBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.publicWordsBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EncryptFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Encrypt/Decrypt a file";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox publicWordsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox privateWordsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progressText;
        private System.Windows.Forms.Button pastePublicWords;
        private System.Windows.Forms.Button pastePrivateWords;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}