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
    public class Shopping_CartRepository : GenericReposiory<Shopping_Cart>, IShopping_CartRepository
    {
        public Shopping_CartRepository(BooXtationContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<int> createNewCartAsync(int customerId)
        {
                var NewShoppingCart = new Shopping_Cart();
                NewShoppingCart.Customer_ID = customerId;
                NewShoppingCart.Created_At = DateTime.Now;
                await _dbcontext.AddAsync(NewShoppingCart);
                await _dbcontext.SaveChangesAsync();

            return NewShoppingCart.Cart_ID;
        }
        public async Task createNewCartAfterPlaceOrderAsync(int customerId)
        {
            var NewShoppingCart = new Shopping_Cart();
            NewShoppingCart.Customer_ID = customerId;
            NewShoppingCart.Created_At = DateTime.Now;
            await _dbcontext.AddAsync(NewShoppingCart);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IQueryable<Cart_Item>> getShoppingCartItems(int customerId)
        {
            var LastCart = await _dbcontext.Shopping_Carts.OrderBy(x => x.Cart_ID)
                .Where(x => x.Customer_ID == customerId).LastOrDefaultAsync();

            return _dbcontext.Cart_Items.Include(x => x.book)
                   .Where(x => x.Shopping_Cart.Cart_ID == LastCart.Cart_ID && LastCart.Customer_ID == customerId)
                   .AsNoTracking();
        }

        public async Task<Shopping_Cart> getShoppingCartByCustIdAsync(int customerId)
        {
            return _dbcontext.Shopping_Carts.OrderBy(x => x.Cart_ID)
                .Where(x => x.Customer_ID == customerId).LastOrDefault();
        }
    }
}
