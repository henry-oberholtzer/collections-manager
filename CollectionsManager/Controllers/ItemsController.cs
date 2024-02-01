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
  public class ItemsController : Controller
  {
    private readonly CollectionsManagerContext _db;
    public ItemsController(CollectionsManagerContext db)
    {
      _db = db;
    }


    public async Task<IActionResult> Index(string searchString)
    {
      IQueryable<Item> model = from m in _db.Items.Include(item => item.Collection)
                               select m;

      if (!String.IsNullOrEmpty(searchString))
      {
        model = model.Where(s => s.Name!.Contains(searchString));
        ViewBag.Title = $"{searchString}";
      }
      ViewBag.Title = "All Items";
      return View(await model.OrderBy(e => e.Name).ToListAsync());
    }


    public ActionResult Create()
    {
      ViewBag.CollectionId = new SelectList(_db.Collections, "CollectionId", "Name");
      ViewBag.TodaysDate = System.DateTime.Now.ToString("dd-MM-yyyy");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Item item)
    {

      if (item.CollectionId == 0)
      {
        return RedirectToAction("Create");
      }

      _db.Items.Add(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      Item thisItem = _db.Items
                          .Include(item => item.Collection)
                          .FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    public ActionResult Edit(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.CollectionId = new SelectList(_db.Collections, "CollectionId", "Name");
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult Edit(Item item)
    {
      _db.Items.Update(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
