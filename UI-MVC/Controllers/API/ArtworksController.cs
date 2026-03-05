using ArtManagement.BL;
using ArtManagement.BL.Domain;
using ArtManagement.UI.MVC.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtManagement.UI.MVC.Controllers.API;
[ApiController]
[Route("/api/[controller]")]
public class ArtworksController : ControllerBase
{

    private readonly IManager _manager;

    public ArtworksController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet("{artworkId}/artists")]
    public ActionResult<IEnumerable<ArtistArtworkDto>> GetArtistsOfArtwork(int artworkId)
    {
        var artistArtworks = _manager.GetArtistsOfArtwork(artworkId);

        if (!artistArtworks.Any())
        {
            return NoContent();
        }
        
        return Ok(artistArtworks.Select(aa => new ArtistArtworkDto
        {
            Id = aa.Artist.ArtistId,
            Name = aa.Artist.FirstName + " " + aa.Artist.LastName,
            TimeFrame = aa.TimeFrame,
            Tutor = aa.Tutor
        }));
    }

    [HttpGet("{artworkId}/artists/{artistId}")]
    public ActionResult<ArtistArtworkDto> GetArtistArtwork(int artworkId, int artistId)
    {
        ArtistArtwork artistArtwork = null;

        if (artistArtwork == null)
        {
            return NotFound();
        }
        
        return Ok(new ArtistArtworkDto
        {
            Id = artistArtwork.Artist.ArtistId,
            Name = artistArtwork.Artist.FirstName + " " + artistArtwork.Artist.LastName,
            TimeFrame = artistArtwork.TimeFrame,
            Tutor = artistArtwork.Tutor
        });
    }

    [HttpPost("{artworkId}/artists")]
    [Authorize]
    public ActionResult<ArtistArtworkDto> AddArtistToArtwork(int artworkId, NewArtistArtworkDto newArtworkDto)
    {
        var createdArtistArtwork = _manager.AddArtistArtwork(
            newArtworkDto.ArtistId,
            artworkId,
            newArtworkDto.Tutor,
            newArtworkDto.TimeFrame
            );
        
        return CreatedAtAction(nameof(GetArtistArtwork),
            new { artworkId, artistId = newArtworkDto.ArtistId },
            new ArtistArtworkDto
            {
                Id = createdArtistArtwork.Artist.ArtistId,
                Name = createdArtistArtwork.Artist.FirstName + " " + createdArtistArtwork.Artist.LastName,
                TimeFrame = createdArtistArtwork.TimeFrame,
                Tutor = createdArtistArtwork.Tutor
            });
    }
}