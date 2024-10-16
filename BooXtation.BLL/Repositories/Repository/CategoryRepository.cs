using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BooXtation.BLL.Repositories.Repository
{
    public class CategoryRepository : GenericReposiory<Category>, ICategoryRepository
    {
        
        public CategoryRepository(BooXtationContext dbcontext) : base(dbcontext)
        {
        }
        public async Task<int> Count()
        {
            return await _dbcontext.Categories.CountAsync();
        }

    }
}