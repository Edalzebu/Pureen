using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pureen.Web.Models
{
    public class AccountPermissionsModel
    {
        [Display(Name = "Email Adress")]
        public bool Email { get; set; }
        [Display(Name = "Facebook")]
        public bool ShowFacebook { get; set; }
        [Display(Name = "Twitter")]
        public bool ShowTwitter { get; set; }
        [Display(Name = "Full Name")]
        public bool ShowName { get; set; }
    }
}