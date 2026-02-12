using ArtManagement.BL;
using ArtManagement.BL.Domain;
using ArtManagement.UI.MVC.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.UnitTests;

public class ArtistControllerTests
{
     private readonly ArtistController _controller;
     private readonly Mock<IManager> _manager;

     public ArtistControllerTests()
     {
          _manager = new Mock<IManager>();
          _controller = new ArtistController(_manager.Object, GetMockUserManager<IdentityUser>().Object);
     }

     [Fact]
     public void Add_Should_Redirect_To_Details_If_ModelState_Valid()
     {
          //Arrange
          var today = DateOnly.FromDateTime(DateTime.Now);
          _manager.Setup(mgr => mgr.AddArtist("naam", "achternaam", 20, today, null, null))
               .Returns(new Artist
               {
                    ArtistId = 888
               });
          
          //Act
          var result = _controller.Add(new Artist
          {
               FirstName = "naam",
               LastName = "achternaam",
               Age = 20,
               BirthDate = today
          });

          //Assert
          Assert.IsType<RedirectToActionResult>(result);
          var redirectResult = (RedirectToActionResult)result;
          Assert.Equal("Details", redirectResult.ActionName);
          Assert.Null(redirectResult.ControllerName);
          Assert.NotNull(redirectResult.RouteValues);
          Assert.Single(redirectResult.RouteValues);
          Assert.Equal(888, redirectResult.RouteValues["id"]);
          _manager.Verify(mgr => mgr.AddArtist("naam", "achternaam", 20, today, null, null),
               Times.Once());
     }
     
     [Fact]
     public void Add_Should_Stay_On_Add_Page_If_ModelState_Invalid()
     {
          //Arrange
          _controller.ModelState.AddModelError("Name", "Required");
          
          //Act
          var result = _controller.Add(new Artist
          {
               FirstName = "naam",
               LastName = "achternaam"
          });

          //Assert
          Assert.IsType<ViewResult>(result);
          var viewResult = (ViewResult)result;
          Assert.Null(viewResult.ViewName);
          Assert.Equal(0, viewResult.ViewData.Count);
          _manager.Verify(mgr => mgr.AddArtist(It.IsAny<string>(), It.IsAny<string>()
                    , It.IsAny<int>(), It.IsAny<DateOnly>(), 
                    It.IsAny<DateOnly>(), It.IsAny<string>()),
                Times.Never());
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