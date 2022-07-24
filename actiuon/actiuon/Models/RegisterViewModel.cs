using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace actiuon.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(200)]
        public string UserName { get; set; }
        [Required, MaxLength(200)]
        public string LastName { get; set; }
        [Required, MaxLength(200)]
        public string FirstName { get; set; }
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public IFormFile ProfImg { get; set; }
    }
}
