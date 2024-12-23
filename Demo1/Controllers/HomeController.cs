using Demo1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache distributedCache;

        public HomeController(ILogger<HomeController> logger,IDistributedCache distributedCache)
        {
            _logger = logger;
            this.distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("key", Guid.NewGuid().ToString());
           // distributedCache.SetString("data", Guid.NewGuid().ToString());
            return View();
        }

        public IActionResult Privacy()
        {
            string data = HttpContext.Session.GetString("key");
            return Content(data);
        }

        public IActionResult Test()
        {
            HttpContext.Session.SetString("k1", Guid.NewGuid().ToString());
            //distributedCache.SetString("data", "00000000");
            //string data= distributedCache.GetString("data");
            return View();
        }

        public IActionResult TestR()
        {
            return Content(HttpContext.Session.GetString("k1"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
