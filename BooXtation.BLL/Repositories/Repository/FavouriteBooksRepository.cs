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
    public class FavouriteBooksRepository : GenericReposiory<FavouriteBooks>, IFavouriteBooksRepository
    {
        public FavouriteBooksRepository(BooXtationContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<IQueryable<FavouriteBooks>> GetFavourites(int Customer_ID)
        {
            return  _dbcontext.FavouriteBooks.Where(x => x.Customer_ID == Customer_ID)
                        .Include(x => x.Book).AsNoTracking();
        }

        public async Task<FavouriteBooks> SearchBookBycusID_bookID(int Book_ID, int Customer_ID)
        {
            var FavouriteItem = await _dbcontext.FavouriteBooks
                .Where(x => x.Book_ID == Book_ID && x.Customer_ID == Customer_ID).FirstOrDefaultAsync();

            return FavouriteItem == null ? null : FavouriteItem;
        }
    }
}
