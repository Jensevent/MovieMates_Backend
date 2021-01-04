using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using MovieMates_Backend;
using MovieMates_Backend.DAL;
using System;
using System.Data.SqlClient;
using System.Net.Http;

namespace MovieMates_Backend_IntegrationTests
{
    public class DatabaseSetup : IDisposable
    {
        private readonly TestServer server;
        public readonly HttpClient client;
        public readonly DatabaseContext db;

        public DatabaseSetup()
        {
            var builder = new Microsoft.AspNetCore.Hosting.WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>();

            server = new TestServer(builder);

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestingDB;Trusted_Connection=True;";

            db = new DatabaseContext(connectionString);

            client = server.CreateClient();
        }

        public void Dispose()
        {
            server.Dispose();
            client.Dispose();
            SqlConnection.ClearAllPools();
            db.Database.Delete();
        }
    }
}
