using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieMates_Backend.Containers;
using MovieMates_Backend.DAL;
using MovieMates_Backend.DAL.Db;
using MovieMates_Backend.Entities;
using MovieMates_Backend.Tools;
using System;
using System.Collections.Generic;

namespace MovieMates_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        readonly UserContainer uc;
        readonly MovieContainer mc;
        readonly IDValidator idValidator = new IDValidator();

        public UserController(IConfiguration config)
        {
            string connName = new ConnHelper().GetConnName();
            uc = new UserContainer(new UserDAL(config, connName));
            mc = new MovieContainer(new MovieDAL(config, connName));
        }



        [HttpGet]
        [Route("all")]
        public ActionResult GetAllUsers()
        {
            ICollection<User> users = uc.GetAllUsers();

            if (users.Count == 0)
            {
                return NotFound("No users exists.");
            }

            foreach (User user in users)
            {
                foreach (Group group in user.Groups)
                {
                    group.Members.Clear();
                }
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetUser(string id)
        {
            if (!idValidator.ValidateUserID(uc, id))
            {
                return BadRequest(idValidator.errorMessage);
            }

            User myUser = uc.GetUser(new Guid(id));

            return Ok(myUser);
        }

        [HttpGet]
        [Route("find/{username}")]
        public ActionResult GetUserByUsername(string username)
        {
            User myUser = uc.GetUser(username);

            if (myUser == null)
            {
                return NotFound("That user does not exist!");
            }
            return Ok(myUser);
        }

        [HttpGet]
        [Route("movies/{userID}")]
        public ActionResult GetUserMovies(string userID)
        {
            if (!idValidator.ValidateUserID(uc, userID))
            {
                return NotFound(idValidator.errorMessage);
            }

            ICollection<UserMovie> myMovies = uc.GetUserMovies(new Guid(userID));
            if (myMovies.Count == 0)
            {
                return BadRequest("This user has no movies!");
            }

            return Ok(myMovies);

        }

        [HttpGet]
        [Route("{userID}/groups")]
        public ActionResult GetUserGroups(string userID)
        {
            if (!idValidator.ValidateUserID(uc, userID))
            {
                return NotFound(idValidator.errorMessage);
            }

            ICollection<Group> myGroups = uc.GetUserGroups(new Guid(userID));
            if (myGroups.Count == 0)
            {
                return BadRequest("This user has no groups!");
            }

            return Ok(myGroups);

        }

        //HTTP patch values
        [HttpPatch]
        [Route("likes/{userid}/{movieid}")]
        public ActionResult UserLikesMovie(string userid, string movieid)
        {
            if (!idValidator.ValidateUserID(uc, userid) || !idValidator.ValidateMovieID(mc, movieid))
            {
                return BadRequest(idValidator.errorMessage);
            }


            if (uc.UserLikesMovie(new Guid(userid), Convert.ToInt32(movieid)))
            {
                return Ok("The movie has been saved!");
            }

            return NotFound("The user has already saved this movie.");
        }

        [HttpPatch]
        [Route("watched/{userid}/{movieid}/{watched}")]
        public ActionResult UserWatchedMovie(string userid, string movieid, bool watched)
        {
            if (!idValidator.ValidateUserID(uc, userid) || !idValidator.ValidateMovieID(mc, movieid))
            {
                return BadRequest(idValidator.errorMessage);
            }

            if (uc.UserWatchedMovie(new Guid(userid), Convert.ToInt32(movieid), watched))
            {
                return Ok("This movie has been marked as watched!");
            }

            return NotFound("This user does not have this movie saved.");
        }

        [HttpPatch]
        [Route("rated/{userid}/{movieid}/{rating}")]
        public ActionResult UserRatedMovie(string userid, string movieid, double rating)
        {
            if (!idValidator.ValidateUserID(uc, userid) || !idValidator.ValidateMovieID(mc, movieid))
            {
                return BadRequest(idValidator.errorMessage);
            }

            if (uc.UserRatedMovie(new Guid(userid), Convert.ToInt32(movieid), rating))
            {
                return Ok("U rated this movie an" + rating.ToString() + ".");
            }

            return NotFound("This user does not have this movie saved.");
        }



        //Authentication
        [HttpPost]
        [Route("register")]
        public ActionResult CreateUser([FromForm] User user)
        {
            UserAuthenticator userAuthenticator = new UserAuthenticator();

            if (userAuthenticator.ValidateUser(user))
            {
                if (!uc.UsernameExists(user.Username))
                {
                    uc.CreateUser(user);
                    return Ok("User has been created!");
                }

                return NotFound("This username already exists!");
            }

            return BadRequest(userAuthenticator.errorMessage);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromForm] string username, [FromForm] string password)
        {
            if (!uc.UsernameExists(username))
            {
                return NotFound("No user exists with this username.");
            }

            if (!uc.ValidateLogin(username, password))
            {
                return BadRequest("Your password is incorrect!");
            }

            return Ok("Your login was succesful!");
        }
    }
}
