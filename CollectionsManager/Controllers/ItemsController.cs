using Microsoft.AspNetCore.Mvc;
using CollectionsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

      if (!string.IsNullOrEmpty(searchString))
      {
        model = model.Where(s => s.Name!.Contains(searchString));
        ViewBag.Title = $"{searchString}";
      }
      ViewBag.Title = "All Items";
      return View(await model.OrderBy(e => e.Name).ToListAsync());
    }

    [HttpGet("/items/create/")]
    public ActionResult Create()
    {
      Dictionary<string, object> model = new() {
        {"Item", new Item()},
        {"CollectionSelect", new SelectList(_db.Collections, "CollectionId", "Name")},
        {"TodaysDate", DateTime.Now.ToString("dd-MM-yyyy")},
        {"Tags", _db.Tags.ToList()}
      };
      return View(model);
    }

    [HttpPost]
    public ActionResult Create(Item item, List<int> TagIds)
    {
      if (item.CollectionId == 0)
      {
        return RedirectToAction("Create");
      }

      _db.Items.Add(item);
      _db.SaveChanges();

      if (TagIds != null && TagIds.Any())
      {
        foreach (var tagId in TagIds)
        {
          _db.ItemTagJoinEntities.Add(new ItemTagJoinEntity
          {
            ItemId = item.ItemId,
            TagId = tagId
          });
        }
        _db.SaveChanges();
      }

      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      Item thisItem = _db.Items
                          .Include(item => item.Collection)
                          .Include(item => item.ItemTagJoinEntities)
                          .ThenInclude(join => join.Tag)
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

    public ActionResult AddTag(int id)
    {
      Item item = _db.Items.FirstOrDefault(items => items.ItemId == id);
      Dictionary<string, object> model = new()
      {
        {"item", item},
        {"selectList", new SelectList(_db.Tags, "TagId", "Name")}
      };
      return View(model);
    }

    [HttpPost]
    public ActionResult AddTag(Item item, int tagId)
    {
#nullable enable
      ItemTagJoinEntity? itemTagJoinEntity = _db.ItemTagJoinEntities
      .FirstOrDefault(join => (join.TagId == tagId && join.ItemId == item.ItemId));
#nullable disable
      if (itemTagJoinEntity == null && tagId != 0)
      {
        _db.ItemTagJoinEntities.Add(new ItemTagJoinEntity()
        {
          TagId = tagId,
          ItemId = item.ItemId
        });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = item.ItemId });
    }

    [HttpPost]
    public ActionResult DeleteItemTagJoinEntity(int itemTagJoinEntityId)
    {
      ItemTagJoinEntity join = _db.ItemTagJoinEntities.FirstOrDefault(entry => entry.ItemTagJoinEntityId == itemTagJoinEntityId);
      _db.ItemTagJoinEntities.Remove(join);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
