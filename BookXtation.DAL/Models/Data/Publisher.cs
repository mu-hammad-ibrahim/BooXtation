namespace BookXtation.DAL.Models.Data
{
    public class Publisher
    {
        public int Publisher_ID { get; set; }
        public string Name { get; set; }
        public string ?Contact { get; set; }

        public string ?Address { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
