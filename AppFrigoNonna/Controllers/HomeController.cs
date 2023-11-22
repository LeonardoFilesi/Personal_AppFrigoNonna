using AppFrigoNonna.Database;
using AppFrigoNonna.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppFrigoNonna.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FridgeProdContext _myDatabase;


        public HomeController(ILogger<HomeController> logger, FridgeProdContext db)
        {
            _myDatabase = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<FridgeProd> fridgeProds = _myDatabase.FridgeProds.Include(fridgeProd => fridgeProd.Categories).ToList<FridgeProd>();

            return View("Index", fridgeProds);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}