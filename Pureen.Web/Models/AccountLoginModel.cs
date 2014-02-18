using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pureen.Web.Models
{
    public class AccountLoginModel
    {
        [Required(ErrorMessage = "Sir, Sir pls enter your Email/Username")]
        [Display(Name = "Username / Email")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}