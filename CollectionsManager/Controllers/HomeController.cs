using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollectionsManager.Models;
using System;

namespace CollectionsManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly CollectionsManagerContext _db;

        public HomeController(CollectionsManagerContext db)
        {
            _db = db;
        }

        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
