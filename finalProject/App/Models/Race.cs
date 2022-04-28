using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Race
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime EventDate { get; set; }
        public int Hour { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public int Place { get; set; } = 15;
        public string Image { get; set; }
        public int AgeLimit { get; set; } = 21;
        public virtual RaceResult Result { get; set; }
        public virtual List<Categorie> Categories { get; set; }

    }
}