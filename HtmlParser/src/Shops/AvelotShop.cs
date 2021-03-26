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
                CQ dom = CQ.CreateFromUrl(this.link);
                CQ priceNode = dom["#our_price_display"];
                if (priceNode.Length > 0)
                {
                    return Convert.ToInt32(priceNode.Attr("content"));
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