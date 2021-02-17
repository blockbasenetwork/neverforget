using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Controllers
{
    public class TwitterContextsController : Controller
    {
        private readonly TwitterContextPoco _twitterContextPoco;

        public TwitterContextsController(TwitterContextPoco twitterContextPoco)
        {
            _twitterContextPoco = twitterContextPoco;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
