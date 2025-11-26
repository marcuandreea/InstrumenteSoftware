namespace CoffeeShop.data.models
{
    public class Tipuri_Cafea
    {
        public int id { get; set; }
        public required string Denumire { get; set; }
        public required string Origine { get; set; }
        public required string Prajire { get; set; }
        public required int Pret { get; set; }
    }
}
