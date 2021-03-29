using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Runtime.InteropServices;

namespace DiscordThatDoesntEatAllOfMyRam
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool SetForegroundWindow(HandleRef hWnd);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;

            CefSettings settings = new CefSettings();

            settings.CachePath = Application.StartupPath + "/cache";

            settings.CefCommandLineArgs.Add("enable-media-stream", "1");

            Cef.Initialize(settings);

            ChromiumWebBrowser chromeBrowser = new ChromiumWebBrowser("https://discord.com/login/");

            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                notifyIcon1.Visible = true;
                this.Hide();
                e.Cancel = true;
            }
        }

        private void notifyIcon1_Clicked(object sender, EventArgs e)
        {
            
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                notifyIcon1.Visible = false;
                this.Show();
            }

            if (e.Button == MouseButtons.Right)
            {
                SetForegroundWindow(new HandleRef(this, this.Handle));
                int x = Control.MousePosition.X;
                int y = Control.MousePosition.Y;
                x = x - 10;
                y = y - 40;
                this.contextMenuStrip1.Show(x, y);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Show();
        }
    }
}
