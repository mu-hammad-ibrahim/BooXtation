using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Specifications
{
    public class BookSpecParams
    {
        private int pageSize = 24;

        public int PageIndex { get; set; } = 1;
        public int PageSize
        {

            get { return pageSize; }

            set { pageSize = value > 100 ? 10 : value; }

        }
        public string? Search { get; set; }
        public string? Sort { get; set; }
        public List<int>? AuthorIds { get; set; } = new List<int>();
        public List<int>? CategoryIds { get; set; } = new List<int>();
        public List<int>? PublisherIds { get; set; } = new List<int>();

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
