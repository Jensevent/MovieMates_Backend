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
    public class GroupController : Controller
    {
        readonly IConfiguration config;
        readonly GroupContainer gc;
        readonly IDValidator idValidator = new IDValidator();

        public GroupController(IConfiguration config)
        {
            this.config = config;
            string connName = new ConnHelper().GetConnName();
            gc = new GroupContainer(new GroupDAL(config, connName));
        }


        [HttpGet]
        [Route("all")]
        public ActionResult GetAllGroups()
        {
            ICollection<Group> groups = gc.GetAllGroups();

            if (groups.Count == 0)
            {
                return NotFound("No groups exists.");
            }

            foreach (Group group in groups)
            {
                foreach (User user in group.Members)
                {
                    user.Groups.Clear();
                }
            }


            return Ok(groups);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetGroup(string id)
        {
            if (!idValidator.ValidateGroupID(gc, id))
            {
                return BadRequest(idValidator.errorMessage);
            }

            Group group = gc.GetGroup(Convert.ToInt32(id));
            foreach (User user in group.Members)
            {
                user.Groups.Clear();
            }


            return Ok(group);
        }

        [HttpGet]
        [Route("{id}/members")]
        public ActionResult GetGroupMembers(string id)
        {
            if (!idValidator.ValidateGroupID(gc, id))
            {
                return BadRequest(idValidator.errorMessage);
            }


            ICollection<User> users = gc.GetGroupMembers(Convert.ToInt32(id));

            if (users.Count != 0)
            {

                foreach (User user in users)
                {
                    user.Groups.Clear();
                }

                return Ok(users);
            }
            return NotFound("This group has no users");


        }

        [HttpGet]
        [Route("{id}/movies")]
        public ActionResult GetGroupMovies(string id)
        {
            if (!idValidator.ValidateGroupID(gc, id))
            {
                return BadRequest(idValidator.errorMessage);
            }

            ICollection<Movie> movies = gc.GetGroupMovies(Convert.ToInt32(id));
            if (movies.Count != 0)
            {
                return Ok(movies);
            }
            return NotFound("The users of this group have no movies!");

        }


        [HttpPost]
        [Route("create/{name}")]
        public ActionResult CreateGroup(string name)
        {
            return Ok(gc.CreateGroup(name));
        }



        //[HttpPatch]
        //[Route("{groupid}/change/{name}")]
        //public ActionResult ChangeGroupName(string groupid, string name)
        //{
        //    if (!idValidator.ValidateGroupID(gc,groupid))
        //    {
        //        return BadRequest(idValidator.errorMessage);
        //    }

        //    return Ok("Name has been changed!");
        //}




        [HttpPatch]
        [Route("{groupid}/add/{userid}")]
        //public ActionResult AddUserToGroup(string groupid, string userid)
        public ActionResult AddUserToGroup(string groupid, Guid userid)
        {
            if (!idValidator.ValidateJoinGroupID(gc, groupid) || !idValidator.ValidateUserID(new UserContainer(new UserDAL(config, new ConnHelper().GetConnName())), userid.ToString()))
            {
                return NotFound(idValidator.errorMessage);
            }

            if (!gc.AddUserToGroup(groupid, userid))
            {
                return BadRequest("This user is already in this group!");
            }

            return Ok("User has been added to group!");
        }
    }
}
