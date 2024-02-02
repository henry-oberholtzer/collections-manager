using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollectionsManager.Models;

public class Collection
{
  [Range(1, int.MaxValue, ErrorMessage = "A Category must be chosen")]
  public int CollectionId { get; set; }

  [Required(ErrorMessage = "This is a required field!")]
  public string Name { get; set; }
  public string Description { get; set; }

  public string ImageUrl { get; set; }
  public List<Item> Items { get; set; }
}
