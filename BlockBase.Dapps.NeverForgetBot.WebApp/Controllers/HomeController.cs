using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using BlockBase.Dapps.NeverForgetBot.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GeneralContextPoco _generalContextPoco;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            this._generalContextPoco = new GeneralContextPoco();
        }

        public IActionResult Index()
        {

            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error(string message)
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
        //}
    }
}
