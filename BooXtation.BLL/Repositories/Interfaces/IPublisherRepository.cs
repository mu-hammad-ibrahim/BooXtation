using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Interfaces
{
    public interface IPublisherRepository 
    {
        Task<IEnumerable<Publisher>> GetAllAsync();
        Task<Publisher> GetByIdAsync(int id);
        Task AddAsync(Publisher publisher);
        Task UpdateAsync(Publisher publisher);
        Task DeleteAsync(int id);
        Task<int> Count();

    }
}
