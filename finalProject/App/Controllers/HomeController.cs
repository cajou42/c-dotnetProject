using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.data.Repositories;
using App.ViewModels;

namespace App.Controllers;

public class HomeController : Controller
{
    private readonly IRaceRepository _raceRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,IRaceRepository raceRepository)
    {
        _logger = logger;
        _raceRepository = raceRepository;
    }

    public IActionResult Index()
    {
        var races = _raceRepository.ThreeNextRaces();
        var lastRace = _raceRepository.LastRace();

        var HomeViewModel = new HomeViewModel(
            races,
            lastRace
        );

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
