using System.ComponentModel.DataAnnotations;
using ArtManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace Tests;

public class ArtistTests
{
    [Fact]
    public void Artist_Is_Valid_With_BirthDate_Not_Future_Date()
    {
        //arrange
        var artist = new Artist
        {
            FirstName = "René",
            LastName = "Magritte",
            Age = 68,
            BirthDate = new DateOnly(1898, 11, 21),
            DeathDate = new DateOnly(1967, 08, 15),
            User = new IdentityUser()
        };
        
        //act
        var isValid = Validator.TryValidateObject(artist, new ValidationContext(artist), null);
        
        //assert
        Assert.True(isValid);
    }
    
    [Fact]
    public void Artist_Is_Invalid_With_BirthDate_As_Future_Date()
    {
        //arrange
        var artist = new Artist
        {
            FirstName = "Jeff",
            LastName = "Klaasen",
            Age = 20,
            BirthDate = DateOnly.FromDateTime(DateTime.Today).AddDays(10),
            User = new IdentityUser()
        };
        
        //act
        var isValid = Validator.TryValidateObject(artist, new ValidationContext(artist), null);
        
        //assert
        Assert.False(isValid);
    }
}