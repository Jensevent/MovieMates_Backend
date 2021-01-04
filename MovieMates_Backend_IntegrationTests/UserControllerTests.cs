using FluentAssertions;
using MovieMates_Backend.DAL;
using MovieMates_Backend.Entities;
using MovieMates_Backend_IntegrationTests.Shared;
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
    public class UserControllerTests
    {

        private readonly HttpClient client;
        private readonly DatabaseContext db;

        public UserControllerTests()
        {
            DatabaseSetup setup = new DatabaseSetup();

            client = setup.client;
            db = setup.db;
        }

        [Fact]
        public async Task GetAll_NotFound()
        {
            //Arrange
            EntityTools.Clear(db.Users);
            db.SaveChanges();
            var request = "api/user/all";

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAll()
        {
            //Arrange
            db.Users.Add(new User { Username = "Harry De Hakker", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" });
            db.SaveChanges();
            var request = "api/user/all";

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetUser_BadRequest()
        {
            //Arrange
            var request = "api/user/99999";

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetUser()
        {
            //Arrange
            User user = new User { Username = "MarkWallenBorg", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);
            db.SaveChanges();

            var request = "api/user/" + user.ID;

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetUserByusername_NotFound()
        {
            //Arrange
            var request = "api/user/find/ThisUsernameDoesntExist";

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetUserByusername()
        {
            //Arrange
            User user = new User { Username = "KlaasJan", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);
            db.SaveChanges();

            var request = "api/user/find/" + user.Username;

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetUserMovies_NotFound()
        {
            //Arrange
            var request = "api/user/movies/9999";

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetUserMovies_BadRequest()
        {
            //Arrange
            User user = new User { Username = "HansKlokkerman", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);
            db.SaveChanges();

            var request = "api/user/movies/" + user.ID;

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetUserMovies()
        {
            //Arrange
            Movie movie = new Movie { Title = "Waterschapsheuvel", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            db.Movies.Add(movie);

            User user = new User { Username = "KarelDeKleine", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);

            db.SaveChanges();

            UserMovie userMovie = new UserMovie { MovieID = movie.ID, Movie = movie, UserID = user.ID, User = user };
            db.UserMovies.Add(userMovie);
            db.SaveChanges();


            var request = "api/user/movies/" + user.ID;

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetUserGroups_NotFound()
        {
            //Arrange
            var request = "api/user/99999/groups";

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetUserGroups_BadRequest()
        {
            //Arrange
            User user = new User { Username = "HenkHogeBoom", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);
            db.SaveChanges();

            var request = "api/user/" + user.ID + "/groups";

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task GetUserGroups()
        {
            //Arrange
            Group group = new Group { Name = "de KaasKnakkers" };
            db.Groups.Add(group);

            User user = new User { Username = "KarelDeKleine", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);

            db.SaveChanges();

            user.Groups.Add(group);
            db.SaveChanges();


            var request = "api/user/" + user.ID + "/groups";

            //Act
            var response = await client.GetAsync(request);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }



        [Fact]
        public async Task UserLikesMovie_BadRequest()
        {
            //Arrange
            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            var request = "api/user/likes/99999/99999";

            //Act
            var response = await client.PatchAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UserLikesMovie_NotFound()
        {
            //Arrange
            Movie movie = new Movie { Title = "Oceans Eight", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            db.Movies.Add(movie);

            User user = new User { Username = "S0LSTICE", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);

            db.SaveChanges();

            UserMovie userMovie = new UserMovie { MovieID = movie.ID, Movie = movie, UserID = user.ID, User = user };
            db.UserMovies.Add(userMovie);
            db.SaveChanges();

            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            var request = "api/user/likes/" + user.ID + "/" + movie.ID;

            //Act
            var response = await client.PatchAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UserLikesMovie()
        {
            //Arrange
            Movie movie = new Movie { Title = "Oceans Eleven", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            db.Movies.Add(movie);

            User user = new User { Username = "UFOCreator", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);

            db.SaveChanges();

            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            var request = "api/user/likes/" + user.ID + "/" + movie.ID;

            //Act
            var response = await client.PatchAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UserWatchedMovie_BadRequest()
        {
            //Arrange
            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            var request = "api/user/watched/99999/99999/false";

            //Act
            var response = await client.PatchAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }



        [Fact]
        public async Task UserWatchedMovie_NotFound()
        {
            //Arrange
            Movie movie = new Movie { Title = "Ready player one", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            db.Movies.Add(movie);

            User user = new User { Username = "ThePrestigeDoughnuts", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);

            db.SaveChanges();
            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            var request = "api/user/watched/" + user.ID + "/" + movie.ID + "/true";

            //Act
            var response = await client.PatchAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task UserWatchedMovie()
        {
            //Arrange
            Movie movie = new Movie { Title = "Sonic the Hedgehog", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            db.Movies.Add(movie);

            User user = new User { Username = "Venison", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);

            db.SaveChanges();

            UserMovie userMovie = new UserMovie { MovieID = movie.ID, Movie = movie, UserID = user.ID, User = user };
            db.UserMovies.Add(userMovie);
            db.SaveChanges();

            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            var request = "api/user/watched/" + user.ID + "/" + movie.ID + "/true";

            //Act
            var response = await client.PatchAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }



        [Fact]
        public async Task UserRatedMovie_BadRequest()
        {
            //Arrange
            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            var request = "api/user/watched/99999/99999/9.9";

            //Act
            var response = await client.PatchAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }



        [Fact]
        public async Task UserRatedMovie_NotFound()
        {
            //Arrange
            Movie movie = new Movie { Title = "Ready player one", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            db.Movies.Add(movie);

            User user = new User { Username = "ThePrestigeDoughnuts", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);

            db.SaveChanges();
            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            var request = "api/user/rated/" + user.ID + "/" + movie.ID + "/9.8";

            //Act
            var response = await client.PatchAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task UserRatedMovie()
        {
            //Arrange
            Movie movie = new Movie { Title = "Sonic the Hedgehog", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            db.Movies.Add(movie);

            User user = new User { Username = "Venison", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);

            db.SaveChanges();

            UserMovie userMovie = new UserMovie { MovieID = movie.ID, Movie = movie, UserID = user.ID, User = user };
            db.UserMovies.Add(userMovie);
            db.SaveChanges();

            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            var request = "api/user/rated/" + user.ID + "/" + movie.ID + "/9.8";

            //Act
            var response = await client.PatchAsync(request, stringContent);


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }



    }
}
