using System.ComponentModel.DataAnnotations;

namespace mvc.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Message is required.")]
        public string message { get; set; }
        public DateTime timeSent { get; set; }
    }
}
