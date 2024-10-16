using BooXtation.BLL.Repositories.Interfaces;
using BookXtation.DAL.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BookXtation.DAL.Models;

namespace BooXtation.BLL.Repositories.Repository
{
    public class CustomerRepository : GenericReposiory<Customer>, ICustomerRepository
    {
        public CustomerRepository(BooXtationContext dbcontext) : base(dbcontext) { }

        public async Task<Customer> GetByUserIdAsync(string userId)
        {
            return await _dbcontext.Customers.Include(x => x.customerDetails).Where(x => x.User_ID == userId).FirstOrDefaultAsync();
        }

        
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _dbcontext.Customers
                        .Include(c => c.customerDetails) 
                        .ToListAsync();
        }

        public async Task<int> Count()
        {
            return await _dbcontext.Customers.CountAsync();
        }
    }
}
