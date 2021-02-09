namespace Mnemonic_keys
{
    partial class Sign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sign));
            this.signmessage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.message_box = new System.Windows.Forms.RichTextBox();
            this.verifymessage = new System.Windows.Forms.Button();
            this.mnemonicTextbox = new System.Windows.Forms.TextBox();
            this.signature_box = new System.Windows.Forms.RichTextBox();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateNewKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAPrivateMnemonicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signDecryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // signmessage
            // 
            this.signmessage.Location = new System.Drawing.Point(76, 486);
            this.signmessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.signmessage.Name = "signmessage";
            this.signmessage.Size = new System.Drawing.Size(122, 27);
            this.signmessage.TabIndex = 1;
            this.signmessage.Text = "Sign message";
            this.signmessage.UseVisualStyleBackColor = true;
            this.signmessage.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 243);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Words";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 114);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Message";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 359);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Signature";
            // 
            // message_box
            // 
            this.message_box.Location = new System.Drawing.Point(76, 29);
            this.message_box.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.message_box.Name = "message_box";
            this.message_box.Size = new System.Drawing.Size(513, 204);
            this.message_box.TabIndex = 8;
            this.message_box.Text = "";
            // 
            // verifymessage
            // 
            this.verifymessage.Location = new System.Drawing.Point(467, 486);
            this.verifymessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.verifymessage.Name = "verifymessage";
            this.verifymessage.Size = new System.Drawing.Size(122, 27);
            this.verifymessage.TabIndex = 9;
            this.verifymessage.Text = "Verify message";
            this.verifymessage.UseVisualStyleBackColor = true;
            this.verifymessage.Click += new System.EventHandler(this.verifymessage_Click);
            // 
            // mnemonicTextbox
            // 
            this.mnemonicTextbox.Location = new System.Drawing.Point(76, 240);
            this.mnemonicTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mnemonicTextbox.Name = "mnemonicTextbox";
            this.mnemonicTextbox.Size = new System.Drawing.Size(513, 23);
            this.mnemonicTextbox.TabIndex = 4;
            // 
            // signature_box
            // 
            this.signature_box.Location = new System.Drawing.Point(76, 269);
            this.signature_box.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.signature_box.Name = "signature_box";
            this.signature_box.Size = new System.Drawing.Size(513, 202);
            this.signature_box.TabIndex = 10;
            this.signature_box.Text = "";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateNewKeysToolStripMenuItem,
            this.importAPrivateMnemonicToolStripMenuItem,
            this.signDecryptToolStripMenuItem,
            this.encryptStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // generateNewKeysToolStripMenuItem
            // 
            this.generateNewKeysToolStripMenuItem.Name = "generateNewKeysToolStripMenuItem";
            this.generateNewKeysToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.generateNewKeysToolStripMenuItem.Text = "Generate new keys";
            // 
            // importAPrivateMnemonicToolStripMenuItem
            // 
            this.importAPrivateMnemonicToolStripMenuItem.Name = "importAPrivateMnemonicToolStripMenuItem";
            this.importAPrivateMnemonicToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.importAPrivateMnemonicToolStripMenuItem.Text = "Import a private mnemonic";
            // 
            // signDecryptToolStripMenuItem
            // 
            this.signDecryptToolStripMenuItem.Name = "signDecryptToolStripMenuItem";
            this.signDecryptToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.signDecryptToolStripMenuItem.Text = "Sign/Verify";
            // 
            // encryptStripMenuItem
            // 
            this.encryptStripMenuItem.Name = "encryptStripMenuItem";
            this.encryptStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.encryptStripMenuItem.Text = "Encrypt/Decrypt";
            // 
            // pasteButton
            // 
            this.pasteButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteButton.Image")));
            this.pasteButton.Location = new System.Drawing.Point(597, 239);
            this.pasteButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(49, 23);
            this.pasteButton.TabIndex = 11;
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // Sign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 541);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.signature_box);
            this.Controls.Add(this.verifymessage);
            this.Controls.Add(this.message_box);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mnemonicTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.signmessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Sign";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sign/Verify a message";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button signmessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox message_box;
        private System.Windows.Forms.Button verifymessage;
        private System.Windows.Forms.TextBox mnemonicTextbox;
        private System.Windows.Forms.RichTextBox signature_box;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateNewKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importAPrivateMnemonicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signDecryptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encryptStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateNewKeys;
        private System.Windows.Forms.Button pasteButton;
    }
}