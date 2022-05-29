using Microsoft.AspNetCore.Mvc;

namespace Projekt.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HttpStatusCodesController : Controller
    {
        public IActionResult PageNotFound(string source)
        {
            ViewBag.Source = source;

            return View();
        }
    }
}
