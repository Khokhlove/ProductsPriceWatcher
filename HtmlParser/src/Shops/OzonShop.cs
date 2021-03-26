using System;
using CsQuery;

namespace HtmlParser
{
    public class OzonShop : Shop
    {
        public OzonShop(string link) : base("Ozon", link) { }

        public override int GetPrice()
        {
            CQ avelotDom = CQ.CreateFromUrl(this.link);
            CQ avelotPriceNode = avelotDom[".c2h5.c2h6"];

            if (avelotPriceNode.Length > 0)
            {
                CQ str = avelotPriceNode.Find("span");
                if (str.Length > 0)
                {
                    return Convert.ToInt32(str.Text().Replace(" ", "").Replace("₽", ""));
                }
                return -1;
            }
            

            return -1;
        }
    }
}