using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Repository
{
    public class OrderRepository : GenericReposiory<Order> , IOrderRepository
    {
        public OrderRepository(BooXtationContext dbcontext) : base(dbcontext)
        {
            
        }

        public async Task<int> AddNewOrderAsync(Order order)
        {
            
            await _dbcontext.Orders.AddAsync(order);
            await _dbcontext.SaveChangesAsync();
            int id = order.Order_ID;
            return id;
        }

        public async Task<IQueryable<Order>> GetAllOrderForCustomer(int customerID)
        {
            return _dbcontext.Orders.OrderByDescending(x => x.OrderDate)
                .Where(x => x.Customer_ID == customerID).Include(x => x.Order_Items)
                .ThenInclude(x => x.book)
                .AsNoTracking();
                
        }
        public async Task<IEnumerable<Order>> GetAllOrderForDashBoard() => await _dbcontext.Orders.ToListAsync();

        public async Task OrderIsCancelledAsync(int Order_ID)
        {
            var order = await _dbcontext.Orders.Where(x => x.Order_ID == Order_ID)
                .Include(x => x.Payment).FirstOrDefaultAsync();
            order.OrderStatus = "Order Is Cancelled";
            order.PaymentStatus = "Order Is Cancelled";
            order.Payment.PaymentStatus = "Order Is Cancelled";

            await _dbcontext.SaveChangesAsync();
        }
        public async Task<int> Count()
        {
            return await _dbcontext.Orders.CountAsync();
        }

        public async Task ChangeStatusAsync(int Order_ID, string status)
        {
            var order = await _dbcontext.Orders.Where(x => x.Order_ID == Order_ID)
                .FirstOrDefaultAsync();
            order.OrderStatus = status;
            
            await _dbcontext.SaveChangesAsync();
        }
    }
}
