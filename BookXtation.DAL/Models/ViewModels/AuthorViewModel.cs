using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.ViewModels
{
	public class AuthorViewModel
	{
		public int Author_ID { get; set; }

		[Required(ErrorMessage = "Author name is required.")]
		[MaxLength(100, ErrorMessage = "Author name cannot exceed 100 characters.")]
		public string Name { get; set; }

		// You can add other properties if necessary, for example, if the view model needs to include a list of books or other related data.
		
	}
}
