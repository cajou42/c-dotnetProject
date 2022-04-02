using System.ComponentModel.DataAnnotations;
namespace App.ViewModels
{
    public class RegisterPilot
    {
        [Required(ErrorMessage = "Prénom requis pour le pilote")]
        [MaxLength(30, ErrorMessage = "Trop long")]
        [MinLength(3, ErrorMessage = "Prénom trop court ! Min : 3")]
        public string? PilotFirstName { get; set; }

        [Required(ErrorMessage = "Nom requis pour le pilote")]
        [MaxLength(30, ErrorMessage = "Trop long")]
        [MinLength(3, ErrorMessage = "Nom trop court ! Min : 3")]
        public string? PilotLastName { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime PilotBirthDay { get; set; }

        [Required(ErrorMessage = "Email requis pour le pilote")]
        [MaxLength(30, ErrorMessage = "Trop long")]
        public string? PilotEmail { get; set; }

        [Required(ErrorMessage = "Mot de passe requis pour le pilote")]
        [MinLength(8, ErrorMessage = "Nom trop court ! Min : 8")]
        public string? PilotPassword { get; set; }
    }
}