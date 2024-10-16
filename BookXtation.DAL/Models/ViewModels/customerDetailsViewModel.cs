using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.ViewModels
{
    public class customerDetailsViewModel
    {
        public int CustomerDetails_ID { get; set; }

        [Required(ErrorMessage = "Your First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Your Last Name is required.")]
        public string LastName { get; set; }

        [Phone]
        [RegularExpression(@"^01[0125]\d{8}$",
        ErrorMessage = "Invalid phone number format. Please enter a valid phone number.")]
        public string Phone { get; set; }

        public string City { get; set; }
        public string street { get; set; }
        public int Build { get; set; }
        public int? Floor { get; set; }
        public int? Flat { get; set; }
        public string? Location { get; set; }
        public string? DistinctiveMark { get; set; }

        public int Customer_ID { get; set; }
        public Customer? Customer { get; set; }
    }
}
