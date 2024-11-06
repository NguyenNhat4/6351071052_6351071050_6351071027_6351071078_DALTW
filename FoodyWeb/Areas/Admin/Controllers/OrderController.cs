using Foody.DataAccess.Repository;
using Foody.DataAccess.Repository.IRepository;
using Foody.Models;
using Foody.utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodyWeb.Areas.Admin.Controllers
{
        [Area("Admin")]
        [Authorize(Roles = SD.Role_Admin)]
    
    public class OrderController : Controller
    {
            private readonly IUnitOfWork _unitofwork;

            public OrderController(IUnitOfWork unitofwork)
            {
                _unitofwork = unitofwork;
            }

            public IActionResult Index()
            {


                return View();
            }


            [HttpGet]
            public IActionResult GetAll()

            {
                List<OrderHeader> orderHeaders = _unitofwork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
                return Json(new { data = orderHeaders });
            }
        }
}
