using App.Data;
using App.Models;
namespace App.Data.Repositories
{
    public class EFPilotRepository : IPilotRepository
    {
        private static EFPilotRepository instance;
        static EFPilotRepository GetInstance()
        {
            return instance ??= new EFPilotRepository();
        }
        private readonly AppDbContext _dbContext;

        public EFPilotRepository(AppDbContext dbContext) 
        {
            this._dbContext = dbContext;
        }

        public EFPilotRepository()
        {
        }

        public Pilot Create(Pilot model)
        {
            return _dbContext.Pilots.Add(model).Entity;
        }

        public Pilot Find(int id)
        {
            return _dbContext.Pilots.Single(Pilot => Pilot.Id == id);
        }

        public IEnumerable<Pilot> GetAll()
        {
            return _dbContext.Pilots.ToList();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        public Pilot GetPilotWithEmailAndPassword(String email, String password)
        {
            return _dbContext.Pilots.SingleOrDefault(pilot => pilot.Email == email && pilot.Password == password);
        }
        public Pilot GetPilotWithBirthday(DateTime birthday) 
        {
            return _dbContext.Pilots.SingleOrDefault(pilot => pilot.BirthDay == birthday);
        }
    }
}