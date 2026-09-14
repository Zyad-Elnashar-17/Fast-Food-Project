namespace Fast_Food_Delievery.Models
{
    public class TapPaymentRequest
    {

        public string amount { get; set; }
        public string currency { get; set; } = "EGP";
        public string description { get; set; } = "Order Payment";
        public string customer_email { get; set; }
        public string source_id { get; set; } = "src_all";
        public string redirect_url { get; set; }
    }
}
