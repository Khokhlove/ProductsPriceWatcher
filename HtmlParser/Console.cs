using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HtmlParser.Src.Console.Commands;

namespace HtmlParser
{
    public partial class Console : Form
    {
        public ListView list;

        public CConsole cc = CConsole.GetInstance();
        public ConsoleCommands commands = ConsoleCommands.GetInstance();

        public Console()
        {
            InitializeComponent();
            list = listView1;
            InitConsoleCommands();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            TextBox tb = textBox1;
            string t = tb.Text;
            if (t.Length > 0)
            {
                commands.ExecuteCommand(t);
                tb.Clear();
            }
        }

        private void EnterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:
                    Button1_Click(this, new EventArgs());
                    break;
                case Keys.Up:
                    e.Handled = true;
                    string prevCommand = commands.GetPrevious();
                    textBox1.Text = prevCommand;
                    textBox1.SelectionStart = prevCommand.Length;
                    break;
                case Keys.Down:
                    e.Handled = true;
                    string nextCommand = commands.GetNext();
                    textBox1.Text = nextCommand;
                    textBox1.SelectionStart = nextCommand.Length;
                    break;
            }
        }

        private void Console_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        void InitConsoleCommands()
        {
            List<Command> cmnds = new List<Command>()
            {
                new Clear(this),
                new HtmlParser.Src.Console.Commands.Help(this),
                new Print()
            };

            commands.AddCommands(cmnds);
        }
    }
}
