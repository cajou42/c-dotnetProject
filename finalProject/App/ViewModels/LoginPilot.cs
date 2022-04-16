using System.ComponentModel.DataAnnotations;
namespace App.ViewModels
{
    public class LoginPilot
    {
        [Required(ErrorMessage = "Email requis")]
        public string? PilotEmail{ get; set; }
        [Required(ErrorMessage = "Mot de passe requis")]
        public string? Password { get; set; }
    }
}