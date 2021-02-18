using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Controllers
{
    public class TwitterDetailsController : Controller
    {
        private readonly ITwitterContextBo _twitterContextBo;

        public TwitterDetailsController(ITwitterContextBo twitterContextBo)
        {
            _twitterContextBo = twitterContextBo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var op = await _twitterContextBo.GetPocoAsync(id);
            if (!op.Success) return View("Error", new ErrorViewModel() { RequestId = op.Exception.Message });
            var result = TwitterContextViewModel.FromData(op.Result);
            return View(result);
        }
    }
}
