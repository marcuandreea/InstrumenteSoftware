using System.ComponentModel.DataAnnotations.Schema;

namespace mvc.Models
{
    public class Cos_cumparaturi
    {
        public int id { get; set; }

        [ForeignKey("Users")]
        public string UserID { get; set; }
        public Users? Users { get; set; }


        [ForeignKey("Tipuri_Cafea")]
        public int? IDCafea { get; set; }
        public Tipuri_Cafea? Tipuri_Cafea { get; set; }


        [ForeignKey("Espressoare")]
        public int? CodEspressor { get; set; }
        public Espressoare? Espressoare { get; set; }



    }
}
