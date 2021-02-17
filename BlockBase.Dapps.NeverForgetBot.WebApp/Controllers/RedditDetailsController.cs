using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Controllers
{
    public class RedditDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
