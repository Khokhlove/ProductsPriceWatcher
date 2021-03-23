using System;
using CsQuery;

namespace HtmlParser
{
    public class CitilinkShop : Shop
    {
        public CitilinkShop(string link) : base("Citilink", link) { }

        public override int GetPrice()
        {
            CQ citilinkDom = CQ.CreateFromUrl(this.link);
            CQ citilinPriceNode = citilinkDom[".ProductHeader__price-default_current-price"];
            if (citilinPriceNode.Length > 0)
            {
                return Convert.ToInt32(citilinPriceNode.Text().Trim().Replace(" ", ""));
            }

            return -1;
        }
    }
}
