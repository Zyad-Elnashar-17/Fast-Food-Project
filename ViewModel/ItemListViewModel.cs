using Fast_Food_Delievery.Models;

namespace Fast_Food_Delievery.ViewModel
{
    public class ItemListViewModel
    {
        public IEnumerable<Item> Items { get; set; } = new List<Item>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Voucher> Vouchers { get; set; } = new List<Voucher>();
    }
}
