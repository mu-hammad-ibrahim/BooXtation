using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.ViewModels
{
    public class PaymentRequestViewModel
    {
        public int Payment_ID { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string CardName { get; set; }
        public string __RequestVerificationToken { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Cvc { get; set; }

        [Required]
        public string ExpiryMonth { get; set; }

        [Required]
        public string ExpiryYear { get; set; }

        [Required]
        public decimal Amount { get; set; }


    }
}
