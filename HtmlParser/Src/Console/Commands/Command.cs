using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser.Src.Console.Commands
{
    public abstract class Command
    {
        string name = "[Команда, которой не присвоили имя:D]";
        string args = "Нет аргументов";

        public string Name { get { return name; } }
        public string Signature { get { return $"{name} {args}"; } }

        public Command(string name, string args)
        {
            this.name = name;
            if (args.Length > 0) this.args = args;
        }

        public abstract void Execute(string[] args);
    }
}
