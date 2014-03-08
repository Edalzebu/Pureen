using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pureen.Web.Models
{
    public class ShowToolbarModel
    {
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }
    }
}