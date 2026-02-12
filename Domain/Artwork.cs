using System.ComponentModel.DataAnnotations;

namespace ArtManagement.BL.Domain;

public class Artwork
{
    public int ArtworkId { get; set; }
    [Required]
    [MinLength(1)]
    public String Name { get; set; }
    [Range(0,1000)]
    public double Width { get; set; }
    [Range(0,1000)]
    public double Length { get; set; }
    [Range(-5000,5000)]
    public int? YearPainted { get; set; }
    public ArtMovementEnum ArtMovement { get; set; }
    public ICollection<ArtistArtwork> Artists { get; set; }
    public Museum Museum { get; set; }
}