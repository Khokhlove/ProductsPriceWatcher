using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HtmlParser
{
    public partial class Console : Form
    {
        public ListView list;
        public Console()
        {
            InitializeComponent();
            list = listView1;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            TextBox t = textBox1;
            CConsole.GetInstance().Log(t.Text);
            t.Clear();
        }

        private void EnterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(this, new EventArgs());
            }
        }

        private void Console_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
