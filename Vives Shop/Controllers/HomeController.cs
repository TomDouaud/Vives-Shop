using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vives_Shop.Core;
using Vives_Shop.Models;

namespace Vives_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly VivesShopDbContext _vivesShopDbContext;
        private BuyingSession model;
        private List<Item> items = new List<Item>();

        public HomeController(VivesShopDbContext vivesShopDbContext)
        {
            items = vivesShopDbContext.Items.ToList();
            _vivesShopDbContext = vivesShopDbContext;
            model = BuyingSession.getInstance(items);
        }

        public IActionResult Index()
        {

            return View(this.model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int itemID)
        {
            model.AddItem(itemID);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int itemID)
        {
            model.RemoveItem(itemID);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            return RedirectToAction("Index", "Checkout");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
