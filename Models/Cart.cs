using System.ComponentModel.DataAnnotations;

namespace Fast_Food_Delievery.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public User? User { get; set; }
        [Required,MinLength(1)]
        public int Count { get; set; }

        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<OrderHeader> Orders { get; set; } = new List<OrderHeader>();

    }
}
