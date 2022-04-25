using App.Controllers;
using App.Models;
using App.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class InscriptionRace
    {
        public int Id { get; set; }
        public int age { get; set; }
        public int nbParticipants { get; set; }
    }
}