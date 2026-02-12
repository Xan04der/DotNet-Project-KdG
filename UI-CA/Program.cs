using ArtManagement.BL;
using ArtManagement.DAL.EF;
using ArtManagement.UI.CA;
using Microsoft.EntityFrameworkCore;

var dbContextOptionsBuilder = new DbContextOptionsBuilder();
dbContextOptionsBuilder.UseSqlite("Data Source=artmanagement.db");
var context = new ArtManagementDbContext(dbContextOptionsBuilder.Options);
if (context.CreateDatabase(dropDatabase: true))
{
    DataSeeder.Seed(context);
}
// composition root
var repository = new Repository(context);
var manager = new Manager(repository);
ConsoleUi consoleUi = new ConsoleUi(manager);

consoleUi.Run();