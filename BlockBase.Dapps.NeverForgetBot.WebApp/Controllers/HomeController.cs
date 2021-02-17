using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGeneralContextBo _generalContextBo;


        public HomeController(ILogger<HomeController> logger, IGeneralContextBo generalContextBo)
        {
            _logger = logger;
            _generalContextBo = generalContextBo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOp = await _generalContextBo.GetRecentCalls();
            if (!listOp.Success) return View("Error", new ErrorViewModel() { RequestId = listOp.Exception.Message });
            var list = new List<GeneralContextViewModel>();
            foreach (var item in listOp.Result)
            {
                list.Add(GeneralContextViewModel.FromData(item));
            }
            return View(list);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
