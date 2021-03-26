using System;
using CsQuery;

namespace HtmlParser
{
    public class AvelotShop : Shop
    {
        public AvelotShop(string link) : base("Avelot", link) { }

        public override int GetPrice()
        {
            try
            {
                CQ avelotDom = CQ.CreateFromUrl(this.link);
                CQ avelotPriceNode = avelotDom["#our_price_display"];
                if (avelotPriceNode.Length > 0)
                {
                    return Convert.ToInt32(avelotPriceNode.Attr("content"));
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