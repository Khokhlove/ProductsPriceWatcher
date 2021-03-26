using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HtmlParser
{
    public class CConsole
    {

            public void Output(string str, int operation, Console cs)
            {
                if (cs != null && str != "" && str != null)
                {
                    ListViewItem i = new ListViewItem();
                    i.Text = str;
                    i.ForeColor = GetColorById(operation);
                    cs.list.Items.Add(i);
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
                default:
                    return Color.White;
                }
            }
    }
}
