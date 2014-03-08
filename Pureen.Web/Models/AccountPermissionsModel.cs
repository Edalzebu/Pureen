using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pureen.Web.Models
{
    public class AccountPermissionsModel
    {
        public bool ShowEmail { get; set; }
        public bool ShowFacebook { get; set; }
        public bool ShowTwitter { get; set; }
        public bool ShowName { get; set; }
    }
}