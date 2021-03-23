using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser
{
    public abstract class Shop
    {
        public string link;
        public string shopName;

        public Shop(string shopName, string link)
        {
            this.link = link;
            this.shopName = shopName;
        }

        public abstract int GetPrice();

        public static Shop GetShop(string[] shop)
        {
            string shopName = shop[2].Trim().ToLower();
            string url = shop[3];

            switch (shopName)
            {
                case "citilink":
                    return new CitilinkShop(url);
                case "avelot":
                    return new AvelotShop(url);
                default:
                    return null;
            }
        }
    }
}
