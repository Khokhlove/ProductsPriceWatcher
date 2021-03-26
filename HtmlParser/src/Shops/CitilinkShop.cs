using System;
using CsQuery;

namespace HtmlParser
{
    public class CitilinkShop : Shop
    {
        public CitilinkShop(string link) : base("Citilink", link) { }

        public override int GetPrice()
        {
            try
            {
                CQ dom = CQ.CreateFromUrl(this.link);
                CQ priceNode = dom[".ProductHeader__price-default_current-price"];
                if (priceNode.Length > 0)
                {
                    return Convert.ToInt32(priceNode.Text().Trim().Replace(" ", ""));
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
