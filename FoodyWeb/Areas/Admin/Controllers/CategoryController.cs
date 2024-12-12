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
            // Tạo một dictionary ánh xạ CategoryId với tên loại
            var categoryMapping = new Dictionary<int, string>
    {
        { 1, "Drinks" },
        { 2, "Foods" },
        { 3, "Addition Foods" },
        { 6, "Street food" },
        { 1005, "Fry foods" },
        { 1006, "Mukang" }
    };

            var objCategoryList = _unitOfWork.Product.GetAll()
                .GroupBy(x => (x.CategoryId != 0 && categoryMapping.ContainsKey(x.CategoryId))
                    ? categoryMapping[x.CategoryId]  // Lấy tên loại từ dictionary
                    : "Không có loại") // Nếu không có CategoryId hợp lệ, nhóm theo "Không có loại"
                .Select(g => new
                {
                    TenLoaiXe = g.Key, // Tên loại sản phẩm
                    SoLuongXe = g.Count() // Đếm số lượng sản phẩm trong nhóm
                })
                .ToList()
                .Select(x => new ThongKeXeViewModel
                {
                    TenLoaiXe = x.TenLoaiXe, // Hiển thị tên loại xe
                    SoLuongXe = x.SoLuongXe
                })
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
