using App.Data;
using App.Models;

namespace App.Data.Repositories
{
    public class EFRaceRepository : IRaceRepository
    {
        private static EFRaceRepository instance;
        static EFRaceRepository GetInstance()
        {
            return instance ??= new EFRaceRepository();
        }
        private readonly AppDbContext _dbContext;

        public EFRaceRepository(AppDbContext dbContext) 
        {
            this._dbContext = dbContext;
        }

        public EFRaceRepository()
        {
        }

        public Race Create(Race model)
        {
            return _dbContext.Races.Add(model).Entity;
        }

        public Race Find(int id)
        {
            return _dbContext.Races.Single(race => race.Id == id);
        }

        public IEnumerable<Race> GetAll()
        {
            return _dbContext.Races.ToList();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        
        public IEnumerable<Race> ThreeNextRaces() 
        {
            return (from race in _dbContext.Races
                where race.EventDate > DateTime.Now
                orderby race.EventDate
                select race).Take(3);
        }

        public Race LastRace()
        {
            return _dbContext.Races
                    .Where(race => race.EventDate > DateTime.Now)
                    .OrderBy(race => race.EventDate)
                    .FirstOrDefault();
        }
    }
}