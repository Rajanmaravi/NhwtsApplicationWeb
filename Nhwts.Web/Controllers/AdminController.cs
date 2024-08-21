using Microsoft.AspNetCore.Mvc;

namespace Nhwts.Web.Controllers
{
    public class AdminController: Controller
    {

        public IActionResult Index()
        {
            // industry page.
            //===========================ss
            ViewBag.Title = "Admin Page";
            return View();
        }
        public IActionResult ManageUsers()
        {
            // industry page.
            //===========================ss
            ViewBag.Title = "Manage Users";
            return View();
        }
    }
}
