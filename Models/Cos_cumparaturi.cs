using mvc.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.data.models
{
    public class Cos_cumparaturi
    {
        public int id { get; set; }
        [ForeignKey("Users")]
        public string UserID { get; set; }
        [ForeignKey("Tipuri_Cafea")]
        public int IDCafea { get; set; }
        [ForeignKey("Espressoare")]
        public int CodEspressor { get; set; }

        public int Cantitate { get; set; }

        public Users Users { get; set; }
        public Tipuri_Cafea Tipuri_Cafea { get; set; }
        public Espressoare Espressoare { get; set; }



    }
}
