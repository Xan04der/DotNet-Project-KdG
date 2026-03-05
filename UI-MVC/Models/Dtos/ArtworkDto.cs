namespace ArtManagement.UI.MVC.Models.Dtos;

public class ArtworkDto
{
    public int ArtworkId { get; set; }
    public String Name { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public int? YearPainted { get; set; }
    public ICollection<ArtistArtworkDto> Artists { get; set; }
}