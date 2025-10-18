namespace Fast_Food_Delievery.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public VoucherType Type { get; set; }
        public double Discount { get; set; }
        public string code { get; set; } = null!;
        public int MinAmount { get; set; } 
        public bool IsActive { get; set; }

        public ICollection<OrderHeader> Orders { get; set; } = new List<OrderHeader>();


    }

    public enum VoucherType
    {
        Percent = 0,
        currency = 1
    }
}
