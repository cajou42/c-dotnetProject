using App.Models;

namespace App.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Race> RaceList { get; }
        public Race LastRace { get; }

        public string TimeNextRace { get; }
        public HomeViewModel(IEnumerable<Race> races, Race lastRace, string timeNextRace ) 
        {
            RaceList = races;
            LastRace = lastRace;
            TimeNextRace = timeNextRace;
        }
    }
}