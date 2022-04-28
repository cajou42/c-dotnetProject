namespace App.Models
{
    public class RaceResult
    {
        public int Id { get; set; }
        public virtual Race Race { get; set; }
        public virtual List<ResultLine> ?ResultLines { get; set; }

    }
}