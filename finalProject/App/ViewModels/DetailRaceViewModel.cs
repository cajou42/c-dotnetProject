using App.Models;

namespace App.ViewModels
{
    public class DetailRaceViewModel
    {
        public Race Race{ get; set; }

        public DetailRaceViewModel(Race race) 
        {
            Race = race;
        }
    }
}