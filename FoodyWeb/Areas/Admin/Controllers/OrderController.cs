using Foody.DataAccess.Repository;
using Foody.DataAccess.Repository.IRepository;
using Foody.Models;
using Foody.Models.ViewModels;
using Foody.utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;

namespace FoodyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class OrderController : Controller
    {
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        private readonly IUnitOfWork _unitofwork;

        public OrderController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {


            return View();
        }
        public IActionResult Details(int orderId)
        {
            OrderVM orderVM = new()
            {
                OrderHeader = _unitofwork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetails = _unitofwork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };

            return View(orderVM);
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderFromDb = _unitofwork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDb.Phonenumber = OrderVM.OrderHeader.Phonenumber;
            orderHeaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFromDb.City = OrderVM.OrderHeader.City;
        
            _unitofwork.OrderHeader.Update(orderHeaderFromDb);
            _unitofwork.Save();
            TempData["Success"] = "Order Details Updated Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromDb.Id });
        }


        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult StartProcessing()
        {
            _unitofwork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.statusProcessing);
            _unitofwork.Save();
            TempData["Success"] = "Order Details Updated Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult ShipOrder()
        {

            var orderHeader = _unitofwork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeader.OrderStatus = SD.statusShipped;

            orderHeader.ShippingDate = DateTime.Now;
            if (orderHeader.PaymentStatus == SD.PaymentStatusDelayed)
            {
                orderHeader.PaymentDueDate = DateTime.Now.AddDays(1).Date;
            }

            _unitofwork.OrderHeader.Update(orderHeader);
            _unitofwork.Save();
            TempData["Success"] = "Order Served Successfully.";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }
    



        [ActionName("Details")]
        [HttpPost]
        public IActionResult Details_PAY_NOW()
        {
            OrderVM.OrderHeader = _unitofwork.OrderHeader
                .Get(u => u.Id == OrderVM.OrderHeader.Id, includeProperties: "ApplicationUser");
            OrderVM.OrderDetails = _unitofwork.OrderDetail
                .GetAll(u => u.OrderHeaderId == OrderVM.OrderHeader.Id, includeProperties: "Product");

            //stripe logic
            var domain = "https://localhost:7169/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"admin/order/PaymentConfirmation?orderHeaderId={OrderVM.OrderHeader.Id}",
                CancelUrl = domain + $"admin/order/details?orderId={OrderVM.OrderHeader.Id}",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach (var item in OrderVM.OrderDetails)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "Vnd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }


            var service = new SessionService();
            Session session = service.Create(options);
            _unitofwork.OrderHeader.UpdateStripePaymentID(OrderVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitofwork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
   

        public IActionResult PaymentConfirmation()
        {
            OrderHeader orderHeader = _unitofwork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            
            string paymentStatus = orderHeader.PaymentStatus;
            bool payCash = (orderHeader.PaymentMethod == SD.PaymentMethodCash);
            bool isOrderServed = (orderHeader.OrderStatus == SD.statusShipped);

            if (!payCash || !isOrderServed) return RedirectToAction(nameof(Details), new { orderId = orderHeader.Id });


            orderHeader.PaymentStatus = SD.PaymentStatusApproved;
            _unitofwork.OrderHeader.Update(orderHeader);
            _unitofwork.Save();

            TempData["Success"] = "Order payment confirmed successfully!";

            return RedirectToAction(nameof(Index));

        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status, string paymentType)
        {
            var objOrderHeaders = _unitofwork.OrderHeader.GetAll(includeProperties: "ApplicationUser");

            switch (status)
            {
                case "paymentPending":
                    objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusPending);
                    break;
                case "inprocess":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.statusProcessing);
                    break;
                case "completed":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.statusShipped);
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
                    
                default:
                    break;
            }


            return Json(new { data = objOrderHeaders.ToList() });
        }
        #endregion
    }
}

