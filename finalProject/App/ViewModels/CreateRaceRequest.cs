using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class CreateRaceRequest
    {
        [Required(ErrorMessage = "Nom requis pour la course")]
        [MaxLength(30, ErrorMessage = "Trop long")]
        [MinLength(3, ErrorMessage = "Nom trop court ! Min : 3")]
        public string? RaceName { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime RaceEventDate { get; set; }

        [Required(ErrorMessage = "il faut un nombre de participants")]
        [Range(10, 30, ErrorMessage = "Nombre de participants invalide")]
        public int RacePlace { get; set; }
    }
}