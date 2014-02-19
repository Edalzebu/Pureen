using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc.Controls;

namespace Pureen.Web.Models
{
    public class ListNewsModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Sir.. Sir! a New without heading!")]
        public string Heading { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Sir.. Sirr.. a New without information!")]
        public string Information { get; set; }
        [Display(Name = "Replies Enabled?")]
        public bool CommentsEnabled { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string UserName { get; set; }

    }
}