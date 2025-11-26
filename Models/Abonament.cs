using Microsoft.EntityFrameworkCore;
using mvc.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.data.models
{
    public class Abonament
    {
        public int id { get; set; }
        [ForeignKey("Users")]
        public string UserID { get; set; }
        public required string Tip_Abonament { get; set; }
        public required string Numar_Cafele { get; set; }
        public int Pret { get; set; }

        public required string Descriere { get; set; }

        public Users Users { get; set; }
    }
}
