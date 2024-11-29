using Foody.DataAccess.Repository.IRepository;
using Foody.Models;
using Foody.utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace FoodyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> menuItemList = _unitOfWork.Product.GetAll(includeProperties:"Category");
            return View(menuItemList);
        }
        public IActionResult Details(int productId)
        {
          
            ShoppingCart cartObj = new ShoppingCart()
            {
                Product = _unitOfWork.Product.Get(c => c.Id == productId, includeProperties: "Category"),
                ProductId = productId
            };
            return View(cartObj);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
           
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        shoppingCart.ApplicationUserId = userID;
         
        ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == shoppingCart.ApplicationUserId && u.ProductId == shoppingCart.ProductId,null,false);

            if (cartFromDb == null)
        {
            _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();

        }
            else
        {
            cartFromDb.Count += shoppingCart.Count;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
            }
            int productCount = 0;

            foreach (var item in _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == shoppingCart.ApplicationUserId))
            {
                productCount += item.Count;
            }


            if (productCount > 0)
            {
                HttpContext.Session.SetInt32(SD.SessionCart, productCount);
            }
            TempData["success"] = "Item has been added to cart";
            return RedirectToAction("Index");   
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
        #region API CALLS

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        //    return Json(new { data = objProductList });
        //}

        [HttpGet]
        public IActionResult GetAll(string? categoryType)
        {


            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            if (categoryType != "All" && !string.IsNullOrEmpty(categoryType) )
            {
                objProductList =  objProductList.Where(u => u.Category.Name == categoryType).ToList();
            }
            return Json(new { data = objProductList });
        }
        #endregion
    }
}
