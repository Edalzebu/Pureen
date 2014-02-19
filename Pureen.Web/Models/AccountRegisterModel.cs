using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pureen.Web.Models
{
    public class AccountRegisterModel
    {
        public long Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please enter your user name.")]
        public string Username { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Please enter your first name.")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string Lastname { get; set; }

        [Display(Name = "E-mail Address")]
        [Required(ErrorMessage = "Please enter your valid E-mail address.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This should match your password.")]
        [Display(Name = "Confirm Password")]

        public string ConfirmPassword { get; set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
    }
}