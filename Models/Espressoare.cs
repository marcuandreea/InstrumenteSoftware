namespace mvc.Models
{
    public class Espressoare
    {
        public int id { get; set; }
        public required string Nume { get; set; }
        public required int Pret { get; set; }
        public required string Cod { get; set; }
        public required string Descriere { get; set; }
        public required string Stoc { get; set; }
        public required string Tip { get; set; }
        public string? Poza { get; set; }
        public string? PozaStanga { get; set; }
        public string? PozaDreapta { get; set; }
        public string? Video { get; set; }
    }
}