namespace BookXtation.DAL.Models.Data
{
    public class Shopping_Cart
    {
        public int Cart_ID { get; set; }

        public int Customer_ID { get; set; }
        public virtual Customer Customer { get; set; }
        
        public DateTime Created_At { get; set; }

        public virtual ICollection<Cart_Item> Cart_Items { get; set; } = new List<Cart_Item>();
    }
}
