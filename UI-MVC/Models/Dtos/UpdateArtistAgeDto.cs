using System.ComponentModel.DataAnnotations;

namespace ArtManagement.UI.MVC.Models.Dtos;

public class UpdateArtistAgeDto
{
    [Range(0, 200)]
    public int Age { get; set; }
}