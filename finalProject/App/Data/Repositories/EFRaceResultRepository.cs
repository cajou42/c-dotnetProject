using App.Data.Repositories;
using App.Models;

namespace App.Data.Repositories
{
    public class EFRaceResultRepository : IRaceResultRepository
    {
        private static EFRaceResultRepository instance;
        static EFRaceResultRepository GetInstance()
        {
            return instance ??= new EFRaceResultRepository();
        }
        private readonly AppDbContext _dbContext;

        public EFRaceResultRepository(AppDbContext dbContext) 
        {
            this._dbContext = dbContext;
        }
        public EFRaceResultRepository()
        {
        }

        public RaceResult Create(RaceResult model)
        {
            throw new NotImplementedException();
        }

        public RaceResult Find(int id)
        {
            return _dbContext.RaceResults.Single(raceResult => raceResult.Id == id);
        }

        public IEnumerable<RaceResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public RaceResult GetLastRaceResult()
        {
            // return _dbContext.RaceResults
            //         .Where(raceResult => raceResult.Race.EventDate > DateTime.Now)
            //         .OrderBy(raceResult => raceResult.Race.EventDate)
            //         .FirstOrDefault();
        
            return _dbContext.RaceResults.ToList()[0];

        }

        public RaceResult GetRaceResultWithWinnerDriver(int id)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }
    }
}