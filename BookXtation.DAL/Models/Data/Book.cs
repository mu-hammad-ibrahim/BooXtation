using System.ComponentModel.DataAnnotations;

namespace BookXtation.DAL.Models.Data
{
    public class Book
    {
        public int Book_ID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string? Summary { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        public decimal ActualPrice { get; set; }
        public decimal Discount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative number.")]
        public int Stock { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pages must be at least 1.")]
        public int? Pages { get; set; }
        public string? Language { get; set; }
        public DateTime? Publish_Date { get; set; }
        public string? Cover_Image { get; set; }

        public int Publisher_ID { get; set; }
        public virtual Publisher Publisher { get; set; }

        public int Author_ID { get; set; }
        public virtual Author Author { get; set; }

        public int Category_ID { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Cart_Item> Cart_Items { get; set; } = new List<Cart_Item>();
        public virtual ICollection<Order_Item> Order_Items { get; set; } = new List<Order_Item>();
        //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<FavouriteBooks> FavouriteBooks { get; set; } = new List<FavouriteBooks>();

        public void CalculatePrice()
        {
            Price = ActualPrice - (ActualPrice * (Discount / 100));
        }
    }

}
