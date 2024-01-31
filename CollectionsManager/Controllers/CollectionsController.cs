using Microsoft.AspNetCore.Mvc;
using CollectionsManager.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace CollectionsManager.Controllers
{
  public class CollectionsController : Controller
  {
    private readonly CollectionsManagerContext _db;

    public CollectionsController(CollectionsManagerContext db)
    {
      _db = db;
    }
    // public ActionResult Index()
    // {
    //   List<Collection> model = _db.Collections.ToList();
    //   return View(model);
    // }
    public async Task<IActionResult> Index(string searchString)
    {
      IQueryable<Collection> model = from m in _db.Collections
                                       // .Include(collection => item.Collection)  THIS LINE MIGHT NEED CHANGING
                                     select m;

      if (!String.IsNullOrEmpty(searchString))
      {
        model = model.Where(s => s.Name!.Contains(searchString));
      }
      return View(await model.ToListAsync());
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Collection collection)
    {
      _db.Collections.Add(collection);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Edit(int id)
    {
      Collection thisCollection = _db.Collections.FirstOrDefault(collection => collection.CollectionId == id);
      return View(thisCollection);
    }
    [HttpPost]
    public ActionResult Edit(Collection collection)
    {
      _db.Collections.Update(collection);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      Collection thisCollection = _db.Collections.FirstOrDefault(collection => collection.CollectionId == id);
      return View(thisCollection);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Collection thisCollection = _db.Collections.FirstOrDefault(collection => collection.CollectionId == id);
      _db.Collections.Remove(thisCollection);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Collection thisCollection = _db.Collections
                                  .Include(collection => collection.Items)
                                  .FirstOrDefault(collection => collection.CollectionId == id);
      return View(thisCollection);
    }
  }
}
