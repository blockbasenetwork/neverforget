﻿using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Controllers
{
    public class TwitterContextsController : Controller
    {
        private readonly ITwitterContextBo _twitterContextBo;

        public TwitterContextsController(ITwitterContextBo twitterContextBo)
        {
            _twitterContextBo = twitterContextBo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var listOp = await _twitterContextBo.GetAllPocoAsync();
            //if (!listOp.Success) return View("Error", new ErrorViewModel() { RequestId = listOp.Exception.Message });
            //var list = new List<TwitterContextViewModel>();
            //foreach (var item in listOp.Result)
            //{
            //    list.Add(TwitterContextViewModel.FromData(item));
            //}
            return View();
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var listOp = await _twitterContextBo.GetPocoAsync(id);
        //    if (!listOp.Success) return View("Error", new ErrorViewModel() { RequestId = listOp.Exception.Message });

        //    var list = new List<TwitterContextViewModel>();
        //    listOp.Result.Comments.OrderByDescending(c => c.CommentDate).ToList();

        //    foreach (var item in listOp.Result.Comments)
        //    {
        //        list.Add(TwitterContextViewModel.FromData(item));
        //    }


        //    return View(listOp);
        //}

        [HttpGet("TwitterContexts/Details/{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var resultOp = await _twitterContextBo.GetPocoAsync((Guid)id);
            if (!resultOp.Success) return View("Error", new ErrorViewModel() { RequestId = resultOp.Exception.Message });
            if (resultOp.Result == null) return NotFound();

            return View(TwitterDetailsViewModel.FromData(resultOp.Result));
        }
    }
}
