using System.ComponentModel.DataAnnotations;

namespace Fast_Food_Delievery.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public DateTime TimeOfPick { get; set; }
        public DateTime TimeOfDelivery { get; set; }
        public double SubTotal { get; set; }
        public double OrderTotal { get; set; }
        public string TransactionId { get; set; } = null!;
        public string OrderStatus { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public int? VoucherId { get; set; }
        public Voucher? Voucher { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();


    }
}
