using App.Data;
using App.Models;
namespace App.Data.Repositories
{
    public class EFCarRepository : ICarRepository
    {
        private static EFCarRepository instance;
        static EFCarRepository GetInstance()
        {
            return instance ??= new EFCarRepository();
        }
        private readonly AppDbContext _dbContext;

        public EFCarRepository(AppDbContext dbContext) 
        {
            this._dbContext = dbContext;
        }

        public EFCarRepository()
        {
        }

        public Car Find(int id)
        {
            return _dbContext.Car.Single(race => race.Id == id);
        }

        public IEnumerable<Car> GetAll()
        {
            return _dbContext.Car.ToList();
        }

        public Car Create(Car model)
        {
            return _dbContext.Car.Add(model).Entity;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        public string Test()
        {
            return "Test";
        }
    }
}