using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.ViewModels
{
    public class Cart_ItemViewModel
    {

        public int CartItem_ID { get; set; }

        public int Cart_ID { get; set; }
        public Shopping_Cart Shopping_Cart { get; set; }

        public int Book_ID { get; set; }
        public Book? book { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
