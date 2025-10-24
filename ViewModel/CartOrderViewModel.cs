using Fast_Food_Delievery.Models;

namespace Fast_Food_Delievery.ViewModel
{
    public class CartOrderViewModel
    {
        public List<Cart> ListOfCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
