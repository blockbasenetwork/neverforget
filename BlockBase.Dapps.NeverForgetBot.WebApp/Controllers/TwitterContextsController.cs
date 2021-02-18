using BlockBase.Dapps.NeverForgetBot.Business.BusinessLayer.Interfaces;
using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using BlockBase.Dapps.NeverForgetBot.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public async Task<IActionResult> Index()
        {
            var listOp = await _twitterContextBo.GetAllPocoAsync();
            if (!listOp.Success) return View("Error", new ErrorViewModel() { RequestId = listOp.Exception.Message });
            var list = new List<TwitterContextViewModel>();
            foreach (var item in listOp.Result)
            {
                list.Add(TwitterContextViewModel.FromData(item));
            }
            return View(list);
        }
    }
}
