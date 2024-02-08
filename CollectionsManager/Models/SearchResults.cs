namespace CollectionsManager.Models;

public class SearchResults
{
    public List<Collection> Collections { get; set; }
    public List<Item> Items { get; set; }
    public List<Tag> Tags { get; set; }
}