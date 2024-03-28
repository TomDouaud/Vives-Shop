using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using Vives_Shop.Core;
using Vives_Shop.Models;

namespace Vives_Shop.Controllers
{
    public class ItemManagerController : Controller
    {
        private readonly VivesShopDbContext _vivesShopDbContext;
        private BuyingSession model;
        private List<Item> items = new List<Item>();

        public ItemManagerController(VivesShopDbContext vivesShopDbContext)
        {
            items = vivesShopDbContext.Items.ToList();
            _vivesShopDbContext = vivesShopDbContext;
            model = BuyingSession.getInstance(items);
        }

        public IActionResult Index()
        {
            return View(this.model);
        }

        [HttpPost]
        public IActionResult AddItem(string name, string price)
        {
            // Convert the string price to a double with 2 decimals
            double curatedPrice = Math.Round(Double.Parse(price, CultureInfo.InvariantCulture), 2);
            // Add the item to the database
            _vivesShopDbContext.Items.Add(new Item { Name = name, Price = curatedPrice });
            // Save the changes
            _vivesShopDbContext.SaveChanges();
            // Refresh the items list
            model.refreshItems(_vivesShopDbContext.Items.ToList());
            return RedirectToAction("Index", "ItemManager");
        }

        [HttpPost]
        public IActionResult RedirectToEdit(string itemName)
        {
            // Redirect to the edit item page with the selected item
            return RedirectToAction("Edit", "ItemManager", new { itemName = itemName });
        }

        public IActionResult Edit(string itemName)
        {
            // Find the item in the database
            Item itemToEdit = _vivesShopDbContext.Items.FirstOrDefault(i => i.Name == itemName);
            // Return the edit item view with the selected item
            return View(itemToEdit);
        }

        [HttpPost]
        public IActionResult ConfirmChanges(string itemName, string price, string oldName)
        {
            // Convert the string price to a double with 2 decimals
            double curatedPrice = Math.Round(Double.Parse(price, CultureInfo.InvariantCulture), 2);
            // Find the item in the database
            Item itemToEdit = _vivesShopDbContext.Items.First(i => i.Name == oldName);
            // Edit the item in the database
            itemToEdit.Name = itemName;
            itemToEdit.Price = curatedPrice;
            // Save the changes
            _vivesShopDbContext.SaveChanges();
            // Refresh the items list
            model.refreshItems(_vivesShopDbContext.Items.ToList());
            return RedirectToAction("Index", "ItemManager");
        }

        [HttpPost]
        public IActionResult RemoveItem(string itemName)
        {
            // Log the item id
            Console.WriteLine(itemName);
            // Find the item in the database
            Item itemToRemove = _vivesShopDbContext.Items.FirstOrDefault(i => i.Name == itemName);
            // Remove the item from the database
            _vivesShopDbContext.Items.Remove(itemToRemove);

            // Save the changes
            _vivesShopDbContext.SaveChanges();
            // Refresh the items list
            model.refreshItems(_vivesShopDbContext.Items.ToList());
            return RedirectToAction("Index", "ItemManager");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
