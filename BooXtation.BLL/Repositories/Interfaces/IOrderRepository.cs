using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<int> AddNewOrderAsync(Order order);
        Task<IQueryable<Order>> GetAllOrderForCustomer(int customerID);
        Task<IEnumerable<Order>> GetAllOrderForDashBoard();

        Task OrderIsCancelledAsync(int Order_ID);
        Task ChangeStatusAsync(int Order_ID , string status);
        Task<int> Count();
    }
}
