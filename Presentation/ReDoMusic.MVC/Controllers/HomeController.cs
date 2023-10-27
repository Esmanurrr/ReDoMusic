using Microsoft.AspNetCore.Mvc;

namespace ReDoMusic.MVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
