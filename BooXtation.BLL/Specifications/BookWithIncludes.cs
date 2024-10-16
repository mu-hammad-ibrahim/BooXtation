using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Specifications
{
    public class BookWithIncludes : BaseSpecification<Book>
    {
        public BookWithIncludes()
        {
            Includes.Add(b => b.Author);
            Includes.Add(b => b.Publisher);
            Includes.Add(b => b.Category);
        }

        public BookWithIncludes(int id) : base(f => f.Book_ID == id)
        {
            Includes.Add(b => b.Author);
            Includes.Add(b => b.Publisher);
            Includes.Add(b => b.Category);
        }

    }
}
