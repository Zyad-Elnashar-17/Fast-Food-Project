using System.ComponentModel.DataAnnotations;

namespace Fast_Food_Delievery.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;


        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public ICollection<Item> Items { get; set; } = new List<Item>();

    }
}
