using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommentoIntegrationTest.Models
{
    public class UserRegistration
    {
        public string Name { get; set; }

        public int Age { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and confirmation dont match")]
        public string ConfirmPassword { get; set; }
    }
}
