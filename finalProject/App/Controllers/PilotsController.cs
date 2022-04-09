using Microsoft.AspNetCore.Mvc;
using App.Data;
using App.ViewModels;
using App.Models;
using Microsoft.AspNetCore.Http;

namespace App.Controllers
{
    public class PilotsController : Controller
    {
        const string SessionName = "_Name";  
        const string SessionAge = "_Age";  
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
            //HttpContext.Session.GetInt32("Id")
            Pilot pilot = _dbContext.Pilots.Find(1);
            var pilotProfile = new ProfilePilot(
                pilot.FirstName,
                pilot.Email
            );
            ISession session = HttpContext.Session;
            // var val = HttpContext.User.Identity.Name;
            HttpContext.Session.SetString(SessionName, pilot.FirstName);  
            HttpContext.Session.SetInt32(SessionAge, 24);  
            return View("ProfilePilot", pilotProfile);
        }
    }
}

