using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFTest1.Models
{
    public class LoginVM
    {
        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

        public bool Authenticate()
        {
            Users users = new Users();

            users.Email = Email;
            users.Password = Password;

            return users.ChechUserAuth();
        }
    }
}