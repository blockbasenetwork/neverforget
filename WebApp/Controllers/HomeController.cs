using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGeneralContextBusinessObject _generalContextBusinessObject;

        public HomeController(ILogger<HomeController> logger, IGeneralContextBusinessObject generalContextBusinessObject)
        {
            _logger = logger;
            _generalContextBusinessObject = generalContextBusinessObject;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recentCallsList = await _generalContextBusinessObject.GetRecentCalls();
            if (!recentCallsList.Success) return Error(recentCallsList.Exception.Message);

            var recentList = new List<GeneralContextViewModel>();
            foreach (var call in recentCallsList.Result)
            {
                recentList.Add(GeneralContextViewModel.FromData(call));
            }

            string logoUrl = Url.Content("~/img/Logo.png");
            ViewData["Logo"] = logoUrl;

            return View(recentList);
        }

        public IActionResult Error(string message)
        {
            _logger.LogError(message);
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return Error(viewModel.Message);
        }
    }
}
