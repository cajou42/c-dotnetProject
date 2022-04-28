using System.ComponentModel.DataAnnotations;
namespace App.ViewModels
{
    public class LoginPilot
    {
        [Required(ErrorMessage = "Email requis")]
        public string? PilotEmail{ get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mot de passe requis")]
        public string? PPassword { get; set; }
    }
}