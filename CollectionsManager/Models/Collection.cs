namespace CollectionsManager.Models;

public class Collection
{
  public int CollectionId { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }

  public string ImageUrl { get; set; }
  public List<Item> Items { get; set; }
}
