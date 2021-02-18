using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            var listOp = await _redditContextBo.GetAllPocoAsync();
            if (!listOp.Success) return View("Error", new ErrorViewModel() { RequestId = listOp.Exception.Message });
            var list = new List<RedditContextViewModel>();
            foreach (var item in listOp.Result)
            {
                list.Add(RedditContextViewModel.FromData(item));
            }
            return View(list);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var op = await _redditContextBo.GetPocoAsync((Guid)id);
            if (!op.Success) return View("Error", new ErrorViewModel() { RequestId = op.Exception.Message });
            if (op.Result == null) return NotFound();
            var rcVm = RedditContextViewModel.FromData(op.Result);
            return View(rcVm);
        }
    }
}
