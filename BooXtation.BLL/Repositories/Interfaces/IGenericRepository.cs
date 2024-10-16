using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll();

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
