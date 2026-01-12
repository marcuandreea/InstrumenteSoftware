using Microsoft.EntityFrameworkCore;
using mvc.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc.Models
{
    public class Abonament
    {
        public int id { get; set; }
        public required string Tip_Abonament { get; set; }
        
    }
}
