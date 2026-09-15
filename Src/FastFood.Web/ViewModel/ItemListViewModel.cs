using FastFood.Web.Models;

namespace FastFood.Web.ViewModel
{
    public class ItemListViewModel
    {
        public IEnumerable<Item> Items { get; set; } = new List<Item>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Voucher> Vouchers { get; set; } = new List<Voucher>();
    }
}
