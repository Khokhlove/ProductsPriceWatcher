using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HtmlParser
{
    public static class Editor
    {
        public static void CreateMenu()
        {
            ProductEditor editor = new ProductEditor();
            editor.ShowDialog();
        }
        public static void EditMenu()
        {
        }
    }
}


