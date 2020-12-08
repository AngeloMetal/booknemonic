﻿using Booknemonic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mnemonic_keys
{
    public partial class techBackground : Form
    {
        public techBackground()
        {
            InitializeComponent();
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
            form.Location = this.Location;
        }

        private void encryptDecryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encrypt form = new Encrypt();
            form.Show();
            form.Location = this.Location;

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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void techBackground_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://en.wikipedia.org/wiki/Digital_signature");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://en.wikipedia.org/wiki/Elliptic-curve_cryptography");
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
