using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ArtManagement.BL.Domain;

public class Artist : IValidatableObject
{
    public int ArtistId { get; set; }
    [Required]
    [MinLength(2)]
    public String FirstName { get; set; }
    [Required]
    [MinLength(2)]
    public String LastName { get; set; }
    [Range(0,110)]
    public int Age { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public ICollection<ArtistArtwork> Artworks { get; set; }
    public IdentityUser User { get; set; }
    
    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        var validationResults = new List<ValidationResult>();
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        if (BirthDate > currentDate)
        {
            var errormessage = "Birth date cannot be greater than current date";
            validationResults.Add(new ValidationResult(errormessage, new[] {nameof(BirthDate)}));
            
        }
        return validationResults;
    }
}