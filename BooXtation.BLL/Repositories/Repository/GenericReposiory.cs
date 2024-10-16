using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Repository
{
    public class GenericReposiory<T> : IGenericRepository<T> where T : class
    {
        private protected readonly BooXtationContext _dbcontext;

        public GenericReposiory(BooXtationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public async Task AddAsync(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
        }

        

        public async Task DeleteAsync(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public Task<IQueryable<T>> Filter()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<T>> GetAll()
        {
            if (typeof(T) == typeof(Book)) { 
                return  (IQueryable<T>)  _dbcontext.Set<Book>().Include(B => B.Publisher)
                                                               .Include(B => B.Author)
                                                               .Include(B => B.Category).AsNoTracking();
            }
            else if ((typeof(T) == typeof(Author)))
            {
                return (IQueryable<T>)_dbcontext.Set<Author>().Include(B => B.Books).AsNoTracking();

            }
            else if ((typeof(T) == typeof(Category)))
            {
                return (IQueryable<T>)_dbcontext.Set<Category>().Include(B => B.Books).AsNoTracking();

            }
            else if ((typeof(T) == typeof(Order)))
            {
                return (IQueryable<T>)_dbcontext.Set<Order>().Include(P => P.Payment)
                                                            .Include(C => C.Customer)
                                                            .Include(O => O.OrderDetails).AsNoTracking();
            }
            else
                return (IQueryable<T>)  _dbcontext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if ((typeof(T) == typeof(Author)))
            {
                var author = await _dbcontext.Set<Author>()
                                    .Include(a => a.Books)
                                    .FirstOrDefaultAsync(a => a.Author_ID == id);

                return author as T; 
            }
            else if ((typeof(T) == typeof(Category)))
            {
                var category = await _dbcontext.Set<Category>()
                                    .Include(a => a.Books)
                                    .FirstOrDefaultAsync(a => a.Category_ID == id);

                return category as T;
            }
            else if ((typeof(T) == typeof(Order)))
            {
                var category = await _dbcontext.Set<Order>()
                                    .Include(P => P.Payment)
                                    .Include(C => C.Customer)
                                    .Include(O => O.OrderDetails).Include(o => o.Order_Items)
                                    .ThenInclude(oi => oi.book)
                                    .FirstOrDefaultAsync(a => a.Order_ID == id);

                return category as T;
            }
            else
            {
                return await _dbcontext.Set<T>().FindAsync(id);
            }
        }


        public async Task UpdateAsync(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
            _dbcontext.Entry(entity).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }


    }
}
