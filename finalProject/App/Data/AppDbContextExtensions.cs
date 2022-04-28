using App.Models;
using System.Security.Cryptography;
using System.Text;

namespace App.Data
{
    public static class AppDbContextExtentions
    {
        public static void Seed(this AppDbContext dbContext)
        {

            var car = new List<Car>()
            {
                new Car() 
                {
                    Id = 1,
                    Categorie = "Voiture",
                    ConstructionYear = new DateTime(2000,01,01),
                    Brand = "test",
                    Model = "alpha",
                    Power = 100,
                    NbPossession = 1,
                    Image = "test"
                },
                new Car() 
                {
                    Id = 2,
                    Categorie = "Camion",
                    ConstructionYear = new DateTime(2000,01,01),
                    Brand = "test",
                    Model = "beta",
                    Power = 100,
                    NbPossession = 1,
                    Image = "test"
                },                
                new Car() 
                {
                    Id = 3,
                    Categorie = "Tracteur",
                    ConstructionYear = new DateTime(2000,01,01),
                    Brand = "test",
                    Model = "omega",
                    Power = 100,
                    NbPossession = 1,
                    Image = "test"
                }
            };
            dbContext.Car.AddRange(car);

                        var race = new List<Race>()
            {
                new Race()
                {
                    Id = 1,
                    Name = "test",
                    EventDate = new DateTime(2022,05,01),
                    Place = 15,
                },
                
                new Race()
                {
                    Id = 2,
                    Name = "une bonne course",
                    EventDate = new DateTime(2022,06,01),
                    Place = 15,
                }
            };

            dbContext.Races.AddRange(race);

            HashAlgorithm sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes("test"));
            var str = System.Text.Encoding.Default.GetString(bytes);
            var pilot = new List<Pilot>()
            {
                new Pilot() 
                {
                    Id = 1,
                    FirstName = "Jean",
                    LastName = "Dupond",
                    BirthDay = new DateTime(1990,01,01),
                    Email = "Jean.Dupond@gmail.com",
                    Password = str,
                    Car = car[1],
                    Race = race[0]
                },
                new Pilot()
                {
                    Id = 2,
                    FirstName = "Sebastien",
                    LastName = "Clerc",
                    BirthDay = new DateTime(1990,01,01),
                    Email = "Sebastion.Clerc@gmail.com",
                    Password = "test",
                    Car = car[2],
                    Race = race[0]
                },
                new Pilot()
                {
                    Id = 3,
                    FirstName = "Francois",
                    LastName = "Bayrou",
                    BirthDay = new DateTime(1990,01,01),
                    Email = "Francois.Bayrou@gmail.com",
                    Password = "test",
                    Car = car[0],
                    Race = race[0]
                }
            };
            dbContext.Pilots.AddRange(pilot);


            var raceResult = new List<RaceResult>()
            {
                new RaceResult()
                {
                    Id = 1,
                    Race = race[0]
                }
            };
            dbContext.RaceResults.AddRange(raceResult);

            var resultLines = new List<ResultLine>()
            {
                new ResultLine()
                {
                    Id = 1,
                    Rank = 1,
                    Pilot = pilot[0],
                    RaceResult = raceResult[0]
                },
                new ResultLine()
                {
                    Id = 2,
                    Rank = 2,
                    Pilot = pilot[1],
                    RaceResult = raceResult[0]
                },
                new ResultLine()
                {
                    Id = 3,
                    Rank = 3,
                    Pilot = pilot[2],
                    RaceResult = raceResult[0]
                }
            };
            dbContext.ResultLines.AddRange(resultLines);
            dbContext.SaveChanges();
        }
    }
}