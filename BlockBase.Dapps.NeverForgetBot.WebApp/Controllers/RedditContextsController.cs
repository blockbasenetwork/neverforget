using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Controllers
{
    public class RedditContextsController : Controller
    {
        private readonly IRedditContextBo _redditContextBo;

        public RedditContextsController(IRedditContextBo redditContextBo)
        {
            _redditContextBo = redditContextBo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var listOp = await _redditContextBo.GetAllPocoAsync();
            //if (!listOp.Success) return View("Error", new ErrorViewModel() { RequestId = listOp.Exception.Message });
            //var list = new List<RedditContextViewModel>();
            //foreach (var item in listOp.Result)
            //{
            //    list.Add(RedditContextViewModel.FromData(item));
            //}
            return View();
        }

        [HttpGet("RedditContexts/Details/{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var resultOp = await _redditContextBo.GetPocoAsync((Guid)id);
            if (!resultOp.Success) return View("Error", new ErrorViewModel() { RequestId = resultOp.Exception.Message });
            if (resultOp.Result == null) return NotFound();

            return View(RedditDetailsViewModel.FromData(resultOp.Result));
        }
    }
}
