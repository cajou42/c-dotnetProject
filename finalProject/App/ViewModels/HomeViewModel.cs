using App.Models;

namespace App.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Race> RaceList { get; }

        public string TimeNextRace { get; }

        public RaceResult LastRaceResult { get; }
        public HomeViewModel(IEnumerable<Race> races, string timeNextRace, RaceResult lastRaceResult ) 
        {
            RaceList = races;
            LastRaceResult =  lastRaceResult;
            TimeNextRace = timeNextRace;
            
        }
    }
}