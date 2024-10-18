using Foody.Models;
using Microsoft.AspNetCore.Mvc;

using Foody.DataAccess.Data;
using Foody.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Foody.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NuGet.Protocol.Plugins;

namespace FoodyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhostEnvironment;



        public ProductController(IUnitOfWork u, IWebHostEnvironment webHostEnvironment)

        {
            _unitOfWork = u;
            _webhostEnvironment = webHostEnvironment;
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
                string wwRootPath = _webhostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string production = Path.Combine(wwRootPath, "images", "product");

                    // Delete existing image file
                    if (!string.IsNullOrEmpty(obj.product.imageUrl))
                    {
                      
                        string existingImagePath = Path.Combine(wwRootPath, obj.product.imageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(existingImagePath))
                        {
                            System.IO.File.Delete(existingImagePath);
                        }
                    }

                    // Save new image file
                    if (Directory.Exists(production))
                    {
                        using (var fileStream = new FileStream(Path.Combine(production, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        obj.product.imageUrl = @"\images\product\" + fileName;
                    }
                }

                if (obj.product.Id == 0)
                {  
                    _unitOfWork.Product.Add(obj.product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product has been created successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(obj.product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product has been updated successfully";
                }

                return RedirectToAction("Index");
            }
            else
            { // Invalid
                obj.categoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                return View(obj);
            }
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
