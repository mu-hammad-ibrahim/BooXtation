using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Interfaces
{
    public interface IFavouriteBooksRepository : IGenericRepository<FavouriteBooks>
    {
        Task<FavouriteBooks> SearchBookBycusID_bookID(int Book_ID, int Customer_ID);
        Task<IQueryable<FavouriteBooks>> GetFavourites(int Customer_ID);
    }
}
