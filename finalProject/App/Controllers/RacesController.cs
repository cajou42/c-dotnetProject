// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using App.Models;
// using App.ViewModels;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using App.Data;
// using App.Data.Repositories;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using System.Security.Claims;


// namespace App.Controllers
// {
//     public class RacesController : Controller
//     {
//         private readonly IRepository<Race> _raceRepository;
        
//         public RacesController(IRepository<Race> raceRepository)
//         {
//         _raceRepository = raceRepository;
//         }
//         private readonly IRepository<Race> _raceRepository;

//         public RacesController(IRepository<Race> raceRepository, AppDbContext dbContext)
//         {
//             _raceRepository = raceRepository;
//         }

//         public ActionResult List()
//         {
//             var races = _raceRepository.GetAll();


//             var raceListViewModel = new RaceListViewModel(
//                 races,
//                 "Liste de courses"
//             );

//             return View("RaceList", raceListViewModel);
//         }

//         // GET: Races/Details/5
//         public ActionResult Details(int id, string toto)
//         {

//             return View();
//         }

//         // GET: Races/Create
//         [HttpGet]
//         public ActionResult Create()
//         {
//             return View("CreateRace");
//         }

//         // POST: Races/Create
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public ActionResult Create(CreateRaceRequest race)
//         {
//             try
//             {
//                 if (race.RaceEventDate.DayOfWeek == DayOfWeek.Tuesday)
//                 {
//                     ModelState.AddModelError(String.Empty, "No race allowed on Tuesdays !");
//                 }

//                 if (ModelState.IsValid)
//                 {
//                     _raceRepository.Create(
//                         new Race()
//                         {
//                             Name = race.RaceName,
//                             EventDate = race.RaceEventDate
//                         }
//                         );
//                         _raceRepository.Save();

//                     return RedirectToAction(nameof(List));
//                 }

//                 return View("CreateRace");

//             }
//             catch
//             {
//                 return View("CreateRace");
//             }
//         }

//         // GET: Races/Edit/5
//         public ActionResult Edit(int id)
//         {
//             return View();
//         }

//         // POST: Races/Edit/5
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public ActionResult Edit(int id, IFormCollection collection)
//         {
//             try
//             {
//                 // TODO: Add update logic here

//                 return RedirectToAction(nameof(Index));
//             }
//             catch
//             {
//                 return View();
//             }
//         }

//         // GET: Races/Delete/5
//         public ActionResult Delete(int id)
//         {
//             return View();
//         }

//         // POST: Races/Delete/5
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public ActionResult Delete(int id, IFormCollection collection)
//         {
//             try
//             {
//                 // TODO: Add delete logic here

//                 return RedirectToAction(nameof(Index));
//             }
//             catch
//             {
//                 return View();
//             }
//         }
        
//         // GET: Races/Inscription/id
//         [HttpGet]
//         public ActionResult Inscription(int id)
//         {
//             return View("InscriptionRace");
//         }

//         // POST: Races/Inscription/id
//         [HttpPost]
//         public ActionResult Inscription(int id, IFormCollection collection){
//             ClaimsPrincipal currentUser = this.User;
//             var birth = DateTime.Parse(currentUser.FindFirst(ClaimTypes.DateOfBirth).Value);
//             var user = _dbContext.Pilots.FirstOrDefault(u => u.BirthDay == birth);
//             var today = DateTime.Today;
//             Race race = _raceRepository.Find(id);
//             var age = today.Year - birth.Year;
//             try
//             {
//                 if(age < 18){
//                     ModelState.AddModelError(nameof(InscriptionRace.age), "You must be 18 years old to participate in a race");
//                     return View("InscriptionRace");
//                 }
//                 if(race.Place == 0){
//                     ModelState.AddModelError(nameof(InscriptionRace.nbParticipants), "the Race is complete");
//                     return View("InscriptionRace");
//                 }
//                 else
//                 {
//                     race.Place --;
//                     user.Race = race;
//                     _raceRepository.Save();
//                     _dbContext.SaveChanges();
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             catch
//             {
//                 return View("InscriptionRace");
//             }
//         }
//     }
// }