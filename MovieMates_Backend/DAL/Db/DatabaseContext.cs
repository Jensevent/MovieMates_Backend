using Microsoft.Extensions.Configuration;
using MovieMates_Backend.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MovieMates_Backend.DAL
{
    public class DatabaseContext : DbContext
    {


        private static IConfiguration Config { get; set; } = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

        public DatabaseContext(IConfiguration config, string connName) : base(Config.GetConnectionString(connName).ToString())
        {
            Database.SetInitializer<DatabaseContext>(new DatabaseInitializer());
        }

        public DatabaseContext(string connStr) : base(connStr)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        public DbSet<Group> Groups { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
