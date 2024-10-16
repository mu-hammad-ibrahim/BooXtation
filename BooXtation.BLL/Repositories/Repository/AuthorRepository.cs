using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BooXtation.BLL.Repositories.Repository
{
	public class AuthorRepository : GenericReposiory<Author> , IAuthorRepository
	{
        public AuthorRepository(BooXtationContext dbcontext) : base(dbcontext)
        {
        }
        public async Task<int> Count()
        {
            return await _dbcontext.Authors.CountAsync();
        }
    }
  
}
