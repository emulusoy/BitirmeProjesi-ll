using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesi_ll.Controllers
{
    public class LayoutController : Controller
    {
        public IActionResult _Layout()
        {
            return View();
        }
    }
}
