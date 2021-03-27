using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser.Src.Console.Commands
{
    public static class PreviousCommands
    {
        static int currCommandInd = 0;
        static List<string> previous = new List<string>();

        public static string GetPrevious()
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

        public static string GetNext()
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

        public static void Add(string command)
        {
            previous.Add(command);
            currCommandInd = previous.Count - 1;
        }
    }
}
