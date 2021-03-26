using System;
using CsQuery;

namespace HtmlParser
{
    public class OzonShop : Shop
    {
        public OzonShop(string link) : base("Ozon", link) { }

        public override int GetPrice()
        {
            //try
            //{
            //    CsQuery.Web.ServerConfig s = new CsQuery.Web.ServerConfig();
            //    s.TimeoutSeconds = 20;
            //    s.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 89.0.4389.90 Safari / 537.36";
            //    CQ avelotDom = CQ.CreateFromUrl(this.link,s);
            //    CQ avelotPriceNode = avelotDom[".c2h5.c2h6"];

            //    if (avelotPriceNode.Length > 0)
            //    {
            //        CQ str = avelotPriceNode.Find("span");
            //        if (str.Length > 0)s
            //        {
            //            return Convert.ToInt32(str.Text().Replace(" ", "").Replace("₽", ""));
            //        }
            //        return -1;
            //    }


            //    return -1;
            //}
            //catch (Exception e)
            //{
            //    CConsole.GetInstance().LogError(e.Message);
            //    return -1;
            //}

            // Думаем над озоном
            return -1;
        }
    }
}