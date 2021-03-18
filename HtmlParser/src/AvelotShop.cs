using System;
using CsQuery;

namespace HtmlParser
{
    public class AvelotShop : Shop
    {
        public AvelotShop(string link) : base("Авелот", link) { }

        public override int GetPrice()
        {
            CQ avelotDom = CQ.CreateFromUrl(this.link);
            IDomElement el = avelotDom.Document.QuerySelector("#our_price_display");
            CQ avelotPriceNode = avelotDom["#our_price_display"];
            if (avelotPriceNode.Length > 0)
            {
                return Convert.ToInt32(avelotPriceNode.Attr("content"));
            }

            return -1;
        }
    }
}