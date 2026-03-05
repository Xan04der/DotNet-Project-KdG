using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtManagement.UI.MVC.Controllers;

public class MuseumController : Controller
{
    [HttpGet]
    [Authorize(Roles = CustomIdentityConstants.AdminRoleName)]
    public IActionResult Index()
    {
        return View();
    }
}