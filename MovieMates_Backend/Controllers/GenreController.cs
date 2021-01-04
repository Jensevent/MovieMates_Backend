using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieMates_Backend.Containers;
using MovieMates_Backend.DAL;
using MovieMates_Backend.DAL.Db;
using MovieMates_Backend.Entities;
using System.Collections.Generic;

namespace MovieMates_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        readonly GenreContainer genreContainer;

        public GenreController(IConfiguration config)
        {
            string connName = new ConnHelper().GetConnName();

            genreContainer = new GenreContainer(new GenreDAL(config, connName));
        }


        [HttpGet]
        [Route("all")]
        public ActionResult GetGenres()
        {
            ICollection<Genre> genres = genreContainer.GetAllGenres();
            if (genres.Count == 0)
            {
                return NotFound("There are no genres");
            }

            return Ok(genres);
        }
    }
}
