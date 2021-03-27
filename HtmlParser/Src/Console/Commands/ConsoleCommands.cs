using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser.Src.Console.Commands
{
    public class ConsoleCommands
    {
        int currCommandInd = 0;
        List<Command> commands = new List<Command>();
        List<string> previous = new List<string>();

        public List<Command> Commands { get { return commands.ToList(); } }

        static ConsoleCommands instance;

        public static ConsoleCommands GetInstance()
        {
            if (instance == null)
            {
                instance = new ConsoleCommands();
            }

            return instance;
        }

        public string GetPrevious()
        {
            if (previous.Count > 0)
            {
                currCommandInd = currCommandInd - 1 < 0
                ? previous.Count - 1
                : currCommandInd - 1;

                return previous[currCommandInd];
            }

            return "";
        }

        public string GetNext()
        {
            if (previous.Count > 0)
            {
                currCommandInd = currCommandInd + 1 >= previous.Count
                ? 0
                : currCommandInd + 1;

                return previous[currCommandInd];
            }

            return "";
        }

        public void AddCommand(Command command)
        {
            commands.Add(command);
        }

        public void AddCommands(List<Command> commands)
        {
            this.commands.AddRange(commands);
        }

        void AddPrevious(string command)
        {
            previous.Add(command);
            currCommandInd = previous.Count - 1;
        }

        public void ExecuteCommand(string commandString)
        {
            AddPrevious(commandString);
            CConsole cc = CConsole.GetInstance();
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
                            cc.LogError($"Команда '{name}' равна null!");
                        }
                    }
                    else
                    {
                        cc.LogError($"Команда '{name}' не найдена! Введите help, чтобы получить перечень доступных команд.");
                    }
                }
                else
                {
                    cc.LogError("Название команды не опознано!");
                }
            }
            else
            {
                cc.LogError("Нет аргументов!");
            }
        }
    }
}
