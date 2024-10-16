using System.ComponentModel.DataAnnotations;

namespace BookXtation.DAL.Models.Data
{
    public class Cart_Item
    {
        public int CartItem_ID { get; set; } 

        public int Cart_ID { get; set; }
        public virtual Shopping_Cart Shopping_Cart { get; set; }

        public int Book_ID { get; set; }
        public virtual Book book { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; set; }

    }
}
