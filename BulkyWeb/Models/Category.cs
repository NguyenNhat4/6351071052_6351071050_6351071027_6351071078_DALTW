using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key]
        public int Category21Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string DisplayOrder { get; set; }
    }

}
