using Microsoft.EntityFrameworkCore;

namespace CollectionsManager.Models
{
  public class CollectionsManagerContext : DbContext
  {
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<ItemTagJoinEntity> ItemTagJoinEntities { get; set; }
    public CollectionsManagerContext(DbContextOptions options) : base(options) { }
  }
}
