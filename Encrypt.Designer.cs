﻿
namespace Booknemonic
{
    partial class Encrypt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Encrypt));
            this.messagebox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mnemonicbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.encryptedbox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.encryptbutton = new System.Windows.Forms.Button();
            this.decryptbutton = new System.Windows.Forms.Button();
            this.pasteButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // messagebox
            // 
            this.messagebox.Location = new System.Drawing.Point(76, 29);
            this.messagebox.Name = "messagebox";
            this.messagebox.Size = new System.Drawing.Size(513, 204);
            this.messagebox.TabIndex = 0;
            this.messagebox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Message";
            // 
            // mnemonicbox
            // 
            this.mnemonicbox.AutoCompleteCustomSource.AddRange(new string[] {
            "test1",
            "test2",
            "test3"});
            this.mnemonicbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.mnemonicbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.mnemonicbox.Location = new System.Drawing.Point(76, 240);
            this.mnemonicbox.Name = "mnemonicbox";
            this.mnemonicbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mnemonicbox.Size = new System.Drawing.Size(513, 23);
            this.mnemonicbox.TabIndex = 2;
            this.mnemonicbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mnemonicbox_KeyPress);
            this.mnemonicbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mnemonicbox_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Words";
            // 
            // encryptedbox
            // 
            this.encryptedbox.Location = new System.Drawing.Point(76, 269);
            this.encryptedbox.Name = "encryptedbox";
            this.encryptedbox.Size = new System.Drawing.Size(513, 202);
            this.encryptedbox.TabIndex = 4;
            this.encryptedbox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 359);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Encrypted";
            // 
            // encryptbutton
            // 
            this.encryptbutton.Location = new System.Drawing.Point(76, 486);
            this.encryptbutton.Name = "encryptbutton";
            this.encryptbutton.Size = new System.Drawing.Size(122, 27);
            this.encryptbutton.TabIndex = 6;
            this.encryptbutton.Text = "Encrypt";
            this.encryptbutton.UseVisualStyleBackColor = true;
            this.encryptbutton.Click += new System.EventHandler(this.encryptbutton_Click);
            // 
            // decryptbutton
            // 
            this.decryptbutton.Location = new System.Drawing.Point(467, 486);
            this.decryptbutton.Name = "decryptbutton";
            this.decryptbutton.Size = new System.Drawing.Size(122, 27);
            this.decryptbutton.TabIndex = 7;
            this.decryptbutton.Text = "Decrypt";
            this.decryptbutton.UseVisualStyleBackColor = true;
            this.decryptbutton.Click += new System.EventHandler(this.decryptbutton_Click);
            // 
            // pasteButton
            // 
            this.pasteButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteButton.Image")));
            this.pasteButton.Location = new System.Drawing.Point(597, 239);
            this.pasteButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(49, 23);
            this.pasteButton.TabIndex = 12;
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(14, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 22);
            this.button1.TabIndex = 13;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Encrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(670, 541);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.decryptbutton);
            this.Controls.Add(this.encryptbutton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.encryptedbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mnemonicbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.messagebox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Encrypt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Encrypt/Decrypt a message";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox messagebox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mnemonicbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox encryptedbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button encryptbutton;
        private System.Windows.Forms.Button decryptbutton;
        private System.Windows.Forms.Button pasteButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}