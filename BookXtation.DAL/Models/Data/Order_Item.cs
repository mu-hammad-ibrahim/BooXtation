using System.ComponentModel.DataAnnotations;

namespace BookXtation.DAL.Models.Data
{
    public class Order_Item
    {
        public int OrderItem_ID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity {  get; set; }
        public decimal Price { get; set; }

        public int Order_ID { get; set; }
        public virtual Order Order { get; set; }

        public int Book_ID { get; set; }
        public virtual Book book { get; set; }
    }
}
