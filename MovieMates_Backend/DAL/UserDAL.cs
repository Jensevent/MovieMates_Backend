using Microsoft.Extensions.Configuration;
using MovieMates_Backend.Entities;
using MovieMates_Backend.Interfaces;
using MovieMates_Backend.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieMates_Backend.DAL
{
    public class UserDAL : IUserDAL
    {
        readonly DatabaseContext db;

        public UserDAL(IConfiguration config, string connName)
        {
            db = new DatabaseContext(config, connName);
        }

        public bool CreateUser(User user)
        {
            string[] passwordSaltHash = new PasswordHandler().GenerateSaltAndHash(user.PasswordSalt);
            user.PasswordHash = passwordSaltHash[0];
            user.PasswordSalt = passwordSaltHash[1];

            db.Users.Add(user);
            db.SaveChanges();
            return true;
        }

        public ICollection<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public User GetUser(Guid userID)
        {
            return db.Users.Where(u => u.ID == userID).FirstOrDefault();
        }

        public User GetUser(string username)
        {
            return db.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public ICollection<Group> GetUserGroups(Guid userID)
        {
            User user = db.Users.Where(u => u.ID == userID).FirstOrDefault();

            return user.Groups;
        }

        public ICollection<UserMovie> GetUserMovies(Guid userID)
        {
            User myUser = db.Users.Where(u => u.ID == userID).FirstOrDefault();
            ICollection<UserMovie> userMovies = myUser.UserMovies;

            foreach (UserMovie userMovie in userMovies)
            {
                userMovie.Movie.UserMovies.Clear();


                foreach (Genre genre in userMovie.Movie.MovieGenres)
                {
                    genre.Movies.Clear();
                }
            }



            return userMovies;
        }

        public bool UpdateUser()
        {
            throw new NotImplementedException();
        }

        public bool UserIDExists(Guid userID)
        {
            if (db.Users.Where(u => u.ID == userID).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }

        public bool UserLikesMovie(Guid userID, int movieID)
        {
            UserMovie userMovie = new UserMovie { MovieID = movieID, UserID = userID };

            if (db.UserMovies.Where(u => u.UserID == userID && u.MovieID == movieID).FirstOrDefault() != null)
            {
                return false;
            }


            db.UserMovies.Add(userMovie);
            db.SaveChanges();
            return true;
        }

        public bool UsernameExists(string username)
        {
            User user = db.Users.Where(u => u.Username == username).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public bool UserRatedMovie(Guid userID, int movieID, double rating)
        {
            UserMovie userMovie = db.UserMovies.Where(u => u.UserID == userID && u.MovieID == movieID).FirstOrDefault();

            if (userMovie != null)
            {
                userMovie.UserRating = rating;
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool UserWatchedMovie(Guid userID, int movieID, bool watched)
        {
            UserMovie userMovie = db.UserMovies.Where(u => u.UserID == userID && u.MovieID == movieID).FirstOrDefault();

            if (userMovie != null)
            {
                userMovie.Watched = watched;
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool ValidateLogin(string username, string password)
        {
            User user = db.Users.Where(u => u.Username == username).FirstOrDefault();
            if (user == null)
            {
                // username does not exist
                return false;
            }

            if (!new PasswordHandler().VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                //password and username dont match
                return false;
            }

            return true;
        }


    }
}
