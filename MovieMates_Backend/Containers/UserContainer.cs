using MovieMates_Backend.Entities;
using MovieMates_Backend.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieMates_Backend.Containers
{
    public class UserContainer : IUserDAL
    {
        readonly IUserDAL iUser;

        public UserContainer(IUserDAL iUser)
        {
            this.iUser = iUser;
        }

        public bool CreateUser(User user)
        {
            return iUser.CreateUser(user);
        }

        public ICollection<User> GetAllUsers()
        {
            return iUser.GetAllUsers();
        }

        public User GetUser(Guid userID)
        {
            return iUser.GetUser(userID);
        }

        public User GetUser(string username)
        {
            return iUser.GetUser(username);
        }

        public ICollection<Group> GetUserGroups(Guid userID)
        {
            return iUser.GetUserGroups(userID);
        }

        public ICollection<UserMovie> GetUserMovies(Guid userID)
        {
            return iUser.GetUserMovies(userID);
        }

        public bool UpdateUser()
        {
            return iUser.UpdateUser();
        }

        public bool UserIDExists(Guid userID)
        {
            return iUser.UserIDExists(userID);
        }

        public bool UserLikesMovie(Guid userID, int movieID)
        {
            return iUser.UserLikesMovie(userID, movieID);
        }

        public bool UsernameExists(string username)
        {
            return iUser.UsernameExists(username);
        }

        public bool UserRatedMovie(Guid userID, int movieID, double rating)
        {
            return iUser.UserRatedMovie(userID, movieID, rating);
        }

        public bool UserWatchedMovie(Guid userID, int movieID, bool watched)
        {
            return iUser.UserWatchedMovie(userID, movieID, watched);
        }

        public bool ValidateLogin(string username, string password)
        {
            return iUser.ValidateLogin(username, password);
        }
    }
}
