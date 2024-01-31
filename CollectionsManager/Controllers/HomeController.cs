using Microsoft.AspNetCore.Mvc;

namespace CollectionsManager.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
