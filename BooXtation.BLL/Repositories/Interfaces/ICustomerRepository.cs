using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BooXtation.BLL.Repositories.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetByUserIdAsync (string userId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<int> Count();

    }
}
