using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcSample.Models;

namespace MvcSample.Controllers {
  public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) {
      _logger = logger;
    }

    [HttpGet]
    public IActionResult Index() {
      if (User.Identity != null && User.Identity.IsAuthenticated)
      {
          return RedirectToAction("Main");
      }
      return View();
    }

    public IActionResult Privacy() {
      return View();
    }

    public IActionResult Terms() {
      return View();
    }

    public IActionResult Contact() {
      return View();
    }

    public IActionResult About() {
      return View();
    }

    [Authorize]
    public IActionResult Main()
    {
        return View();
    }

    [Authorize]
    public IActionResult Profile()
    {
        return View();
    }

    [Authorize]
    public IActionResult Games()
    {
        return View();
    }

    [Authorize]
    public IActionResult Teams()
    {
        return View();
    }

    [Authorize]
    public IActionResult Scores()
    {
        return View();
    }

    [Authorize]
    public IActionResult Stats()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
      return View(new ErrorViewModel {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
      });
    }
  }
}
