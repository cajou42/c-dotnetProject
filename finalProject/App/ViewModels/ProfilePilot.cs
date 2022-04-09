namespace App.ViewModels
{
    public class ProfilePilot
    {
        public int? Id { get; set; }
        public string? PilotFirstName { get; set; }
        public string? PilotMail{ get; set; }

        public ProfilePilot(string name, string mail)
        {
            PilotFirstName = name;
            PilotMail = mail;
        }
    }
}