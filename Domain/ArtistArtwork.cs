using System.ComponentModel.DataAnnotations;

namespace ArtManagement.BL.Domain;

public class ArtistArtwork
{
    [Required]
    public Artist Artist { get; set; }
    [Required]
    public Artwork Artwork { get; set; }

    public bool Tutor { get; set; }

    public String TimeFrame { get; set; }
}