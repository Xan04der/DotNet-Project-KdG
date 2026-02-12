using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class ArtistController : IClassFixture<CustomWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly CustomWebApplicationFactoryWithMockAuth<Program> _factory;

    public ArtistController(CustomWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Add_Should_Require_Authorization()
    {
        //Arrange
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions{ AllowAutoRedirect = false });
        
        //Act
        var response = client.PostAsync("/Artist/Add", null).Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        Assert.Equal("/Identity/Account/Login", response.Headers.Location?.AbsolutePath);
    }

    /*[Fact]
    public void Add_Should_Accept_Valid_Artist()
    {
        //Arrange
        var client = _factory
            .AuthenticatedInstance(
                new Claim(ClaimTypes.NameIdentifier, "15487548772487"),
                new Claim(ClaimTypes.Name, "xander@kdg.be"))
            .CreateClient(new WebApplicationFactoryClientOptions{ AllowAutoRedirect = false });
        
        //Act
        var response = client.PostAsync("/Artist/Add",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"FirstName", "Jan"},
                {"LastName", "Meeuwen"},
                {"BirthDate", DateTime.Now.ToString("dd/MM/yyyy")}
            })).Result;
        
        //Assert
        Assert.Equal(HttpStatusCode.Ok, response.StatusCode);
        Assert.Matches(@"/Artist/Details/\d+", response.Headers.Location?.ToString());
    }
    
    [Fact]
    public void Add_Should_Not_Accept_Invalid_Artist()
    {
        //Arrange
        var client = _factory
            .AuthenticatedInstance(
                new Claim(ClaimTypes.NameIdentifier, "15487548772487"),
                new Claim(ClaimTypes.Name, "xander@kdg.be"))
            .CreateClient();
        
        //Act
        var response = client.PostAsync("/Artist/Add",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "FirstName", "Jan" },
                { "LastName", "Meeuwen" },
                { "BirthDate", DateTime.Now.AddDays(10).ToString("dd/MM/yyyy") }
            })).Result;
        var content = response.Content.ReadAsStringAsync().Result;

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(">Birth date cannot be greater than current date<", content);

    }*/
}