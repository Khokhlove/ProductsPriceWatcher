using System.Windows.Forms;

namespace HtmlParser.Src.Console.Commands
{
    class Clear : Command
    {
        HtmlParser.Console console;
        public Clear(HtmlParser.Console console) : base("clear", description: "Очистка консоли.")
        {
            this.console = console;
        }

        public override void Execute(string[] args)
        {
            CConsole c = CConsole.GetInstance();
            if (console != null)
            {
                ListView view = console.list;
                if (view != null)
                {
                    if (view.Items.Count > 0)
                    {
                        view.Items.Clear();
                        c.LogSuccess("Консоль очищена!");
                    }
                    else
                    {
                        c.Log("Консоль пуста.");
                    }
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
