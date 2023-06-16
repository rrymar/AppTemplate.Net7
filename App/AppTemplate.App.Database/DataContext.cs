using AppTemplate.Users.Database;
using Core.App.CurrentUser;
using Core.Database;
using Microsoft.EntityFrameworkCore;

namespace AppTemplate.App.Database
{
    public class DataContext : CoreDataContext
    {
        public DbSet<UserPreference> UserPreferences { get; set; }

        public DataContext(DbContextOptions<DataContext> options, ICurrentUserLocator currentUserLocator)
         : base(options, currentUserLocator)
        {
        }

        public DataContext()
        {

        }
    }
}
