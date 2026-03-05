namespace ArtManagement.UI.MVC.Models.Dtos;

public class MuseumDto
{
    public int MuseumId { get; set; }
    public String Name { get; set; }
    public String Location { get; set; }
    public int YearEstablished { get; set; }
    public ICollection<ArtworkDto> Artworks { get; set; }
}