namespace App.Models
{
    public class Pilot
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Car? Car { get; set; }

    }
}