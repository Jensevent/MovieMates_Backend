using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MovieMates_Backend.Containers;
using MovieMates_Backend.DAL;
using MovieMates_Backend.DAL.Db;
using MovieMates_Backend.Entities;
using MovieMates_Backend.Hubs;
using MovieMates_Backend.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieMates_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        readonly IConfiguration config;
        readonly MovieContainer mc;
        readonly IDValidator idValidator = new IDValidator();

        private readonly IHubContext<NotificationHub> _notificationHub;


        public MovieController(IConfiguration config, IHubContext<NotificationHub> notificationHub)
        {
            this.config = config;
            string connName = new ConnHelper().GetConnName();
            mc = new MovieContainer(new MovieDAL(config, connName));
            _notificationHub = notificationHub;
        }



        [HttpGet]
        [Route("all")]
        public ActionResult GetMovies()
        {
            ICollection<Movie> movies = mc.GetAllMovies();

            if (movies.Count == 0)
            {
                return NotFound("No movies found");
            }

            foreach (Movie movie in movies)
            {
                foreach (Genre genre in movie.MovieGenres)
                {
                    genre.Movies.Clear();
                }
                movie.UserMovies.Clear();
            }



            return Ok(movies);
        }

        [HttpGet]
        [Route("filter/{genreid}")]
        public ActionResult GetMoviesByGenre(string genreid)
        {
            GenreDAL test = new GenreDAL(config, new ConnHelper().GetConnName());

            GenreContainer genreContainer = new GenreContainer(test);

            if (!idValidator.ValidateGenreID(genreContainer, genreid))
            {
                return BadRequest(idValidator.errorMessage);
            }

            ICollection<Movie> movies = mc.GetMoviesByGenre(genreContainer.GetGenre(Convert.ToInt32(genreid)));

            if (movies.Count == 0)
            {
                return NotFound("No movies found with this genre");
            }

            foreach (Movie movie in movies)
            {
                foreach (Genre genre in movie.MovieGenres)
                {
                    genre.Movies.Clear();
                }
                movie.UserMovies.Clear();
            }


            return Ok(movies);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult FindMovie(string id)
        {
            if (!idValidator.ValidateMovieID(mc, id))
            {
                return NotFound(idValidator.errorMessage);
            }

            Movie movie = mc.GetMovie(Convert.ToInt32(id));

            movie.UserMovies.Clear();
            foreach (Genre genre in movie.MovieGenres)
            {
                genre.Movies.Clear();
            }

            return Ok(mc.GetMovie(Convert.ToInt32(id)));
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddMovie([FromBody] Movie movie)
        {
            if (mc.AddMovie(movie))
            {
                Message msg = new Message { msgHeader = "New Movie added", msgContext = "The movie " + movie.Title + " has been added to " + movie.Platform + "!" };

                await _notificationHub.Clients.All.SendAsync("sendToUser", msg.msgHeader, msg.msgContext);
                return Ok("Movie has been Added!");
            }
            else
            {
                return BadRequest("This movie is already in the database!");
            }
        }
    }
}
