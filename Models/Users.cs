using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc.Models
{
    public class Users : IdentityUser
    {
        public string Tip_utilizator { get; set; }

        [ForeignKey("Abonament")]
        public int ID_abonament { get; set; }
        public Abonament? Abonament { get; set; }
        public string? photoPath { get; set; }

    }
}
