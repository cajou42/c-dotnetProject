namespace App.Models
{
    public class ResultLine
    {
        public int Id { get; set; } 
        public int Rank { get; set; }
        public virtual Pilot Pilot { get; set; }
        public virtual RaceResult RaceResult { get; set; }
    }
}