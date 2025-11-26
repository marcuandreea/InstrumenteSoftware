using Microsoft.AspNetCore.Identity;

namespace mvc.Models
{
    public class Users : IdentityUser
    {
        public string Tip_utilizator { get; set; }
    }
}
