using Tinder.DAL;

namespace Tinder.IntegrationTests.Utilities
{
    public static class InitializeDb
    {
        public static void InitializeDbForTests(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
