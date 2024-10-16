using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Interfaces
{
    public interface IShopping_CartRepository : IGenericRepository<Shopping_Cart>
    {
        Task<int> createNewCartAsync(int customerId);
        Task createNewCartAfterPlaceOrderAsync(int customerId);
        Task<Shopping_Cart> getShoppingCartByCustIdAsync(int customerId);

        Task<IQueryable<Cart_Item>> getShoppingCartItems(int customerId);
    }
}
