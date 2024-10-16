using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Specifications
{
    public class BooktWithSpecifications : BaseSpecification<Book>
    {
        public BooktWithSpecifications(BookSpecParams bookSpecParams) : base(

             p =>
             //(!string.IsNullOrEmpty(bookSpecParams.Search) ? p.Title.ToLower().Contains(bookSpecParams.Search.ToLower()) : true)
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
            Includes.Add(b => b.Author);
            Includes.Add(b => b.Publisher);
            Includes.Add(b => b.Category);

            if (!string.IsNullOrEmpty(bookSpecParams.Sort))
            {
                switch (bookSpecParams.Sort)
                {
                    case "priceAsc":
                        OrderBy = e => e.Price;
                        break;
                    case "priceDesc":
                        OrderByDescending = e => e.Price;
                        break;
                    case "BestSelling":
                        OrderByDescending = e => e.Order_Items.Sum(oi => oi.Quantity);
                        break;
                    case "NewestArrivals":
                        OrderByDescending = e => e.Book_ID;
                        break;
                    default:
                        OrderBy = e => e.Title;
                        break;
                }
            }

            ApplyPagination((bookSpecParams.PageIndex - 1) * bookSpecParams.PageSize, bookSpecParams.PageSize);
        }

        public BooktWithSpecifications(int id) : base(e => e.Book_ID == id)
        {
            Includes.Add(b => b.Author);
            Includes.Add(b => b.Publisher);
            Includes.Add(b => b.Category);
        }

    }
}
