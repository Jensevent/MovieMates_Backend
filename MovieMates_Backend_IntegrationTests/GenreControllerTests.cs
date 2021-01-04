using FluentAssertions;
using MovieMates_Backend.DAL;
using MovieMates_Backend.Entities;
using MovieMates_Backend_IntegrationTests.Shared;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MovieMates_Backend_IntegrationTests
{
    [Collection("Tests")]
    public class GenreControllerTests
    {
        private readonly HttpClient client;
        private readonly DatabaseContext db;

        public GenreControllerTests()
        {
            DatabaseSetup setup = new DatabaseSetup();

            client = setup.client;
            db = setup.db;
        }


        [Fact]
        public async Task GetAll_NotFound()
        {
            //Arrange
            EntityTools.Clear(db.Genres);
            db.SaveChanges();
            var request = "api/genre/all";

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAll()
        {
            //Arrange
            db.Genres.Add(new Genre { GenreName = "Horror" });
            db.SaveChanges();
            var request = "api/genre/all";

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
