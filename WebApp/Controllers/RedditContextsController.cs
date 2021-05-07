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
    public class RedditContextsController : Controller
    {
        private readonly ILogger<RedditContextsController> _logger;
        private readonly IRedditContextBusinessObject _redditContextBusinessObject;

        public RedditContextsController(ILogger<RedditContextsController> logger, IRedditContextBusinessObject redditContextBusinessObject)
        {
            _logger = logger;
            _redditContextBusinessObject = redditContextBusinessObject;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOp = await _redditContextBusinessObject.GetAllPocoAsync();
            if (!listOp.Success) return Error(listOp.Exception.Message);

            var list = new List<RedditContextViewModel>();
            foreach (var item in listOp.Result)
            {
                list.Add(RedditContextViewModel.FromData(item));
            }

            var orderedList = list.OrderByDescending(i => i.Date);

            string logoUrl = Url.Content("~/img/redditRobot.png");
            ViewData["Logo"] = logoUrl;

            return View(orderedList);
        }

        [HttpGet("RedditContexts/Details/{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var resultOp = await _redditContextBusinessObject.GetPocoAsync((Guid)id);
            if (!resultOp.Success) return Error(resultOp.Exception.Message);

            if (resultOp.Result == null) return NotFound();

            string logoUrl = Url.Content("~/img/redditRobot.png");
            ViewData["Logo"] = logoUrl;

            return View(RedditDetailsViewModel.FromData(resultOp.Result));
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
