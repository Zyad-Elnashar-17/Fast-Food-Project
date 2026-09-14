using System.ComponentModel.DataAnnotations;

namespace Fast_Food_Delievery.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;


        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;


        public ICollection<Item> Items { get; set; } = new List<Item>();


    }
}
