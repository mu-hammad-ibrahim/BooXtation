using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using BookXtation.DAL.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BooXtation.BLL.Repositories.Specifications;
using BookXtation.DAL;

namespace BooXtation.BLL.Repositories.Repository
{
    public class BookRepository : GenericReposiory<Book> , IBookRepository
    {
        
        public BookRepository(BooXtationContext dbcontext) : base(dbcontext)
        {
            
        }
        public async Task<Book> GetBookByIdAsync(int id)
        {
            var bookDetails =await _dbcontext.Books.Include(b => b.Publisher)
                                              .Include(b => b.Category)
                                              .Include(b => b.Author)
                                              .Where(b => b.Book_ID == id).FirstOrDefaultAsync();
            return bookDetails ;
        }



        public async Task<BookDropdownsViewModel> GetBookDropdownsValues()
        {
            
            var response = new BookDropdownsViewModel()
            {
                Authors = await _dbcontext.Authors.OrderBy(a => a.Name).ToListAsync(),
                Categories = await _dbcontext.Categories.OrderBy(a => a.Name).ToListAsync(),
                Publishers = await _dbcontext.Publishers.OrderBy(a => a.Name).ToListAsync()
            };

            return response;
        }

        public async Task<IQueryable<Book>> FilterBooks(string searchString)
        {
            searchString = searchString.ToLower();
            var books = _dbcontext.Books.Where(b => b.Title.Contains(searchString)
                                                  ||b.Publisher.Name.Contains(searchString)
                                                  ||b.Author.Name.Contains(searchString))
                                        .Include(b=>b.Author)
                                        .Include(b=>b.Publisher)
                                        .Include(b=>b.Category);

            return books;
        }

        public async Task<IEnumerable<Book>> GetALLWithSpecSync(ISpecification<Book> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<Book> GetWithSpecSync(ISpecification<Book> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<Book> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<Book> ApplySpecification(ISpecification<Book> spec)
        {
            return SpecificationEvaluator<Book>.GetQuery(_dbcontext.Set<Book>(), spec);
        }

        public async Task<decimal> GetMinPriceAsync()
        {
            return await _dbcontext.Books.MinAsync(b => b.Price);
        }

        public async Task<decimal> GetMaxPriceAsync()
        {
            return await _dbcontext.Books.MaxAsync(b => b.Price);
        }

        public async Task<IQueryable<Book>> GetAllTack24()
        {
            var books = await _dbcontext.Set<Book>()
                                        .Include(b => b.Publisher)
                                        .Include(b => b.Author)
                                        .Include(b => b.Category)
                                        .Where(b => b.Stock > 0)
                                        .OrderByDescending(b => b.Order_Items.Sum(oi => oi.Quantity))
                                        .Take(24)
                                        .ToListAsync();

            return books.AsQueryable();
        }

        public async Task<int> Count()
        {
            return await _dbcontext.Books.CountAsync();
        }

        public async Task ReturnBooksToStock(IQueryable<Order_Item> order_Items)
        {
            
            foreach (var item in order_Items.ToList())
            {
                var book = await _dbcontext.Books.FirstOrDefaultAsync(x => x.Book_ID == item.Book_ID);
                book.Stock += item.Quantity;

            }
            await _dbcontext.SaveChangesAsync();
        }
    }

}
