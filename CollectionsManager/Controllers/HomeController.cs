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
        public IActionResult Index(string searchTerm)
        {
            string search = searchTerm?.ToLower();
            if (search != null)
            {
                List<Collection> collections = _db.Collections.Where(c => c.Name.ToLower().Contains(search)).ToList();
                List<Item> items = _db.Items.Where(i => i.Name.ToLower().Contains(search)).ToList();
                List<Tag> tags = _db.Tags.Where(t => t.Name.ToLower().Contains(search)).ToList();

                SearchResults searchResults = new()
                {
                    Collections = collections,
                    Items = items,
                    Tags = tags
                };

                return View(searchResults);
            }
            return View();
        }
    }
}
