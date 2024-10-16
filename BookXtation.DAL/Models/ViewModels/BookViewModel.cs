using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookXtation.DAL.Models.ViewModels
{
    public class BookViewModel
    {
        public int Book_ID { get; set; }

        [Display(Name = "Book Title")]
        [Required(ErrorMessage = "Title Is Required")]
        [MaxLength(60, ErrorMessage = "Max Length Of Title Is 60 Chars")]
        [MinLength(2, ErrorMessage = "Minimum Length Of Title Is 2 Chars")]
        public string Title { get; set; }

        [Display(Name = "Book Summary")]
        [Required(ErrorMessage = "Book Summary Is Required")]
        [MaxLength(200, ErrorMessage = "Max Length Of Title Is 200 Chars")]
        [MinLength(5, ErrorMessage = "Minimum Length Of Title Is 5 Chars")]
        public string Summary { get; set; }


        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "ISBN Is Required")]
        public string ISBN { get; set; }

        public decimal Price
        {
            get => ActualPrice - (ActualPrice * (Discount / 100));
        }

        [Display(Name = "Actual Price")]
        [Required(ErrorMessage = "Actual Price Is Required")]
        [Range(0, double.MaxValue, ErrorMessage = "Actual Price must be a positive value.")]
        public decimal ActualPrice { get; set; }

        [Display(Name = "Discount (%)")]
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Stock Count Is Required")]
        public int Stock { get; set; }

        [Display(Name = "Number Of Book Pages")]
        [Required(ErrorMessage = "Number Of Pages Is Required")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "Language Is Required")]

        public string Language { get; set; }

        public DateTime? Publish_Date { get; set; }

        public string? Cover_Image { get; set; }

        [Display(Name = "Book Cover Image")]
        public IFormFile? Cover_Image_File { get; set; }

        [Display(Name = "Choose Book Publisher")]
        [Required(ErrorMessage = "Please Choose The Publisher Name Or Add A New One")]
        public int Publisher_ID { get; set; }

        [Display(Name = "Choose Book Author")]
        [Required(ErrorMessage = "Please Choose The Author Name Or Add A New One")]
        public int Author_ID { get; set; }

        [Display(Name = "Choose Book Category")]
        [Required(ErrorMessage = "Please Choose The Category Name Or Add A New One")]
        public int Category_ID { get; set; }
    }
}
