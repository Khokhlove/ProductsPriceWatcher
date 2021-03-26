using System;
using CsQuery;
using System.Text.RegularExpressions;

namespace HtmlParser
{
    public class MvideoShop : Shop
    {
        public MvideoShop(string link) : base("Mvideo", link) { }

        public override int GetPrice()
        {
            CQ dom = CQ.CreateFromUrl(this.link);
            CQ priceNode = dom[".fl-pdp-price__current"];
            if (priceNode.Length > 0)
            {
                string s = "";
                MatchCollection matches = Regex.Matches(priceNode.Text(), @"[\d]+");
                foreach (Match m in matches)
                {
                    s += m.Value;
                }
                if (s != "")
                    return Convert.ToInt32(s);
                else
                    return -1;
            }
            return -1;
        }
    }
}
