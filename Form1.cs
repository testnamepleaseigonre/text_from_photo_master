using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace text_from_photo_master
{
    public partial class Form1 : Form
    {
        Sender sndr = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void UploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                label1.Text = fileDialog.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread senderThread = new Thread(() =>
            {
                sndr = new Sender(label1.Text);
                sndr.Send();
            });
            senderThread.Start();
        }
    }
}
