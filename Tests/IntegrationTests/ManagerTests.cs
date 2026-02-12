using System.ComponentModel.DataAnnotations;
using ArtManagement.BL;
using ArtManagement.BL.Domain;
using Microsoft.EntityFrameworkCore;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class ManagerTests : IClassFixture<CustomWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly CustomWebApplicationFactoryWithMockAuth<Program> _factory;

    public ManagerTests(CustomWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void AddMuseum_Should_Validate_Year_Established()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
        
        //Assert
        Assert.Throws<ValidationException>(() =>
        {
            //Act
            manager.AddMuseum("", "", DateTime.Now.Year);
        });
    }
    
    [Fact]
    public void AddMuseum_Should_Return_Created_Museum()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
        
        //Act
        var createdMuseum = manager.AddMuseum("German National Museum", "Berlin", DateTime.Now.Year);
        
        //Assert
        Assert.NotNull(createdMuseum);
        Assert.InRange(createdMuseum.MuseumId, 1, Int64.MaxValue);
        Assert.Equal("German National Museum", createdMuseum.Name);
        Assert.Equal("Berlin", createdMuseum.Location);
        Assert.Equal(DateTime.Now.Year, createdMuseum.YearEstablished);
    }

    [Fact]
    public void DeleteArtistArtwork_Should_Delete_ArtistArtwork_Given_Valid_ArtistId_And_ArtworkId()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
        
        //Act
        var deletedArtistArtwork = manager.DeleteArtistArtwork(1, 1);
        
        //Assert
        Assert.NotNull(deletedArtistArtwork);
        Assert.InRange(deletedArtistArtwork.Artist.ArtistId, 1, Int64.MaxValue);
        Assert.InRange(deletedArtistArtwork.Artwork.ArtworkId, 1, Int64.MaxValue);
        Assert.Equal(1, deletedArtistArtwork.Artist.ArtistId);
        Assert.Equal(1, deletedArtistArtwork.Artwork.ArtworkId);
    }

    [Fact]
    public void DeleteArtistArtwork_Should_Throw_Exception_Given_Invalid_ArtistId_And_ArtworkId()
    {
        //Arrange
        using var scope = _factory.Services.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IManager>();
        
        //Assert
        Assert.Throws<InvalidOperationException>(
            //Act
            () => manager.DeleteArtistArtwork(20, 20)
            );
        
    }
}