namespace App.ViewModels
{
    public class ProfilePilot
    {
        public string? PilotFirstName { get; set; }
        public string? PilotLastName { get; set; }
        public string? PilotMail{ get; set; }
        public DateTime PilotBirthDay { get; set; }
        // public ProfilePilot(string name, string mail, string lastName, DateTime birthDay)
        // {
        //     PilotFirstName = name;
        //     PilotLastName = lastName;
        //     PilotMail = mail;
        //     PilotBirthDay = birthDay;
        // }
    }
}