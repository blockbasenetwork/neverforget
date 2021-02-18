using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Controllers
{
    public class RedditDetailsController : Controller
    {
        private readonly IRedditContextBo _redditContextBo;

        public RedditDetailsController(IRedditContextBo redditContextBo)
        {
            _redditContextBo = redditContextBo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var op = await _redditContextBo.GetPocoAsync(id);
            if (!op.Success) return View("Error", new ErrorViewModel() { RequestId = op.Exception.Message });
            var result = RedditContextViewModel.FromData(op.Result);
            return View(result);
        }
    }
}
