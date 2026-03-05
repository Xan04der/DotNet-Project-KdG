using ArtManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace ArtManagement.UI.MVC.Controllers;

public class ArtworkController : Controller
{
    private readonly IManager _manager;

    public ArtworkController(IManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        return View(_manager.GetArtwork(id));
    }
}