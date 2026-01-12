namespace mvc.Models
{
    public class Tipuri_Cafea
    {
        public int id { get; set; }
        public required string Nume { get; set; }
        public required int Pret { get; set; }
        public required int Rating { get; set; }
        public string? Poza { get; set; }
    }
}