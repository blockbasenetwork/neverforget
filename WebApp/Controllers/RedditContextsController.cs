using BlockBase.Dapps.NeverForget.Business.Interfaces;
using BlockBase.Dapps.NeverForget.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForget.WebApp.Controllers
{
    public class RedditContextsController : Controller
    {
        private readonly IRedditContextBusinessObject _redditContextBusinessObject;

        public RedditContextsController(IRedditContextBusinessObject redditContextBusinessObject)
        {
            _redditContextBusinessObject = redditContextBusinessObject;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOp = await _redditContextBusinessObject.GetAllPocoAsync();
            if (!listOp.Success) return View("Error", new ErrorViewModel() { RequestId = listOp.Exception.Message });
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
            if (!resultOp.Success) return View("Error", new ErrorViewModel() { RequestId = resultOp.Exception.Message });
            if (resultOp.Result == null) return NotFound();

            string logoUrl = Url.Content("~/img/redditRobot.png");
            ViewData["Logo"] = logoUrl;
            return View(RedditDetailsViewModel.FromData(resultOp.Result));
        }
    }
}
