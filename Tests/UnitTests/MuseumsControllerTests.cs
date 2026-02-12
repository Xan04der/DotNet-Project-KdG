using ArtManagement.BL;
using ArtManagement.BL.Domain;
using ArtManagement.UI.MVC.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.UnitTests;

public class MuseumsControllerTests
{
    private readonly ArtistController _controller;
    private readonly Mock<IManager> _manager;

    public MuseumsControllerTests()
    {
        _manager = new Mock<IManager>();
        _controller = new ArtistController(_manager.Object, GetMockUserManager<IdentityUser>().Object);
    }
    
    [Fact]
    public void Add_Museum_Should_Be_Unauthorized_If_User_Is_Not_Authenticated()
    {
        //Arrange
        var unauthorizedException = new UnauthorizedAccessException("User is not authenticated.");
        
        _manager.Setup(mgr => mgr.AddMuseum("Naam", "Location", 1844))
            .Throws(unauthorizedException);
        
        //Act & Assert
        var exception = Assert.Throws<UnauthorizedAccessException>(() =>
            _manager.Object.AddMuseum("Naam", "Location", 1844));
        
        Assert.Equal(unauthorizedException.Message, exception.Message);
    }

    [Fact]
    public void Add_Museum_Should_Add_Museum_If_User_Is_Admin()
    {
        // Arrange
        var expectedMuseum = new Museum
        {
            MuseumId = 188,
            Name = "naam",
            Location = "Location",
            YearEstablished = 1844
        };
    
        _manager.Setup(mgr => mgr.AddMuseum("naam", "Location", 1844))
            .Returns(expectedMuseum);

        // Act
        var result = _manager.Object.AddMuseum("naam", "Location", 1844);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMuseum.MuseumId, result.MuseumId);
        Assert.Equal(expectedMuseum.Name, result.Name);
        Assert.Equal(expectedMuseum.Location, result.Location);
        Assert.Equal(expectedMuseum.YearEstablished, result.YearEstablished);
    }
    
    private Mock<UserManager<TUser>> GetMockUserManager<TUser>()
        where TUser : class
    {
        var userManagerMock = new Mock<UserManager<TUser>>(
            new Mock<IUserStore<TUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<TUser>>().Object,
            new IUserValidator<TUser>[0],
            new IPasswordValidator<TUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<TUser>>>().Object);
 
        return userManagerMock;
    }
}