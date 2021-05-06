using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.WebApp.Controllers
{
    public class TwitterContextsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITwitterContextBusinessObject _twitterContextBusinessObject;

        public TwitterContextsController(ILogger<HomeController> logger, ITwitterContextBusinessObject twitterContextBusinessObject)
        {
            _logger = logger;
            _twitterContextBusinessObject = twitterContextBusinessObject;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var twitterContextsList = await _twitterContextBusinessObject.GetAllPocoAsync();
            if (!twitterContextsList.Success) return Error(twitterContextsList.Exception.Message);

            var contextsList = new List<TwitterContextViewModel>();
            foreach (var context in twitterContextsList.Result)
            {
                contextsList.Add(TwitterContextViewModel.FromData(context));
            }

            var orderedList = contextsList.OrderByDescending(i => i.Date);

            string logoUrl = Url.Content("~/img/twitterRobot.png");
            ViewData["Logo"] = logoUrl;

            return View(orderedList);
        }

        [HttpGet("TwitterContexts/Details/{id}")]
        public async Task<IActionResult> Details(Guid? contextId)
        {
            if (contextId == null) return NotFound();

            var detailedContext = await _twitterContextBusinessObject.GetPocoAsync((Guid)contextId);
            if (!detailedContext.Success) return Error(detailedContext.Exception.Message);

            if (detailedContext.Result == null) return NotFound();

            string logoUrl = Url.Content("~/img/twitterRobot.png");
            ViewData["Logo"] = logoUrl;

            return View(TwitterDetailsViewModel.FromData(detailedContext.Result));
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
