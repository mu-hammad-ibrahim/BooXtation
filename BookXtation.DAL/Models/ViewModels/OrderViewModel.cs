using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.ViewModels
{
    public class OrderViewModel
    {
        public List<Order_ItemViewModel> _Items { get; set; }

        public OrderViewModel()
        {
            //Amount = TotalAmount;
            //_Items = new List<Order_ItemViewModel>();
        }
        public OrderViewModel(List<Order_ItemViewModel> Items)
        {
            _Items = new List<Order_ItemViewModel>();
            for (int i = 0; i < Items.Count; i++)
            {
                _Items.Add(Items[i]);
            }
        }
        public int Order_ID { get; set; }
        public int Customer_ID { get; set; }
        public Customer? customer { get; set; }

        public decimal TotalAmount { get; set; }

        
        //public decimal Amount ;
        public string OrderStatus { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public int CustomerDetails_ID { get; set; }
        public CustomerDetails? CustomerDetails { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        
    }
}
