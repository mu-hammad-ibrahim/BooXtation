using BookXtation.DAL.Models.Data;
using BookXtation.DAL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Interfaces
{
    public interface ICart_ItemRepository : IGenericRepository<Cart_Item>
    {
        Task AddBookToCartAsync(int Book_ID , int customerId , int cart_ID, int quantity = 1);
        Task<Cart_Item> GetCart_itemByCartIdAsync(int Cart_ID);
        Task<IQueryable<Cart_Item>> GetAllCartItemsWithBooks(int customerId);
        Task DeleteBookFromCartAsync (int CartItem_ID);
        Task<bool> CheckQuantityAsync (int Book_ID , int quantity , int customerId);

        Task<int> getCountOfCartItemsAsync(int Cart_ID);
        
    }
}
