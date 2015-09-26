using Microsoft.AspNet.Mvc;

namespace Etchd.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}