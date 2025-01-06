using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SocialRecipes.Infrastructure.Settings;

namespace SocialRecipes.DAL
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        Settings settings = new Settings();
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(settings.GetConnectionString()); 

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
