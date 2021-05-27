using LoginMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoginMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> LoginUser(UserInfo user)
        {
            using(var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using(var response = await httpClient.PostAsync("https://localhost:44396/api/Token",content))
                {
                    string token = await response.Content.ReadAsStringAsync();
                    if(token =="Invalid Credentials")
                    {
                        return Redirect("~/Home/Index");

                    }
                    else
                    {
                        HttpContext.Session.SetString("JWToken", token);
                        return Redirect("~/Dashboard/Index");
                    }
                }
              
            }
        }

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Home/Index");
        }
        public IActionResult Index()
        {
            return View();
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
    }

    public class UserDefinedException : Exception
    {
        public UserDefinedException(string Message) : base(Message)
        {

        }
    }
}
