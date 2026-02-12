using System.ComponentModel.DataAnnotations;

namespace ArtManagement.BL.Domain;

public class Museum : IValidatableObject
{
    public int MuseumId { get; set; }
    [MinLength(1)]
    public String Name { get; set; }
    [MinLength(1)]
    public String Location { get; set; }
    public int YearEstablished { get; set; }
    public ICollection<Artwork> Artworks { get; set; }
    
    public override string ToString()
    {
        return $"{Name} Location:{Location}, Established in:{YearEstablished}, Amount of Artworks:{Artworks.Count}";
    }

    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        var validationResults = new List<ValidationResult>();
        var currentYear = DateTime.Now.Year;
        if (YearEstablished > currentYear)
        {
           var errorMessage="Year established cannot be greater than current year"; 
           validationResults.Add(new ValidationResult(errorMessage, new []{nameof(YearEstablished)}));
        }

        return validationResults;
    }
}