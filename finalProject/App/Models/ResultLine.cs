namespace App.Models
{
    public class ResultLine
    {
        public int Id { get; set; } 
        public int Rank { get; set; }
        public Pilot Pilot { get; set; }
        public RaceResult RaceResult { get; set; }
    }
}