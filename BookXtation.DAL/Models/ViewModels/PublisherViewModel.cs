using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.ViewModels
{
    public class PublisherViewModel
    {
        public int Publisher_ID { get; set; }
        public string Name { get; set; }
        public string? Contact { get; set; }

        public string? Address { get; set; }
    }
}
