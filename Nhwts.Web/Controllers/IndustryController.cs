using Microsoft.AspNetCore.Mvc;

namespace Nhwts.Web.Controllers
{
    public class IndustryController : Controller
    {
        public IActionResult Industry()
        {
            ViewBag.Title = "Industry Page";
            return View();
        }
    }
}
