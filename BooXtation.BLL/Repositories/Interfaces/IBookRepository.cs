using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using BooXtation.BLL.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IQueryable<Book>> GetAllTack24();
        Task<Book> GetBookByIdAsync(int id);
        Task<BookDropdownsViewModel> GetBookDropdownsValues();
        Task<IQueryable<Book>> FilterBooks(string searchString);

        Task<IEnumerable<Book>> GetALLWithSpecSync(ISpecification<Book> spec); //GetALL
        Task<Book> GetWithSpecSync(ISpecification<Book> spec); //Get by ID
        Task<int> GetCountWithSpecAsync(ISpecification<Book> spec); //Get by ID

        Task<decimal> GetMaxPriceAsync();
        Task<decimal> GetMinPriceAsync();

        Task ReturnBooksToStock(IQueryable<Order_Item> order_Items);
        Task<int> Count();
    }
}
