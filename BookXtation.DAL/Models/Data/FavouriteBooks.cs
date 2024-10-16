using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.Data
{
    public class FavouriteBooks
    {
        public int Favourit_ID { get; set; }

        public int Customer_ID { get; set; }
        public virtual Customer Customer { get; set; }

        public int Book_ID { get; set; }
        public virtual Book Book { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
