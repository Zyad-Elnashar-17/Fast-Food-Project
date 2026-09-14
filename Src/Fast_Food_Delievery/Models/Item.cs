namespace Fast_Food_Delievery.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; } = null!;


        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    }
}
