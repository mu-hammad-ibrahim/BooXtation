using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalBooks { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalOrders { get; set; }
        public int TotalPublishers { get; set; }
        public int TotalCategories { get; set; }
        public int TotalCustomers { get; set; }
        public List<string> OrderDates { get; set; } = new List<string>();
        public List<int> OrderCounts { get; set; } = new List<int>();
        public List<string> CustomerNames { get; set; } = new List<string>();
        public List<int> CustomerOrderCounts { get; set; } = new List<int>();
        public List<string> CustomerCreationDates { get; set; } = new List<string>();
        public List<int> CustomerCreationCounts { get; set; } = new List<int>();
    }

}
