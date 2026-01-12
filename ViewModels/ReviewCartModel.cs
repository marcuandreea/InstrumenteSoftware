using System.Collections.Generic;
using mvc.Models;

namespace mvc.ViewModels
{
    public class ReviewViewModel
    {
        public List<Review> Review { get; set; }
        public List<Users> Users { get; set; }
    }
}
