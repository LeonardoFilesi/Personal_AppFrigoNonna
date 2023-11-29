using AppFrigoNonna.Database;
using AppFrigoNonna.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppFrigoNonna.Controllers
{
    public class FridgeProdController : Controller
    {
        private readonly FridgeProdContext _myDatabase;

        public FridgeProdController( FridgeProdContext db)
        {
            _myDatabase = db;
        }

        public IActionResult Index()
        {
            List<FridgeProd> fridgeProds = _myDatabase.FridgeProds.Include(fridgeProd => fridgeProd.Categories).ToList<FridgeProd>();

            return View("ProdIndex", fridgeProds);
        }



        //========================  CREATE  =========================
        [HttpGet]
        public IActionResult ProdCreate()
        {
            List<Category> categories = _myDatabase.Categories.ToList();

            // OPERAZIONE NECESSARIA per passare al nuovo FridgeProdFormModel solo le informazioni string Title e int Id delle istanze di Category
            List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
            List<Category> databaseAllCategories = _myDatabase.Categories.ToList();
            foreach (Category category in databaseAllCategories)
            {
                allCategoriesSelectList.Add(
                    new SelectListItem
                    {
                        Text = category.Name,
                        Value = category.Id.ToString()
                    });
            }

            FridgeProdFormModel model = new FridgeProdFormModel { FridgeProd = new FridgeProd(), Category = allCategoriesSelectList };

            return View("ProdCreate", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProdCreate(FridgeProdFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                List<Category> databaseAllCategories = _myDatabase.Categories.ToList();
                foreach (Category category in databaseAllCategories)
                {
                    allCategoriesSelectList.Add(new SelectListItem { Text = category.Name, Value = category.Id.ToString() });
                }
                data.Category = allCategoriesSelectList;

                return View("ProdCreate", data);
            }

            data.FridgeProd.Categories = new List<Category>();

            if (data.SelectedCategoryId != null)
            {
                foreach (string categorySelectedid in data.SelectedCategoryId)
                {
                    int intCategorySelectedId = int.Parse(categorySelectedid);

                    Category? categoryInDataBase = _myDatabase.Categories.Where(category => category.Id == intCategorySelectedId).FirstOrDefault();

                    if (categoryInDataBase != null)
                    {
                        data.FridgeProd.Categories.Add(categoryInDataBase);
                    }
                }
            }

            _myDatabase.FridgeProds.Add(data.FridgeProd);
            _myDatabase.SaveChanges();
            return RedirectToAction("Index");
        }


        //==========================  DELETE  ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            FridgeProd? fridgeProdToDelete = _myDatabase.FridgeProds.Where(fridgeProd => fridgeProd.Id == id).FirstOrDefault();

            if (fridgeProdToDelete != null)
            {
                _myDatabase.FridgeProds.Remove(fridgeProdToDelete);
                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("Il prodotto da eliminare non è stata trovata");
            }

        }


    }
}
