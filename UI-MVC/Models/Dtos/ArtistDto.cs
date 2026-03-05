namespace ArtManagement.UI.MVC.Models.Dtos;

public class ArtistDto
{
    public int ArtistId { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public int Age { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public ICollection<ArtistArtworkDto> Artworks { get; set; }
}