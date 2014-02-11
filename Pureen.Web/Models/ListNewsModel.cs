using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pureen.Web.Models
{
    public class ListNewsModel
    {
        public long Id { get; set; }
        public string Heading { get; set; }
        public string Information { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}