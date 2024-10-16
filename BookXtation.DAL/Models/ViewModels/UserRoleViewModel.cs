using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<IdentityUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public Dictionary<IdentityUser, IList<string>> UserRoles { get; set; }

    }
}
