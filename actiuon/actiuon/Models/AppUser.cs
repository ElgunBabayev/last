using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Models
{
    public class AppUser: IdentityUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ProfilePhoto { get; set; }
        public bool IsActive { get; set; }
        public List<Product> Products { get; set; }
       

    }
}
