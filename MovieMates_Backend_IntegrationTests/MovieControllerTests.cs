using FluentAssertions;
using MovieMates_Backend.DAL;
using MovieMates_Backend.Entities;
using MovieMates_Backend_IntegrationTests.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieMates_Backend_IntegrationTests
{
    [Collection("Tests")]
    public class MovieControllerTests
    {
        readonly HttpClient client;
        readonly DatabaseContext db;

        readonly JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };


        public MovieControllerTests()
        {
            DatabaseSetup setup = new DatabaseSetup();

            client = setup.client;
            db = setup.db;
        }


        [Fact]
        public async Task GetAll_Empty()
        {
            //Arange
            EntityTools.Clear(db.Movies);
            db.SaveChanges();

            var request = "/api/movie/all";


            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAll()
        {
            //Arrange
            db.Movies.Add(new Movie { Title = "V for Vendetta", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } });
            db.SaveChanges();

            var request = "/api/movie/all";


            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }


        [Fact]
        public async Task GetByGenre_BadRequest()
        {
            //Arange
            var request = "/api/movie/filter/abc";


            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetbyGenre_NotFound()
        {
            //Arange
            Genre genre = new Genre { GenreName = "Sci-Fi" };
            db.Genres.Add(genre);
            db.SaveChanges();

            var request = "api/movie/filter/" + genre.ID;


            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task GetByGenre()
        {
            //Arrange
            Genre genre = new Genre { GenreName = "Action" };
            db.Genres.Add(genre);

            Movie movie = new Movie { Title = "Avengers: Endgame", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { genre } };
            db.Movies.Add(movie);
            db.SaveChanges();

            var request = "api/movie/filter/" + genre.ID;


            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task FindMovie_NotFound()
        {
            //Arange
            var request = "/api/movie/abcdefg";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task FindMovie()
        {
            //Arange
            Movie movie = new Movie { Title = "Up", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            db.Movies.Add(movie);
            db.SaveChanges();

            var request = "api/movie/" + movie.ID;


            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddMovie_BadRequest()
        {
            //Arange 
            Movie movie = new Movie { Title = "Ready Player One", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            db.Movies.Add(movie);
            db.SaveChanges();

            var request = "api/movie/add";


            //Act
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(movie, Formatting.Indented, settings), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddMovie()
        {
            //Arange 
            Movie movie = new Movie { Title = "Bambi", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            var request = "api/movie/add";


            //Act
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(movie, Formatting.Indented, settings), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}


