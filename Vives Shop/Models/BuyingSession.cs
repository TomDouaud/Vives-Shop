using Microsoft.AspNetCore.Mvc;
using Vives_Shop.Core;

namespace Vives_Shop.Models
{
    public class BuyingSession
    {

        private static BuyingSession? instance;

        public List<Item> Items { get; set; }
        public List<Item> ItemsInCart { get; set; }
        public double TotalPrice { get; set; }
        public string PayingMethod { get; set; }
        public bool IsPaid { get; set; } = false;

        private BuyingSession(List<Item> items)
        {
            Items = items;
            ItemsInCart = new List<Item>();
            TotalPrice = 0;
            PayingMethod = "";
        }

        public static BuyingSession getInstance(List<Item> items)
        {
            if (instance == null)
            {
                return instance = new BuyingSession(items);
            }
            else
            {
                return instance;
            }
        }

        public void AddItem(int itemID)
        {
            ItemsInCart.Add(Items[itemID -1]);
            TotalPrice += Items[itemID -1].Price;
        }

        public void RemoveItem(int itemID)
        {
            ItemsInCart.Remove(Items[itemID - 1]);
            TotalPrice -= Items[itemID - 1].Price;
        }

    }
}
