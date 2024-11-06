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
            public IActionResult GetAll(string status , string paymentType)
            {
                var objOrderHeaders = _unitofwork.OrderHeader.GetAll(includeProperties: "ApplicationUser");

            switch (status)
            {
                case "pending":
                    objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusDelayed);
                    break;
                case "inprocess":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.statusProcessing);
                    break;
                case "completed":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.statusShipped);
                    break;
                case "approved":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.PaymentStatusApproved);
                    break;
                default:
                    break;
            }
            switch (paymentType)
            {
                case "Cash":
                    objOrderHeaders = objOrderHeaders.Where(u => u.PaymentMethod == SD.PaymentMethodCash);
                    break;
                case "Card":
                    objOrderHeaders = objOrderHeaders.Where(u => u.PaymentMethod == SD.PaymentMethodCard);
                    break;
            }


            return Json(new { data = objOrderHeaders.ToList() });
            }
        }
}
