using App.Models;

namespace App.data.Repositories
{
    public class EFRaceRepository : IRepository<Race>
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
    }
}