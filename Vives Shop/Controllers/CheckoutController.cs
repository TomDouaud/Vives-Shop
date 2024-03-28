using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vives_Shop.Core;
using Vives_Shop.Models;

namespace Vives_Shop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly VivesShopDbContext _vivesShopDbContext;
        private BuyingSession model;
        private List<Item> items = new List<Item>();

        public CheckoutController(VivesShopDbContext vivesShopDbContext)
        {
            items = vivesShopDbContext.Items.ToList();
            _vivesShopDbContext = vivesShopDbContext;
            model = BuyingSession.getInstance(items);
        }

        public IActionResult Index()
        {
            return View(this.model);
        }

        public IActionResult Return()
        {
            model.ItemsInCart.Clear();
            model.TotalPrice = 0;
            model.IsPaid = false;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Pay(string PayingMethod)
        {
            model.IsPaid = true;
            // Print the method of payement for debugging purposes
            Console.WriteLine("Ceci est un test" + PayingMethod);
            model.PayingMethod = PayingMethod;
            return RedirectToAction("Index", "Checkout");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
