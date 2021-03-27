using System.Drawing;
using System.Windows.Forms;

namespace HtmlParser
{
    public class CConsole
    {
        static CConsole instance;
        static Console c;

        public static CConsole GetInstance()
        {
            if (instance == null)
            {
                instance = new CConsole(c);
            }

            return instance;
        }

        public CConsole LogSuccess(string str)
        {
            Output(str, 1);
            return instance;
        }

        public CConsole LogWarning(string str)
        {
            Output(str, 4);
            return instance;
        }

        public CConsole LogError(string str)
        {
            Output(str, 2);
            return instance;
        }

        public CConsole Log(string str)
        {
            Output(str, 3);
            return instance;
        }

        public CConsole(Console console)
        {
            c = console;
        }

        void Output(string str, int operation)
        {
            if (c != null && str != "")
            {
                ListViewItem i = new ListViewItem();
                i.Text = str;
                i.ForeColor = GetColorById(operation);
                c.list.Items.Add(i);
            }
        }
            
        Color GetColorById(int operation)
        {
            switch(operation)
            {
            case 1:
                return Color.Green;
            case 2:
                return Color.Red;
            case 3:
                return Color.LightGray;
            case 4:
                return Color.Yellow;
            default:
                return Color.White;
            }
        }
    }
}
