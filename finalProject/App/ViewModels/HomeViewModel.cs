using App.Models;

namespace App.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Race> RaceList { get; }
        public Race LastRace { get; }
        public HomeViewModel(IEnumerable<Race> races, Race lastRace) 
        {
            RaceList = races;
            LastRace = lastRace;
        }
    }
}