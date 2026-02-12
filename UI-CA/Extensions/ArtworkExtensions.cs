using System.Text;
using ArtManagement.BL.Domain;

namespace ArtManagement.UI.CA.Extensions;

public static class ArtworkExtensions
{
    public static string GetStringInfo(this Artwork artwork)
    {
        var result = new StringBuilder();
        result.Append($"{artwork.Name}, Painted in:{artwork.YearPainted}, Movement:{artwork.ArtMovement}, Length:{artwork.Length}, Width:{artwork.Width}");

        if (artwork.Museum != null)
        {
            result.Append($", Museum: {artwork.Museum.Name}"); 
        }
        return result.ToString();
    }
}