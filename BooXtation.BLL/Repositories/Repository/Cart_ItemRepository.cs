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
    public class Cart_ItemRepository : GenericReposiory<Cart_Item> , ICart_ItemRepository
    {
        
        public Cart_ItemRepository(BooXtationContext dbContext) : base(dbContext)
        {
            
        }

        public async Task AddBookToCartAsync(int Book_ID, int customerId, int Cart_ID, int quantity = 1)
        {
            var check = await _dbcontext.Cart_Items.Where(x => x.Book_ID == Book_ID && x.Cart_ID == Cart_ID).OrderBy(x => x.Cart_ID)
                .Select(x => x.Book_ID).LastOrDefaultAsync();
            
            var cartItems = await GetCart_itemByCartIdAsync(Cart_ID);

            
            // عشان لو اخر عربيه لاخر دخول للعميل بتساوي رقم العربيه في الايتيمز و رقم الكتاب موجود يبقي كتاب عدده 2
            if (Cart_ID == cartItems?.Cart_ID && check == Book_ID)
            {
                var shoppingCart = await _dbcontext.Cart_Items.Select(x => new { shoppingCart=x.Shopping_Cart , cartItem = x })
                    .Where(x => x.shoppingCart.Cart_ID == cartItems.Cart_ID && x.shoppingCart.Customer_ID == customerId)
                    .Where(x => x.cartItem.Book_ID == Book_ID)
                    .FirstOrDefaultAsync();

                shoppingCart.cartItem.Price += (shoppingCart.cartItem.Price / shoppingCart.cartItem.Quantity);
                shoppingCart.cartItem.Quantity += quantity;
                
            }
            else // لو لأ يروح يعمل عمليه اضافه ايتيم جديد ببيانات الكتاب المختلف عن اللي موجود
            {
                var item = new Cart_Item();
                item.Book_ID = Book_ID;
                item.Quantity = quantity;
                item.Price = (decimal)_dbcontext.Books.Where(x => x.Book_ID == Book_ID)
                    .FirstOrDefaultAsync().GetAwaiter().GetResult().Price * quantity;

                item.Cart_ID = Cart_ID;
                await _dbcontext.Cart_Items.AddAsync(item);
            }
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IQueryable<Cart_Item>> GetAllCartItemsWithBooks(int customerId)
        {
            return _dbcontext.Cart_Items.Include(x => x.book)
                .OrderBy(x => x.Cart_ID)
                .Where(x => x.Shopping_Cart.Cart_ID == x.Cart_ID && x.Shopping_Cart.Customer_ID == customerId)
                .AsNoTracking();
                
        }

        public async Task DeleteBookFromCartAsync(int CartItem_ID)
        {
            var GetItemfromDatabase = await _dbcontext.Cart_Items.Where(x => x.CartItem_ID == CartItem_ID).FirstOrDefaultAsync();
            
            _dbcontext.Cart_Items.Remove(GetItemfromDatabase);

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> CheckQuantityAsync(int Book_ID , int Quantity , int customerId)
        {
            var Checking = await _dbcontext.Books.Where(x => x.Book_ID == Book_ID).FirstOrDefaultAsync();

            if (Checking.Stock - Quantity >= 0)
            {
                var cart = await _dbcontext.Shopping_Carts.Where(x => x.Customer_ID == customerId)
                    .OrderBy(x => x.Cart_ID).LastOrDefaultAsync();

                var cartItems = await GetCart_itemByCartIdAsync(cart.Cart_ID);

                var ShoppingCart = await _dbcontext.Cart_Items.Select(x => new { shoppingCart = x.Shopping_Cart, cartItem = x })
                    .Where(x => x.shoppingCart.Cart_ID == cartItems.Cart_ID && x.shoppingCart.Customer_ID == customerId)
                    .Where(x => x.cartItem.Book_ID == Book_ID)
                    .FirstOrDefaultAsync();

                ShoppingCart.cartItem.Price = (ShoppingCart.cartItem.Price / ShoppingCart.cartItem.Quantity) * Quantity;
                ShoppingCart.cartItem.Quantity = Quantity;

                await _dbcontext.SaveChangesAsync();
                return true;
            }
            else
            {

                return false;
            }

        }

        public async Task<int> getCountOfCartItemsAsync(int Cart_ID)
        {
            var items =  _dbcontext.Cart_Items
                            .Where(x => x.Shopping_Cart.Cart_ID == Cart_ID && x.Cart_ID == Cart_ID);

            
            return await items.CountAsync();


        }
        public async Task<Cart_Item> GetCart_itemByCartIdAsync(int Cart_ID)
        {
            return await _dbcontext.Cart_Items.Select(x => new { Cart = x.Shopping_Cart , item = x })
                    .Where(x => x.Cart.Cart_ID == Cart_ID && x.item.Cart_ID == Cart_ID)
                    .Select(x => x.item).OrderBy(x => x.Cart_ID).LastOrDefaultAsync();
            

        }
    }
}
