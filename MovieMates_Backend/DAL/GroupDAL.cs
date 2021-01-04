using Microsoft.Extensions.Configuration;
using MovieMates_Backend.Entities;
using MovieMates_Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieMates_Backend.DAL
{
    public class GroupDAL : IGroupDAL
    {
        readonly DatabaseContext db;

        public GroupDAL(IConfiguration config, string connName)
        {
            db = new DatabaseContext(config, connName);
        }

        public bool AddUserToGroup(string joinGroupID, Guid userID)
        {
            Group group = db.Groups.Where(g => g.JoinID == joinGroupID).FirstOrDefault();
            User user = db.Users.Where(u => u.ID == userID).FirstOrDefault();


            foreach (User myUser in group.Members)
            {
                if (myUser.ID == user.ID)
                {
                    return false;
                }
            }

            group.Members.Add(user);
            db.SaveChanges();
            return true;
        }

        public Group CreateGroup(string name)
        {
            Group group = new Group { Name = name };
            db.Groups.Add(group);
            db.SaveChanges();
            return group;
        }

        public ICollection<Group> GetAllGroups()
        {
            return db.Groups.ToList();
        }

        public Group GetGroup(int groupID)
        {
            return db.Groups.Where(g => g.ID == groupID).FirstOrDefault();
        }

        public ICollection<User> GetGroupMembers(int groupID)
        {
            Group group = db.Groups.Where(g => g.ID == groupID).FirstOrDefault();
            return group.Members;
        }

        public ICollection<Movie> GetGroupMovies(int groupID)
        {
            Group group = db.Groups.Where(g => g.ID == groupID).FirstOrDefault();


            List<Movie> movies = new List<Movie>();
            foreach (User user in group.Members)
            {

                foreach (UserMovie userMovie in user.UserMovies)
                {
                    Movie movie = userMovie.Movie;

                    if (movies.Contains(movie) || userMovie.Watched == true)
                    {
                        int x = movie.Amount;
                        movie.Amount = x + 1;
                    }
                    else
                    {
                        foreach (Genre genre in movie.MovieGenres)
                        {
                            genre.Movies.Clear();
                        }
                        movie.Amount = 1;
                        movies.Add(movie);
                    }
                }
            }

            foreach (Movie movie in movies)
            {
                movie.UserMovies.Clear();
            }

            var newlist = movies.OrderByDescending(m => m.Amount).ToList();
            return newlist;
        }

        public bool GroupIDExists(int groupID)
        {
            if (db.Groups.Where(g => g.ID == groupID).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }

        public bool JoinGroupIDExists(string groupID)
        {
            if (db.Groups.Where(g => g.JoinID == groupID).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
    }
}
