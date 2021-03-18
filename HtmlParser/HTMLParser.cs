using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using System.Windows.Forms;

namespace HtmlParser
{
    public partial class HTMLParser : Form
    {
        public HTMLParser()
        {
            InitializeComponent();
        }

        private void Pars_Click(object sender, EventArgs e)
        {
            //CQ citilinkDom = CQ.CreateFromUrl("https://www.citilink.ru/product/videokarta-msi-nvidia-geforce-gtx-1660-gtx-1660-ventus-xs-6g-oc-6gb-gd-1133566/");
            //CQ citilinPriceNode = citilinkDom[".ProductHeaderprice-default_current-price"];
            //if (citilinPriceNode != null)
            //{
            //    int price = Convert.ToInt32(citilinPriceNode.Text().Trim().Replace(" ", ""));
            //    label1.Text = price.ToString();
            //}
            CQ avelotDom = CQ.CreateFromUrl("https://avelot.ru/videokarty/262003-videokarta-gigabyte-geforce-gtx1650-4gb-gddr5128bit3hdmidp-gv-n1650gaming-oc-4gdret.html?search_query=gtx1650&results=26");
            CQ avelotPriceNode = avelotDom["#our_price_display"];
            if (avelotPriceNode != null)
            {
                int price = Convert.ToInt32(avelotPriceNode.Attr("content"));
               // label2.Text = price.ToString();
            }
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ДобавитьТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.CreateMenu();
        }

        private void ИзменитьТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor.EditMenu();
        }
    }
}
