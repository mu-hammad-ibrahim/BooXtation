namespace BookXtation.DAL.Models.Data
{
    public class Author
    {
        public int Author_ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
