using System.Text;
using ArtManagement.BL.Domain;

namespace ArtManagement.UI.CA.Extensions;

public static class ArtistExtensions
{
    public static string GetStringInfo(this Artist artist)
    {
        var result = new StringBuilder();
        result.Append($"{artist.FirstName} {artist.LastName}, Age:{artist.Age}, Lived from:{artist.BirthDate} to {artist.DeathDate}");
        
        if (artist.Artworks != null)
        {
            foreach (var artwork in artist.Artworks)
            {
                var artworkInfo = artwork.Artwork.Name;
                result.Append($" \nArtworks: \n{artworkInfo}");
            }
        }

        return result.ToString();
    }
}