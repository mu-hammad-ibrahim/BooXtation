using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Interfaces
{
    public interface IOrder_ItemRepository : IGenericRepository<Order_Item>
    {
        Task AddListAsync(List<Order_Item> entity);
        Task<Customer> GetCustomerById(int id);

        Task<bool> DeleteFromBookStock(List<Order_Item> order_Items);

        Task<Order_Item> GetOrderItemWithOrderAndBookasync(int itemID);
        Task<int> GetStockOfBooks(int Book_ID);

        Task<IQueryable<Order_Item>> GetOrderItemWithAllEntity(int Order_ID);

        Task<IQueryable<Order_Item>> GetCustomerOrderItems(int Order_ID);
    }
}
