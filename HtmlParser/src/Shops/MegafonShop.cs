using System;
using CsQuery;
using System.Text.RegularExpressions;
namespace HtmlParser
{
    public class MegafonShop : Shop
    {
        public MegafonShop(string link) : base("Megafon", link) { }

        public override int GetPrice()
        {
            try
            {
                CQ Dom = CQ.CreateFromUrl(this.link);
                CQ priceNode = Dom["meta[itemprop='price']"];
                if (priceNode.Length > 0)
                {
                    string s = priceNode.Attr("content");
                    if (s != "")
                        return Convert.ToInt32(s);
                    else
                        return -1;
                }

                return -1;

            }
            catch(Exception e)
            {
                CConsole.GetInstance().LogError(e.Message);
                return -1;
            }
        }
    }
}
