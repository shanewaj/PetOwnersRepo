using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PetOwnerDatabase.Helper;
using PetOwnerDatabase.Models;

namespace PetOwnerDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<AppSettings> config;

        public HomeController(IOptions<AppSettings> _config)
        {
            this.config = _config;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [Authorize]
        public IActionResult TestAPI()
        {
            try
            {
                var list = HttpClientHelper.getPetsViewModel(getUrl());
                if (list != null || list.Count() > 0)
                {
                    ViewBag.Massage = "Read data successfully from Web API";
                    return View(list);
                }
                else
                {
                    ViewBag.Massage = "Failed to Read Data from Web API";
                }
            }
            catch (Exception ex)
            {
                //log issues
            }

            return View();
        }

        private string getUrl()
        {
            var url = config != null ? config.Value != null ? config.Value.ApiUrl.ToString() : "" : "";

            return url;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
