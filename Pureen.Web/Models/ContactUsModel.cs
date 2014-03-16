using System.ComponentModel.DataAnnotations;

namespace Pureen.Web.Models
{
    public class ContactUsModel
    {
        [Required(ErrorMessage = "It needs a Heading")]
        public string Heading { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "It needs a Message")]
        public string Message { get; set; }
    }
}