using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Data.Repositories;
using App.ViewModels;

namespace App.Controllers;

public class HomeController : Controller
{
    private readonly IRaceRepository _raceRepository;
    private readonly ILogger<HomeController> _logger;
    private readonly IRaceResultRepository _raceResultRepository;

    public HomeController(ILogger<HomeController> logger,IRaceRepository raceRepository, IRaceResultRepository raceResultRepository)
    {
        _logger = logger;
        _raceRepository = raceRepository;
        _raceResultRepository = raceResultRepository;
    }

    public IActionResult Index()
    {
        var races = _raceRepository.ThreeNextRaces();
        var lastRaceResult = _raceResultRepository.GetLastRaceResult();
        var timeNextRace = races.First().EventDate - DateTime.Now;
        var HomeViewModel = new HomeViewModel(
            races,
            ToReadableString(timeNextRace),
            lastRaceResult
        );

        return View("Index", HomeViewModel);
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
    public static string ToReadableString(TimeSpan span)
    {
        string formatted = string.Format("{0}{1}{2}{3}",
            span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
            span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
            span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
            span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

        if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

        if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

        return formatted;
    }
}
