using ArtManagement.BL;
using ArtManagement.BL.Domain;
using ArtManagement.UI.MVC.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtManagement.UI.MVC.Controllers.API;

[ApiController]
[Route("/api/[controller]")]
public class ArtistsController: ControllerBase
{
    private readonly IManager _manager;

    public ArtistsController(IManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ArtistDto>> GetAllArtists()
    {
        var artists = _manager.GetAllArtists().ToList();

        if (!artists.Any())
        {
            return NoContent();
        }
        
        return Ok(artists.Select(art => new ArtistDto
        {
            ArtistId = art.ArtistId,
            FirstName = art.FirstName,
            LastName = art.LastName,
            Age = art.Age,
            BirthDate = art.BirthDate,
            DeathDate = art.DeathDate
        }));
    }

    [HttpPut("{artistId}")]
    [Authorize]
    public IActionResult UpdateArtist(int artistId, [FromBody] UpdateArtistAgeDto updateDto)
    {
        var artist = _manager.GetArtistWithArtworksAndUser(artistId);
        if (artist == null)
        {
            return NotFound(new {message = "Artist not found"});
        }

        if (!User.IsInRole("Admin") && User.Identity?.Name != artist.User.UserName)
        {
            return Forbid();
        }

        if (updateDto == null || updateDto.Age < 0 || updateDto.Age >120)
        {
            return BadRequest(new {message = "Age must be between 0 and 120"});
        }
        
        artist.Age = updateDto.Age;
        _manager.UpdateArtist(artist);
        
        return Ok(new { message = "Age updated successfully", newAge = artist.Age });
    }
}