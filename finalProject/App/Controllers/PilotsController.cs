using Microsoft.AspNetCore.Mvc;
using App.Data;
using App.ViewModels;
using App.Models;

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
                        }
                        );
                        _dbContext.SaveChanges();

                    return RedirectToAction(nameof(HomeController));
                }

                return View("RegisterPilot");

            }
            catch
            {
                return View("RegisterPilot");
            }
        }
    }
}