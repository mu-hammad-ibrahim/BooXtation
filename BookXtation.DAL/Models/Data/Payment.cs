namespace BookXtation.DAL.Models.Data
{
    public class Payment
    {
        public int Payment_ID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod {  get; set; }
        public decimal Amount { get; set; } 
        public string PaymentStatus { get; set; }

        public int Order_ID { get; set; }
        public virtual Order Order { get; set; }
    }
}
