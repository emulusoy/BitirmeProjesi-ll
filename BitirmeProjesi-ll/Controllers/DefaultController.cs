using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesi_ll.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ActivePage = "Anasayfa";

            return View();
        }
    }
}
