using System.ComponentModel.DataAnnotations;

namespace BookXtation.DAL.Models.Data
{
    public class Customer
    {
        public int Customer_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Phone]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$",
        ErrorMessage = "Invalid phone number format. Please enter a valid phone number.")]
        public string Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? User_ID { get; set; }
        public virtual ApplicationUser applicationUser { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Shopping_Cart> Shopping_Carts { get; set; } = new List<Shopping_Cart>();
        //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<CustomerDetails> customerDetails { get; set; } = new List<CustomerDetails>();
        public virtual ICollection<FavouriteBooks> FavouriteBooks { get; set; } = new List<FavouriteBooks>();
    }
}
