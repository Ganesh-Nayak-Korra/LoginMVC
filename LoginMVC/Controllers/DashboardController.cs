using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginMVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            return View();
        }
    }
}
