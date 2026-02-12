using ArtManagement.BL;
using ArtManagement.DAL;
using ArtManagement.DAL.EF;
using ArtManagement.UI.MVC;
using AspNetCoreLiveMonitoring.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ArtManagementDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ArtManagementDbContext>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddLiveMonitoring();

builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.Events.OnRedirectToLogin += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments("/api"))
        {
            ctx.Response.StatusCode = 401;
        }

        return Task.CompletedTask;
    };

    cfg.Events.OnRedirectToAccessDenied += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments("/api"))
        {
            ctx.Response.StatusCode = 403;
        }

        return Task.CompletedTask;
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ArtManagementDbContext>();
    if (context.CreateDatabase(dropDatabase: true))
    {
        var userMgr = scope.ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();
        var roleMgr = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();
        SeedIdentity(userMgr, roleMgr);
        DataSeeder.Seed(context);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAndMapLiveMonitoring();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

void SeedIdentity(UserManager<IdentityUser> userMgr, RoleManager<IdentityRole> roleMgr)
{
    var xander = new IdentityUser
    {
        Id = "15487548772487",
        Email = "xander@kdg.be",
        UserName = "xander@kdg.be",
        EmailConfirmed = true
    };
    userMgr.CreateAsync(xander, "Password1!").Wait();
    var jasper = new IdentityUser
    {
        Id = "15487548772488",
        Email = "jasper@kdg.be",
        UserName = "jasper@kdg.be",
        EmailConfirmed = true
    };
    userMgr.CreateAsync(jasper, "Password1!").Wait();
    var sam = new IdentityUser
    {
        Id = "15487548772489",
        Email = "sam@kdg.be",
        UserName = "sam@kdg.be",
        EmailConfirmed = true
    };
    userMgr.CreateAsync(sam, "Password1!").Wait();  
    var quinten = new IdentityUser
    {
        Id = "15487548772490",
        Email = "quinten@kdg.be",
        UserName = "quinten@kdg.be",
        EmailConfirmed = true
    };
    userMgr.CreateAsync(quinten, "Password1!").Wait();
    var admin = new IdentityUser
    {
        Id = "15487548772491",
        Email = "admin@kdg.be",
        UserName = "admin@kdg.be",
        EmailConfirmed = true
    };
    userMgr.CreateAsync(admin, "Password1!").Wait();

    var userRole = new IdentityRole
    {
        Name = CustomIdentityConstants.UserRoleName
    };
    roleMgr.CreateAsync(userRole).Wait();
    var adminRole = new IdentityRole
    {
        Name = CustomIdentityConstants.AdminRoleName
    };
    roleMgr.CreateAsync(adminRole).Wait();
    
    userMgr.AddToRoleAsync(xander, CustomIdentityConstants.UserRoleName).Wait();
    userMgr.AddToRoleAsync(jasper, CustomIdentityConstants.UserRoleName).Wait();
    userMgr.AddToRoleAsync(sam, CustomIdentityConstants.UserRoleName).Wait();
    userMgr.AddToRoleAsync(quinten, CustomIdentityConstants.UserRoleName).Wait();
    userMgr.AddToRoleAsync(admin, CustomIdentityConstants.AdminRoleName).Wait();
}

public partial class Program { }