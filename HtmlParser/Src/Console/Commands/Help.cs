using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HtmlParser.Src.Console.Commands
{
    class Help : Command
    {
        HtmlParser.Console console;
        public Help(HtmlParser.Console console) : base("help", description: "Показать перечень команд консоли.")
        {
            this.console = console;
        }

        public override void Execute(string[] args)
        {
            CConsole c = CConsole.GetInstance();
            if (console != null)
            {

                if (console.list != null)
                {
                    c.Log(" ");
                    c.Log("Доступные команды:");
                    ConsoleCommands.GetInstance().Commands.ForEach(command => c.Log(command.Signature));
                }
                else
                {
                    c.LogError("Не найден ListView!");
                }
            }
            else
            {
                c.LogError("Консоль не найдена!");
            }
        }
    }
}
