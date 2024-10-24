using Foody.DataAccess.Repository.IRepository;
using Foody.DataAccess.Data;
using Foody.Models;
using Foody.Models.ViewModels;
using Foody.utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
namespace FoodyWeb.Areas.Admin.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public IActionResult Index()
        {
           
            return View();
        }
    }
}
