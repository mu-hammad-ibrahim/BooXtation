using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Repository
{
    public class Order_ItemRepository : GenericReposiory<Order_Item>, IOrder_ItemRepository
    {
        public Order_ItemRepository(BooXtationContext dbContext) :base(dbContext) 
        {
            
        }
        public async Task AddListAsync(List<Order_Item> entity)
        {
            await _dbcontext.Order_Items.AddRangeAsync(entity);
            await _dbcontext.SaveChangesAsync();
           
        }

        public async Task<Customer> GetCustomerById (int id)
        {
            return await _dbcontext.Customers.Where(x => x.Customer_ID == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteFromBookStock(List<Order_Item> order_Items)
        {
            bool Check = true;
                foreach (var item in order_Items)
                {
                    var BookStock = await _dbcontext.Books.Where(x => x.Book_ID == item.Book_ID).FirstOrDefaultAsync();

                    Check = (item.Quantity <= BookStock.Stock) ? (BookStock.Stock -= item.Quantity) >= 0 : false;
                }
                if(Check)
                    await _dbcontext.SaveChangesAsync();

            return Check;
        }

        public async Task<int> GetStockOfBooks(int Book_ID)
        {
            var Book =  await _dbcontext.Books.Where(x => x.Book_ID == Book_ID).FirstOrDefaultAsync();
            return Book.Stock;
        }

        public async Task<Order_Item> GetOrderItemWithOrderAndBookasync(int itemID)
        {
            return await _dbcontext.Order_Items.Where(x => x.OrderItem_ID == itemID)
                .Include(x => x.Order).ThenInclude(x => x.Payment)
                .Include(x => x.Order).ThenInclude(x => x.OrderDetails)
                .Include(x => x.book).ThenInclude(x => x.Category)
                .Include(x => x.book).ThenInclude(x => x.Author)
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Order_Item>> GetCustomerOrderItems(int Order_ID)
        {
            var orderItems =  _dbcontext.Order_Items.Where(x => x.Order_ID == Order_ID).AsNoTracking();

            return orderItems;
        }

        public async Task<IQueryable<Order_Item>> GetOrderItemWithAllEntity(int Order_ID)
        {
            return _dbcontext.Order_Items.Where(x => x.Order_ID == Order_ID)
                .Include(x => x.Order).ThenInclude(x => x.Payment)
                .Include(x => x.book).AsNoTracking();
        }
    }
}
