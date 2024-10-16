using Foody.Models;
using Microsoft.AspNetCore.Mvc;

using Foody.DataAccess.Data;
using Foody.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Foody.Models.ViewModels;

namespace FoodyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;


        public ProductController(IUnitOfWork u)

        {
            _unitOfWork = u;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
           

            return View(objProductList);
        }


        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new ProductVM()
            {
                product = new Product(),
                categoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if(id == null || id == 0)   
            {
                //create
            return View(productVM);
            }
            else
            {
                // update
                productVM.product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

        }




        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)  
        {
            if (ModelState.IsValid)
            {
              
                _unitOfWork.Product.Add(obj.product);
                _unitOfWork.Save();
                TempData["success"] = "Product has been created successfully";

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
            Product ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product has been deleted successfully";
            return RedirectToAction("Index");


        }
    }
}
