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
        public CConsole cCs = new CConsole();
        public Console()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            TextBox t = textBox1;
            cCs.Output(t.Text, 3, this);
            t.Clear();
        }

        private void Console_Load(object sender, EventArgs e)
        {
            list = listView1;
        }
        private void EnterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(this, new EventArgs());
            }
        }
    }
}
