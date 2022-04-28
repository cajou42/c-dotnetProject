using App.Models;

namespace App.Data.Repositories
{
    public interface IRepository<TModel> where TModel : class
    {

        TModel Find(int id); 
        IEnumerable<TModel> GetAll(); 
        TModel Create(TModel model);

        int Save();

    }
    public interface IRaceResultRepository : IRepository<RaceResult>
    {
        RaceResult GetRaceResultWithWinnerDriver(int id);
        RaceResult GetLastRaceResult();
    }

    public interface IRaceRepository : IRepository<Race>
    {
        IEnumerable<Race> ThreeNextRaces();
        Race LastRace();
    }
    public interface IPilotRepository : IRepository<Pilot>
    {
        Pilot GetPilotWithEmailAndPassword(String email, String password);
        Pilot GetPilotWithBirthday(DateTime birthday);
    }


}