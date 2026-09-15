using FastFood.Web.Models;

namespace FastFood.Web.ViewModel
{
    public class CartOrderViewModel
    {
        public List<Cart> ListOfCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
