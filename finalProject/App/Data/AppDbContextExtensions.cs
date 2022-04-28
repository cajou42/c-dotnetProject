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

            var categories = new List<Categorie>()
            {
                new Categorie() 
                {
                    Id = 1,
                    Name = "Voiture"
                },
                new Categorie() 
                {
                    Id = 2,
                    Name = "Camion"
                },
                new Categorie() 
                {
                    Id = 3,
                    Name = "Tracteur"
                }
            };

            dbContext.Categories.AddRange(categories);
            var races = new List<Race>()
            {
                new Race()
                {
                    Id = 1,
                    Name = "Sonic Racing",
                    EventDate = new DateTime(2022,05,30),
                    Hour = new DateTime(2022,05,30).Hour,
                    Longitude = 13.3f,
                    Latitude = 43.7f,
                    Place = 15,
                    Image = "https://fs-prod-cdn.nintendo-europe.com/media/images/10_share_images/games_15/nintendo_switch_4/H2x1_NSwitch_TeamSonicRacing.jpg",
                    AgeLimit = 21,
                    Categories = categories
                },
                new Race()
                {
                    Id = 2,
                    Name = "Crash Bandicoot",
                    EventDate = new DateTime(2022,08,24),
                    Hour = new DateTime(2022,08,24).Hour,
                    Longitude = 13.3f,
                    Latitude = 43.7f,
                    Place = 15,
                    Image = "https://image.api.playstation.com/cdn/EP0002/CUSA07399_00/bPXMfRCtjGgvIS0BxOWqVsn94MAMLMkE.png",
                    AgeLimit = 21,
                    Categories = categories
                },
                new Race()
                {
                    Id = 3,
                    Name = "Mario Kart",
                    EventDate = new DateTime(2022,03,12),
                    Hour = new DateTime(2022,08,24).Hour,
                    Longitude = 13.3f,
                    Latitude = 43.7f,
                    Place = 15,
                    Image = "https://img.generation-nt.com/mario-kart-tour_05F4000001662811.jpg",
                    AgeLimit = 21,
                    Categories = categories
                },
                new Race()
                {
                    Id = 4,
                    Name = "Les fous du volant",
                    EventDate = new DateTime(2022,05,28),
                    Hour = new DateTime(2022,05,28).Hour,
                    Longitude = 13.3f,
                    Latitude = 43.7f,
                    Place = 15,
                    Image = "https://d2lv662meabn0u.cloudfront.net/boomerang/dynamic/show/00000000/795/6c17fb8340e5ed14c8eba597c48014bdf59fe484_1587660640.jpg",
                    AgeLimit = 21,
                    Categories = categories
                }
            };

            dbContext.Races.AddRange(races);

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
                    Race = races[0]
                },
                new Pilot()
                {
                    Id = 2,
                    FirstName = "Sebastien",
                    LastName = "Clerc",
                    BirthDay = new DateTime(1990,01,01),
                    Email = "Sebastion.Clerc@gmail.com",
                    Password = str,
                    Car = car[2],
                    Race = races[0]
                },
                new Pilot()
                {
                    Id = 3,
                    FirstName = "Francois",
                    LastName = "Bayrou",
                    BirthDay = new DateTime(1990,01,01),
                    Email = "Francois.Bayrou@gmail.com",
                    Password = str,
                    Car = car[0],
                    Race = races[0]
                }
            };
            dbContext.Pilots.AddRange(pilot);



            var raceResult = new List<RaceResult>()
            {
                new RaceResult()
                {
                    Id = 1,
                    Race = races[2],

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