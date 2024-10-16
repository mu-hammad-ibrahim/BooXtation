using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookXtation.DAL.Models.Data
{
    public class ApplicationUser : IdentityUser
    {    
        public DateTime? CreatedAt { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
