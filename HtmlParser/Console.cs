using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HtmlParser.Src.Console.Commands;

namespace HtmlParser
{
    public partial class Console : Form
    {
        public ListView list;
        public List<Command> commands;

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
                ExecuteCommand(t);
                PreviousCommands.Add(t);
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
                    string prevCommand = PreviousCommands.GetPrevious();
                    textBox1.Text = prevCommand;
                    break;
                case Keys.Down:
                    e.Handled = true;
                    string nextCommand = PreviousCommands.GetNext();
                    textBox1.Text = nextCommand;
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
            commands = new List<Command>()
            {
                new Clear(this),
                new HtmlParser.Src.Console.Commands.Help(this),
                new print_test()
            };
        }

        void ExecuteCommand(string commandString)
        {
            string[] strs = commandString.Split(' ');
            if (strs.Length > 0)
            {
                string name = strs[0];
                if (name.Length > 0)
                {
                    Command command = commands.Find(p => p.Name == name);
                    if (command != null)
                    {
                        if (command != null)
                        {
                            string[] args = new string[] { };
                            if (strs.Length > 1)
                            {
                                args = new string[strs.Length - 1];
                                for (int i = 1; i < strs.Length; i++)
                                {
                                    args[i - 1] = strs[i];
                                }
                            }

                            command.Execute(args);
                        }
                        else
                        {
                            CConsole.GetInstance().LogError($"Команда '{name}' равна null!");
                        }
                    }
                    else
                    {
                        CConsole.GetInstance().LogError($"Команда '{name}' не найдена! Введите help, чтобы получить перечень доступных команд.");
                    }
                }
                else
                {
                    CConsole.GetInstance().LogError("Название команды не опознано!");
                }
            }
            else
            {
                CConsole.GetInstance().LogError("Нет аргументов!");
            }
        }
    }
}
