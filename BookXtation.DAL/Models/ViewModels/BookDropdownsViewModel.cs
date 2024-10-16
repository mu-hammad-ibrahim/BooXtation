using BookXtation.DAL.Models.Data;
namespace BookXtation.DAL.Models.ViewModels
{
    public class BookDropdownsViewModel
    {
        public BookDropdownsViewModel()
        {
            Authors = new List<Author>();
            Publishers = new List<Publisher>();
            Categories = new List<Category>();
        }

        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Category> Categories { get; set; }
    }
}
