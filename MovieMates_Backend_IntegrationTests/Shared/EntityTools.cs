using System.Data.Entity;

namespace MovieMates_Backend_IntegrationTests.Shared
{
    public static class EntityTools
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
