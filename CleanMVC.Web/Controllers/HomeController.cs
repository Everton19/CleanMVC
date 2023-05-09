using Microsoft.AspNetCore.Mvc;

namespace CleanMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Links()
        {
            return View();
        }

    }
}
