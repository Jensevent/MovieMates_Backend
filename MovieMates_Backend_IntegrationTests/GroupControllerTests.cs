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
    public class GroupControllerTests
    {
        private readonly HttpClient client;
        private readonly DatabaseContext db;

        public GroupControllerTests()
        {
            DatabaseSetup setup = new DatabaseSetup();

            client = setup.client;
            db = setup.db;
        }



        [Fact]
        public async Task GetAll()
        {
            //Arange
            db.Groups.Add(new Group { Name = "abc" });
            db.SaveChanges();

            var request = "/api/group/all";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_NotFound()
        {
            //Arange
            EntityTools.Clear(db.Groups);
            db.SaveChanges();

            var request = "/api/group/all";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }



        [Fact]
        public async Task GetGroup_NotFound()
        {
            //Arange
            var request = "/api/group/55";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetGroup_BadRequest()
        {
            //Arange
            var request = "/api/group/abc";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetGroup()
        {
            //Arange
            Group group = new Group { Name = "Coole Kids Club" };
            db.Groups.Add(group);
            db.SaveChanges();

            var request = "/api/group/" + group.ID;

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }



        [Fact]
        public async Task GetGroupMembers_BadRequest()
        {
            //Arrange
            var request = "/api/group/abcdefg/members";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetGroupMembers_NotFound()
        {
            //Arrange
            Group group = new Group { Name = "De BreiTantes" };
            db.Groups.Add(group);
            db.SaveChanges();

            var request = "/api/group/" + group.ID + "/members";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetGroupMembers()
        {
            //Arrange
            Group group = new Group { Name = "De Hangjeugd" };
            db.Groups.Add(group);

            group.Members.Add(new User { Username = "Gerda65", Email = "gerda65@gmail.com", PasswordHash = "Lorem Ipsum", PasswordSalt = "Lorem Ipsum" });

            db.SaveChanges();

            var request = "/api/group/" + group.ID + "/members";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }



        [Fact]
        public async Task GetGroupMovies_BadRequest()
        {
            //Arrange
            var request = "/api/group/abcdefg/movies";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetGroupMovies_NotFound()
        {
            //Arrange
            Group group = new Group { Name = "FrankieGang" };
            db.Groups.Add(group);
            db.SaveChanges();

            var request = "/api/group/" + group.ID + "/movies";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetGroupMovies()
        {
            //Arrange
            Group group = new Group { Name = "De PitpullLovers" };
            db.Groups.Add(group);

            Movie movie = new Movie { Title = "Atlantis", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
            db.Movies.Add(movie);

            User user = new User { Username = "Lage Linda", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };
            db.Users.Add(user);

            db.SaveChanges();

            UserMovie userMovie = new UserMovie { MovieID = movie.ID, Movie = movie, UserID = user.ID, User = user };
            db.UserMovies.Add(userMovie);
            db.SaveChanges();


            group.Members.Add(user);

            db.SaveChanges();

            var request = "/api/group/" + group.ID + "/members";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }



        [Fact]
        public async Task CreateGroup()
        {
            //Arrange
            string request = "/api/group/create/myGroup";
            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await client.PostAsync(request, stringContent);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }



        [Fact]
        public async Task AddUserToGroup_NotFound()
        {
            //Arrange
            string request = "/api/group/abcdfg/add/" + Guid.NewGuid();
            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await client.PatchAsync(request, stringContent);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task AddUserToGroup_BadRequest()
        {
            //Arrange
            Group group = new Group { Name = "Gamers Elite" };
            User user = new User { Username = "GamerRabbit", Email = "linda@gmail.com", PasswordHash = "LoremIpsum", PasswordSalt = "LoremIpsum" };

            db.Users.Add(user);

            group.Members.Add(user);
            db.Groups.Add(group);
            db.SaveChanges();

            string request = "/api/group/" + user.ID + "/add/" + group.JoinID;
            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await client.PatchAsync(request, stringContent);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddUserToGroup()
        {
            //Arrange
            Group group = new Group { Name = "SwordFighters" };
            User user = new User { Username = "XxPotatoMan95", Email = "BeeMeister141@outlook.com", PasswordHash = "SuikerWafels156", PasswordSalt = "IkeaKast#69_po" };

            db.Users.Add(user);
            db.Groups.Add(group);
            db.SaveChanges();

            string request = "/api/group/" + group.JoinID + "/add/" + user.ID;
            StringContent stringContent = new StringContent("", Encoding.UTF8, "application/json");

            //Act
            HttpResponseMessage response = await client.PatchAsync(request, stringContent);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
