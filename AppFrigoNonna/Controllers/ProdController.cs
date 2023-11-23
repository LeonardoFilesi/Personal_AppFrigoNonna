using AppFrigoNonna.Database;
using AppFrigoNonna.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppFrigoNonna.Controllers
{
    public class ProdController : Controller
    {
        private readonly FridgeProdContext _myDatabase;

        public ProdController( FridgeProdContext db)
        {
            _myDatabase = db;
        }

        public IActionResult Index()
        {
            List<FridgeProd> fridgeProds = _myDatabase.FridgeProds.Include(fridgeProd => fridgeProd.Categories).ToList<FridgeProd>();

            return View("ProdIndex", fridgeProds);
        }
    }
}
