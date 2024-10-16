using System.ComponentModel.DataAnnotations;

namespace BookXtation.DAL.Models.Data
{
    public class Order
    {
        public int Order_ID { get; set; }
        public DateTime OrderDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total Amount must be a non-negative value.")]
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }

        public int Customer_ID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual OrderDetails OrderDetails { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ICollection<Order_Item> Order_Items { get; set; } = new List<Order_Item>();
    }
}
