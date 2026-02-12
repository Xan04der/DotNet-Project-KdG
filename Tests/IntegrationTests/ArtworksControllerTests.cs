using System.Net;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class ArtworksControllerTests : IClassFixture<CustomWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly CustomWebApplicationFactoryWithMockAuth<Program> _factory;

    public ArtworksControllerTests(CustomWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Add_ArtistArtwork_Should_Be_Unauthorized_If_Not_Authenticated()
    {
        //Arrange
        var client = _factory.CreateClient();

        //Act
        var response = client.PostAsync($"/api/Artworks/{1}/artists", null).Result;

        //Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

    }

    [Fact]
    public void Add_ArtistArtwork_Should_Be_Created_If_Authorized()
    {
        //Arrange
        var client = _factory
            .AuthenticatedInstance(new Claim(ClaimTypes.Name, "xander@kdg.be"))
            .CreateClient();

        var newArtistArtworkDto = new
        {
            ArtistId = 2,
            ArtistName = "Oscar-Claude Monet",
            TimeFrame = "2024-2025",
            Tutor = false

        };
        
        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(newArtistArtworkDto),
            Encoding.UTF8,
            "application/json"
            );
        
        //Act
        var response = client.PostAsync($"/api/Artworks/{1}/artists", jsonContent).Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public void Add_ArtistArtwork_Should_Give_InternalServerError_If_Artwork_Does_Not_Exist()
    {
        //Arrange
        var client = _factory
            .AuthenticatedInstance(new Claim(ClaimTypes.Name, "xander@kdg.be"))
            .CreateClient();
        
        var newArtistArtworkDto = new
        {
            ArtistId = 2,
            ArtistName = "Oscar-Claude Monet",
            TimeFrame = "2024-2025",
            Tutor = false

        };
        
        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(newArtistArtworkDto),
            Encoding.UTF8,
            "application/json"
        );
        
        //Act
        var response = client.PostAsync($"/api/Artworks/{10}/artists", jsonContent).Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}