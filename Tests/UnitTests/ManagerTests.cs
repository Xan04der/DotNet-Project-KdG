using System.ComponentModel.DataAnnotations;
using ArtManagement.BL;
using ArtManagement.BL.Domain;
using ArtManagement.DAL;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests.UnitTests;

public class ManagerTests
{
    private readonly IManager _manager;
    private readonly Mock<IRepository> _repository;

    public ManagerTests()
    {
        _repository = new Mock<IRepository>();
        _manager = new Manager(_repository.Object);
    }

    [Fact]
    public void Add_Artist_Should_Reject_Invalid_Fields()
    {
        //Arrange
        _repository.Setup(repo => repo.ReadUser("15487548772487"))
            .Returns(new IdentityUser());
        
        //Act
        var action = () => _manager.AddArtist("firstName", "lastName", -10, 
            DateOnly.FromDateTime(DateTime.Today),null,"15487548772487");
        
        //Assert
        Assert.Throws<ValidationException>(action);
        _repository.Verify(repo => repo.CreateArtist(It.IsAny<Artist>()), Times.Never);
    }

    [Fact]
    public void Add_Artist_Should_Accept_Valid_Fields()
    {
        //Arrange
        _repository.Setup(repo => repo.ReadUser("15487548772487"))
            .Returns(new IdentityUser());
        
        //Act
        var createdArtist = _manager.AddArtist("firstName", "lastName", 10, 
            DateOnly.FromDateTime(DateTime.Today),null,"15487548772487");
        
        //Assert
        _repository.Verify(repo => repo.CreateArtist(It.Is<Artist>(
            artist => artist.ArtistId == createdArtist.ArtistId
            && artist.FirstName == createdArtist.FirstName
            && artist.LastName == createdArtist.LastName
            && artist.Age == createdArtist.Age
            && artist.BirthDate == createdArtist.BirthDate
            && artist.DeathDate == createdArtist.DeathDate
            && artist.User == createdArtist.User
            )), Times.Once);
    }

    [Fact]
    public void Delete_ArtistArtwork_Should_Reject_Invalid_ArtistArtwork()
    {
        //Arrange
        _repository.Setup(repo => repo.ReadArtistArtwork(1, 3))
            .Returns((ArtistArtwork)null);
        
        //Act
        var action = () => _manager.DeleteArtistArtwork(1, 3);
        
        //Assert
        Assert.Throws<InvalidOperationException>(action);
        _repository.Verify(repo => repo.DeleteArtistArtwork(It.IsAny<ArtistArtwork>()), Times.Never);

    }

    [Fact]
    public void Delete_ArtistArtwork_Should_Accept_Valid_ArtistArtwork()
    {
        //Arrange
        _repository.Setup(repo => repo.ReadArtistArtwork(1,1))
            .Returns(new ArtistArtwork());
        
        //Act
        var deletedArtistArtwork = _manager.DeleteArtistArtwork(1,1);
        
        //Assert
        _repository.Verify(repo => repo.DeleteArtistArtwork(It.Is<ArtistArtwork>(
            artistArtwork => artistArtwork.Artist  == deletedArtistArtwork.Artist
            && artistArtwork.Artwork == deletedArtistArtwork.Artwork
            )), Times.Once);
    }
}