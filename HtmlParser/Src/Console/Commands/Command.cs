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
        string args;
        string description;
        bool test = false;
        bool depricated = false;

        public string Name { get { return name; } }
        public string Signature { 
            get {
                string test = this.test ? "[TEST]" : "";
                string depricated = this.depricated ? "[DEPRICATED]" : "";
                string descr = description != "" ? $" - {description}" : "";
                return $"{test}{depricated}{name} {args}{descr}"; 
            } 
        }

        public Command(string name, string args = "Нет аргументов", string description = "", bool test = false, bool depricated = false)
        {
            this.name = name;
            this.description = description;
            if (args.Length > 0) this.args = args;
            this.test = test;
            this.depricated = depricated;
        }

        public abstract void Execute(string[] args);
    }
}
