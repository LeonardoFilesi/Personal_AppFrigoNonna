using AppFrigoNonna.Database;
using AppFrigoNonna.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;

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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // List<FridgeProd> fridgeProds = _myDatabase.FridgeProds.Include(fridgeProd => fridgeProd.Categories).ToList<FridgeProd>();
            // vecchio codice 

            var allProds = _myDatabase.FridgeProds.ToList();

            var userProds = allProds.Where(p => p.OwnerId == userId).ToList();

            var viewModel = new ProdIndexViewModel
            {
                FridgeProds = _myDatabase.FridgeProds.Include(fridgeProd => fridgeProd.Categories).ToList(),
                AllProds = allProds,
                UserProds = userProds
            };

            // ViewBag.AllProds = allProds;
            // ViewBag.UserProds = userProds;
            // vecchio codice, ricorda "ViewBag" per il futuro

            return View("ProdIndex", viewModel);
            // CONTENUTO PARENTESI "ProdIndex", fridgeProds
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



        //==============   UPDATE   ==================
        [HttpGet]
        public IActionResult ProdUpdate(int id)
        {

            FridgeProd? fridgeProdToEdit = _myDatabase.FridgeProds.Where(fridgeProd => fridgeProd.Id == id).Include(fridgeProd => fridgeProd.Categories).FirstOrDefault();

            if (fridgeProdToEdit == null)
            {
                return NotFound("Il Prodotto non è stato trovato");
            }
            else
            {
                List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                List<Category> databaseAllCategories = _myDatabase.Categories.ToList();
                foreach (Category category in databaseAllCategories)
                {
                    allCategoriesSelectList.Add(new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name,
                        Selected = fridgeProdToEdit.Categories.Any(categoryAssociated => categoryAssociated.Id == category.Id)
                    });
                }

                FridgeProdFormModel model = new FridgeProdFormModel { FridgeProd = fridgeProdToEdit, Category = allCategoriesSelectList };
                return View("ProdUpdate", model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProdUpdate(int id, FridgeProdFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> allCategoriesSelectList = new List<SelectListItem>();
                List<Category> databaseAllCategories = _myDatabase.Categories.ToList();
                foreach (Category category in databaseAllCategories)
                {
                    allCategoriesSelectList.Add(new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name,
                    });
                }
                data.Category = allCategoriesSelectList;

                return View("ProdUpdate", data);
            }
            data.FridgeProd.Id = id;
            FridgeProd? fridgeProdToUpdate = _myDatabase.FridgeProds.Where(fridgeProd => fridgeProd.Id == id).Include(fridgeProd => fridgeProd.Categories).FirstOrDefault();

            if (fridgeProdToUpdate != null)
            {
                data.FridgeProd.Categories = new List<Category>();
                EntityEntry<FridgeProd> entryEntity = _myDatabase.Entry(fridgeProdToUpdate);

                if (data.SelectedCategoryId != null)
                {
                    foreach (string categorySelectedId in data.SelectedCategoryId)
                    {
                        int intCategorySelectedId = int.Parse(categorySelectedId);

                        Category? categoryInDataBase = _myDatabase.Categories.Where(category => category.Id == intCategorySelectedId).FirstOrDefault();

                        if (categoryInDataBase != null)
                        {
                            fridgeProdToUpdate.Categories.Add(categoryInDataBase);
                        }
                    }
                }

                // SetFridgeProdFileFromFormFile(data);

                entryEntity.CurrentValues.SetValues(data.FridgeProd);

                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("Mi spiace, non sono state trovati prodotti da aggiornare");
            }
        }



        /// <summary>
        /// PRENDERE contenuto delle immagini di ImageFormData e trasferirlo in ImgFile
        /// </summary>
        /// <param name="formData"></param>
        // private void SetFridgeProdFileFromFormFile(FridgeProdFormModel formData)
        // {
        //     if (formData.FridgeProdFormFile == null)
        //     {
        //         return;
        //     }
        // 
        //     MemoryStream stream = new MemoryStream();
        //     formData.FridgeProdFormFile.CopyTo(stream);
        //     formData.FridgeProdFormFile.ImgFile = stream.ToArray();
        // 
        // }


        //==========================  DELETE  ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProdDelete(int id)
        {

            FridgeProd? fridgeProdToDelete = _myDatabase.FridgeProds.Where(fridgeProd => fridgeProd.Id == id).FirstOrDefault();

            if (fridgeProdToDelete != null)
            {
                _myDatabase.FridgeProds.Remove(fridgeProdToDelete);
                _myDatabase.SaveChanges();

                return RedirectToAction("ProdIndex");
            }
            else
            {
                return NotFound("Il prodotto da eliminare non è stato trovato");
            }

        }


    }
}
