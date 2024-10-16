using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Interfaces
{
	public interface IAuthorRepository : IGenericRepository<Author>
	{
        Task<int> Count();

    }
}
