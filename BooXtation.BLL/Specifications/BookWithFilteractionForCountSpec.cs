using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Specifications
{
    public class BookWithFilteractionForCountSpec : BaseSpecification<Book>
    {
        public BookWithFilteractionForCountSpec(BookSpecParams bookSpecParams): base(
          p =>
            (!string.IsNullOrEmpty(bookSpecParams.Search)
                ? p.Title.ToLower().Contains(bookSpecParams.Search.ToLower()) ||
                  p.Author.Name.ToLower().Contains(bookSpecParams.Search.ToLower()) 
                : true)
            && (bookSpecParams.AuthorIds != null && bookSpecParams.AuthorIds.Any()
                ? bookSpecParams.AuthorIds.Contains(p.Author_ID)
                : true)
            &&
            (bookSpecParams.CategoryIds != null && bookSpecParams.CategoryIds.Any()
                ? bookSpecParams.CategoryIds.Contains(p.Category_ID)
                : true)
            &&
            (bookSpecParams.PublisherIds != null && bookSpecParams.PublisherIds.Any()
                ? bookSpecParams.PublisherIds.Contains(p.Publisher_ID)
                : true)
            && (bookSpecParams.MinPrice.HasValue ? p.Price >= bookSpecParams.MinPrice : true)
            && (bookSpecParams.MaxPrice.HasValue ? p.Price <= bookSpecParams.MaxPrice : true)
            )
        {

        }
    }
}
