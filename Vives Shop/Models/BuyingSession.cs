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

        public void refreshItems(List<Item> items)
        {
            instance.Items = items;
        }

        public void AddItem(string itemName)
        {
            ItemsInCart.Add(Items.FirstOrDefault(i => i.Name == itemName));
            TotalPrice += Items.FirstOrDefault(i => i.Name == itemName).Price;
        }

        public void RemoveItem(string itemName)
        {
           // int itemToRemove = Items.First(i => i.Name == itemName).Id;
            ItemsInCart.Remove(ItemsInCart.First(i => i.Name == itemName));
            TotalPrice -= Math.Round(Items.FirstOrDefault(i => i.Name == itemName).Price, 1);
        }

    }
}
