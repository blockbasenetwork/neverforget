using BlockBase.Dapps.NeverForgetBot.Data.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Controllers
{
    public class RedditController : Controller
    {
        private readonly RedditContextPoco _redditContextPoco;

        public RedditController(RedditContextPoco redditContextPoco)
        {
            _redditContextPoco = redditContextPoco;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
