using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Pureen.Web.Models
{
    public class AccountProfileModel
    {
        public Image ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime MemberSince { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
    }
}