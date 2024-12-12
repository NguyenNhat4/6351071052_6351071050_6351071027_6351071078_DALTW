using Foody.Models;
using Microsoft.AspNetCore.Mvc;

using Foody.DataAccess.Data;
using Foody.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Foody.utility;

namespace FoodyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class CategoryController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;


        public CategoryController(IUnitOfWork u)

        {
            _unitOfWork = u;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }


        public IActionResult Create()
        {

            return View();
        }

        public ActionResult Bieudoxe()
        {
            // Retrieve all categories from the database dynamically
            var categories = _unitOfWork.Category.GetAll().ToDictionary(c => c.Id, c => c.Name);

            var objCategoryList = _unitOfWork.Product.GetAll()
                .GroupBy(x => x.CategoryId != 0 && categories.ContainsKey(x.CategoryId)
                    ? categories[x.CategoryId]  // Use the actual category name from database
                    : "Uncategorized") // Use "Uncategorized" for products without a valid category
                .Select(g => new ThongKeXeViewModel
                {
                    TenLoaiXe = g.Key, // Category name
                    SoLuongXe = g.Count() // Number of products in the category
                })
                .OrderByDescending(x => x.SoLuongXe) // Optional: Sort by product count
                .ToList();

            // Additional insights
            ViewBag.TotalCategories = categories.Count;
            ViewBag.TotalProducts = _unitOfWork.Product.GetAll().Count();
            ViewBag.CategoriesWithNoProducts = categories.Keys
                .Where(catId => !_unitOfWork.Product.GetAll().Any(p => p.CategoryId == catId))
                .Select(catId => categories[catId])
                .ToList();

            return View(objCategoryList);
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category has been created successfully";

                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category has been updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category has been deleted successfully";
            return RedirectToAction("Index");


        }
    }
}
