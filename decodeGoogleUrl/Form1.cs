using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Web;
using ClipboardAssist;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        ClipboardMonitor monitor;
        public Form1()
        {   
            InitializeComponent();
            monitor = new ClipboardMonitor();
            monitor.ClipboardChanged += new EventHandler<ClipboardChangedEventArgs>(monitor_ClipboardChanged);
            
        }

        void monitor_ClipboardChanged(object sender, ClipboardChangedEventArgs e)
        {
            if(Clipboard.GetText().Contains("encrypted.google.com")){
                textBox1.Text = Clipboard.GetText();
                FindUrl();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FindUrl();
        }

        private void FindUrl()
        {
            Regex regex = new Regex("url=(.*?)&");
            Match match = regex.Match(textBox1.Text);
            if (match.Groups.Count > 1)
            {
                textBox2.Text = Uri.UnescapeDataString(Regex.Unescape(match.Groups[1].Value)).Substring(7);
                if (textBox2.Text[0] == '/')
                    textBox2.Text = textBox2.Text.Substring(1);

                Clipboard.SetText(textBox2.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            textBox1.Text = Clipboard.GetText();
            FindUrl();

        }
    }
}
