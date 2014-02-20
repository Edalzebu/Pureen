using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pureen.Web.Models
{
    public class MakeaCommentModel
    {
        [DataType(DataType.MultilineText)]
        [Display(Name = "Make a Comment!")]
        public string Information { get; set; }
        [HiddenInput(DisplayValue = false)]
        public long NewId { get; set; }
    }
}