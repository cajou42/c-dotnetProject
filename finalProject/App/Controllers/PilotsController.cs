using Microsoft.AspNetCore.Mvc;
using App.Data;
using App.ViewModels;
using App.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Controllers
{
    public class PilotsController : Controller
    {
        private readonly AppDbContext _dbContext;
        public PilotsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
                    _dbContext.Pilots.Add(
                        new Pilot()
                        {
                            FirstName = pilot.PilotFirstName,
                            LastName = pilot.PilotLastName,
                            BirthDay = pilot.PilotBirthDay,
                            Email = pilot.PilotEmail,
                            Password = pilot.PilotPassword,
                            Car = GetRamdomCar(),
                        }
                        );
                        _dbContext.SaveChanges();
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
                Pilot pilot = _dbContext.Pilots.FirstOrDefault(p => p.Email == Lpilot.PilotEmail && p.Password == Lpilot.Password);
                Console.WriteLine(pilot.LastName);
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, pilot.Email),
                        new Claim(ClaimTypes.Name, pilot.FirstName + " " + pilot.LastName),
                        new Claim(ClaimTypes.DateOfBirth, pilot.BirthDay.ToString()),
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
                Pilot pilot = _dbContext.Pilots.FirstOrDefault(p => p.Email == HttpContext.User.FindFirst(ClaimTypes.Email).Value);
                pilot.FirstName = Edit.FirstName;
                _dbContext.SaveChanges();
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, pilot.Email),
                        new Claim(ClaimTypes.Name, Edit.FirstName + " " + pilot.LastName),
                        new Claim(ClaimTypes.DateOfBirth, pilot.BirthDay.ToString()),
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
                Pilot pilot = _dbContext.Pilots.FirstOrDefault(p => p.Email == HttpContext.User.FindFirst(ClaimTypes.Email).Value);
                pilot.LastName = Edit.LastName;
                _dbContext.SaveChanges();
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, pilot.Email),
                        new Claim(ClaimTypes.Name, pilot.FirstName + " " + Edit.LastName),
                        new Claim(ClaimTypes.DateOfBirth, pilot.BirthDay.ToString()),
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
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, _dbContext.Car.Count());
            Car car = _dbContext.Car.Find(randomNumber);
            return car;
        }
    }
}

