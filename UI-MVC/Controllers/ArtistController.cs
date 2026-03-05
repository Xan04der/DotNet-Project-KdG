using ArtManagement.BL;
using ArtManagement.BL.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArtManagement.UI.MVC.Controllers;

public class ArtistController : Controller
{
    private readonly IManager _manager;
    private readonly UserManager<IdentityUser> _userManager;

    public ArtistController(IManager manager, UserManager<IdentityUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_manager.GetAllArtists());
    }

    [HttpGet]
    [Authorize]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Add(Artist artist)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var createdArtist = _manager.AddArtist(
            artist.FirstName,
            artist.LastName,
            artist.Age,
            artist.BirthDate,
            artist.DeathDate,
            _userManager.GetUserId(User)
        );
        
        return RedirectToAction("Details", new {id = createdArtist.ArtistId});
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        return View(_manager.GetArtistWithArtworksAndUser(id));
    }
}