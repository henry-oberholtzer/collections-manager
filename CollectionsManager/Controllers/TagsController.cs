using Microsoft.AspNetCore.Mvc;
using CollectionsManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollectionsManager.Controllers;

public class TagsController : Controller
{
    private readonly CollectionsManagerContext _db;
    public TagsController(CollectionsManagerContext db)
    {
        _db = db;
    }
    public ActionResult Index()
    {
        return View(_db.Tags.Include(tag => tag.ItemTagJoinEntities).Take(10).OrderBy(Tag => Tag.Name).ToList());
    }
    public ActionResult Details(int id)
    {
        Tag tag = _db.Tags
        .Include(Tag => Tag.ItemTagJoinEntities)
        .ThenInclude(join => join.Item)
        .FirstOrDefault(tag => tag.TagId == id);
        return View(tag);
    }
    public ActionResult Create()
    {
        Dictionary<string, object> model = new() {
        {"action", "Create"},
        {"Tag", new Tag()}
        };
        return View(model);
    }
    [HttpPost]
    public ActionResult Create(Tag tag)
    {
        _db.Tags.Add(tag);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult AddItem(Tag tag, int itemId)
    {
#nullable enable
        ItemTagJoinEntity? entity = _db.ItemTagJoinEntities.FirstOrDefault(join => join.ItemId == itemId && join.TagId == tag.TagId);
#nullable disable
        if (entity == null && itemId != 0)
        {
            _db.ItemTagJoinEntities.Add(new ItemTagJoinEntity() { ItemId = itemId, TagId = tag.TagId });
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = tag.TagId });
    }
    public ActionResult Edit(int id)
    {
        Tag tag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
        Dictionary<string, object> model = new() {
        {"action", "Edit"},
        {"Tag", tag}
        };
        return View(model);
    }
    [HttpPost]
    public ActionResult Edit(Tag tag)
    {
        _db.Tags.Update(tag);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = tag.TagId });
    }
    [HttpPost]
    public ActionResult Delete(int id)
    {
        Tag thisTag = _db.Tags
        .Include(tag => tag.ItemTagJoinEntities)
        .FirstOrDefault(tags => tags.TagId == id);

        _db.Tags.Remove(thisTag);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteItemTagJoinEntity(int joinId)
    {
        ItemTagJoinEntity join = _db.ItemTagJoinEntities.FirstOrDefault(entry => entry.ItemTagJoinEntityId == joinId);
        _db.ItemTagJoinEntities.Remove(join);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}