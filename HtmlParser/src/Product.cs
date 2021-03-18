using System.Collections.Generic;

namespace HtmlParser
{
    public class Product
    {
        public string name;
        List<Shop> shops = new List<Shop>();
        public List<Shop> Shops { get { return this.shops; } }

        public Product(string name)
        {
            this.name = name;
        }

        public void AddShop(Shop shop)
        {
            this.shops.Add(shop);
        }

        public void AddShops(List<Shop> shops)
        {
            this.shops.AddRange(shops);
        }
    }
}
