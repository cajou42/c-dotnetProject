using Microsoft.AspNetCore.Mvc;
using App.Data;
using App.Data.Repositories;
using App.ViewModels;
using App.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
//using App.Models.ClaimTypesModels;
using System.Security.Cryptography;
using System.Text;

namespace App.Controllers
{
    public class PilotsController : Controller
    {
        private readonly IPilotRepository _pilotRepository;
        private readonly IRaceRepository _raceRepository;
        private readonly IRepository<Car> _CarRepository;

        private HashAlgorithm sha = SHA256.Create();
        public PilotsController(IPilotRepository pilotRepository, IRaceRepository raceRepository, ICarRepository CarRepository)
        {
            _pilotRepository = pilotRepository;
            _raceRepository = raceRepository;
            _CarRepository = CarRepository;
        }

        // GET: Pilots/Register
        [HttpGet]
        public ActionResult Register(){
            return View("RegisterPilot");
        }

        // POST: Pilots/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterPilot pilot)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pilot.PilotPassword));
                    var hashPassword = System.Text.Encoding.Default.GetString(bytes);
                    _pilotRepository.Create(
                        new Pilot()
                        {
                            FirstName = pilot.PilotFirstName,
                            LastName = pilot.PilotLastName,
                            BirthDay = pilot.PilotBirthDay,
                            Email = pilot.PilotEmail,
                            Password = hashPassword,
                            Car = GetRamdomCar(),
                            Race =  _raceRepository.Find(1),
                        }
                        );
                        _pilotRepository.Save();
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                return View("RegisterPilot");
            }
            catch
            {
                return View("RegisterPilot");
            }
        }

        // GET: Pilots/Profile
        [HttpGet]
        public ActionResult Profile(){
            ViewBag.Email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            ViewBag.Name = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            ViewBag.BirthDay = HttpContext.User.FindFirst(ClaimTypes.DateOfBirth).Value;
            ViewBag.Car = HttpContext.User.FindFirst("Car").Value;
            ViewBag.RaceInscription = HttpContext.User.FindFirst("InscriptionRace").Value;
            return View("ProfilePilot");
        }


        // GET: Pilots/Login
        [HttpGet]
        public ActionResult Login(){
            return View("LoginPilot");
        }

        //POST: Pilots/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginPilot Lpilot){

            if(Lpilot == null){
                return View("LoginPilot");
            }
            try
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(Lpilot.PPassword));
                var hashPassword = System.Text.Encoding.Default.GetString(bytes);
                Pilot pilot = _pilotRepository.GetPilotWithEmailAndPassword(Lpilot.PilotEmail, hashPassword);
                Console.WriteLine(pilot.Car);
                //pilot.Car = GetRamdomCar();
                pilot.Race = _raceRepository.Find(1);
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, pilot.Email),
                        new Claim(ClaimTypes.Name, pilot.FirstName + " " + pilot.LastName),
                        new Claim(ClaimTypes.DateOfBirth, pilot.BirthDay.ToString()),
                        new Claim("Car", pilot.Car.Model),
                        new Claim("InscriptionRace", pilot.Race.Name),
                        new Claim("Hash", hashPassword)
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch
            {
                return View("LoginPilot");
            }
        }

        // GET: Pilots/Edit
        [HttpGet]
        public ActionResult Edit(){
            ViewBag.Name = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            return View("EditPilot");
        }

        // POST: Pilots/Edit
        [HttpPost]
        public async Task<ActionResult> EditName(EditPilot Edit)
        {
            try
            {
                Pilot pilot = _pilotRepository.GetPilotWithEmailAndPassword(HttpContext.User.FindFirst(ClaimTypes.Email).Value, HttpContext.User.FindFirst("Hash").Value);
                pilot.FirstName = Edit.FirstName;
                _pilotRepository.Save();
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, pilot.Email),
                        new Claim(ClaimTypes.Name, Edit.FirstName + " " + pilot.LastName),
                        new Claim(ClaimTypes.DateOfBirth, pilot.BirthDay.ToString()),
                        new Claim("Car", pilot.Car.Model),
                        new Claim("InscriptionRace", pilot.Race.Name),
                        new Claim("Hash", HttpContext.User.FindFirst("Hash").Value)
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(claimsIdentity));
                return View("EditPilot");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditLastName(EditPilot Edit)
        {
            try
            {
                Pilot pilot = _pilotRepository.GetPilotWithEmailAndPassword(HttpContext.User.FindFirst(ClaimTypes.Email).Value, HttpContext.User.FindFirst("Hash").Value);                pilot.LastName = Edit.LastName;
                _pilotRepository.Save();
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, pilot.Email),
                        new Claim(ClaimTypes.Name, pilot.FirstName + " " + Edit.LastName),
                        new Claim(ClaimTypes.DateOfBirth, pilot.BirthDay.ToString()),
                        new Claim("Car", pilot.Car.Model),
                        new Claim("InscriptionRace", pilot.Race.Name),
                        new Claim("Hash", HttpContext.User.FindFirst("Hash").Value)
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(claimsIdentity));
                return View("EditPilot");
            }
            catch
            {
                return View();
            }
        }

        public Car GetRamdomCar(){
            var cars = _CarRepository.GetAll();
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, cars.Count());
            Car car = cars.FirstOrDefault(c => c.Id == randomNumber);
            return car;
        }
    }
}

