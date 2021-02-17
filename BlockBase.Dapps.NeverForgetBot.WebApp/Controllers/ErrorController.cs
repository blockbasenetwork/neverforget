using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlockBase.Dapps.NeverForgetBot.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorTitle = "404";
                    ViewBag.ErrorMessage = "The page you're looking for was not found";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
                default:
                    ViewBag.ErrorTitle = "500";
                    ViewBag.ErrorMessage = "An error has occurred, please try again later";
                    ViewBag.RouteOfException = statusCodeData?.OriginalPath;
                    break;
            }

            return View();
        }
    }
}
