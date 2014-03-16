using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pureen.Web.Models
{
    public class ShowContactMessageModel
    {
        public long MessageId { get; set; }
        public string UserName { get; set; }
        public string DateSent { get; set; }
        public string Heading { get; set; }
        public string Message { get; set; }
    }
}