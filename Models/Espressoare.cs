namespace CoffeeShop.data.models
{
    public class Espressoare
    {
        public int id { get; set; }
        public required string Nume { get; set; }
        public required int Pret { get; set; }

        public required string Descriere { get; set; }

        public required string Stoc { get; set; }
    }
}
