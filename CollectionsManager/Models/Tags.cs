using System.ComponentModel.DataAnnotations;

namespace CollectionsManager.Models;
public class Tag
{
    [Range(0, int.MaxValue)]
    public int TagId { get; set; }
    [Required(ErrorMessage = "A tag name is required")]
    [MaxLength(255, ErrorMessage = "Names must be less than 256 characters")]

    public string Name { get; set; }
    public List<ItemTagJoinEntity> ItemTagJoinEntities { get; }
}