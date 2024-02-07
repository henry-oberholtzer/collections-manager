namespace CollectionsManager.Models;

public class ItemTagJoinEntity
{
    public int ItemTagJoinEntityId { get; set; }
    public int ItemId { get; set; }
    public int TagId { get; set; }
    public Item Item { get; set; }
    public Tag Tag { get; set; }
}