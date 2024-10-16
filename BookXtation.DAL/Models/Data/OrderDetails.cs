using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.Data
{
    public class OrderDetails
    {
        public int OrderDetails_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string street { get; set; }
        public int Build { get; set; }
        public int? Floor { get; set; }
        public int? Flat { get; set; }
        public string? Location { get; set; }
        public string? DistinctiveMark { get; set; }

        
        public int Order_ID { get; set; }
        public virtual Order Order { get; set; }
    }
}
