using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser.Src.Console.Commands
{
    class print_test : Command
    {
        public print_test() : base("print_test", "text: string") {}

        public override void Execute(string[] args)
        {
            CConsole c = CConsole.GetInstance();
            if (args.Length == 1)
            {
                string text = args[0].Trim();
                if (text != "")
                {
                    c.Log(text);
                }
                else
                {
                    c.LogError("Текст не может быть пустой строкой!");
                }
            }
            else
            {
                c.LogError("Задано неверное количество аргументов!");
            }
        }
    }
}
