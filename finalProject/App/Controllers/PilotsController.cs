using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class PilotsController : Controller
    {
        
        public ActionResult Register(){
            return View("PilotRegister");
        }
    }
}