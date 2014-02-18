using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pureen.Web.Models
{
    public class JellieTestingModel
    {
        //great i forgot a tag
        [HiddenInput(DisplayValue = true)]
        public string Message { get; set; }
        [HiddenInput(DisplayValue = true)]
        public string Username { get; set; }
    }
}