namespace App.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string? Categorie { get; set; }
        public DateTime ConstructionYear { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Power { get; set; }
        public int NbPossession { get; set; }
        public string? Image { get; set; }
    }
}