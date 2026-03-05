using ArtManagement.BL;
using ArtManagement.UI.MVC.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtManagement.UI.MVC.Controllers.API;

[ApiController]
[Route("/api/museums")]
public class MuseumsController : ControllerBase
{
    private readonly IManager _manager;

    public MuseumsController(IManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MuseumDto>> GetAllMuseums()
    {
        var museums = _manager.GetAllMuseumsWithArtworks();
        if (museums.Any())
        {
            return Ok(museums.Select(museum => new MuseumDto
            {
                MuseumId = museum.MuseumId,
                Name = museum.Name,
                Location = museum.Location,
                YearEstablished = museum.YearEstablished
            }));
        }

        return NoContent();
    }

    [HttpGet("{museumId}")]
    public ActionResult<MuseumDto> GetOneMuseum(int museumId)
    {
        var museum = _manager.GetMuseum(museumId);
        if (museum != null)
        {
            return Ok(museum);
        }

        return NotFound();
    }

    [HttpPost]
    [Authorize]
    public IActionResult AddMuseum(MuseumDto museum)
    {
        var createdMuseum = _manager.AddMuseum(museum.Name, museum.Location, museum.YearEstablished);
        
        return CreatedAtAction(nameof(GetOneMuseum),
            new {museumId = createdMuseum.MuseumId},
            new MuseumDto
            {
                MuseumId = createdMuseum.MuseumId,
                Name = createdMuseum.Name,
                Location = createdMuseum.Location,
                YearEstablished = createdMuseum.YearEstablished
            });
    }
}