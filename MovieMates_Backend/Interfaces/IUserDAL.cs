using MovieMates_Backend.Entities;
using System;
using System.Collections.Generic;

namespace MovieMates_Backend.Interfaces
{
    public interface IUserDAL
    {
        public ICollection<User> GetAllUsers();
        public User GetUser(Guid userID);
        public User GetUser(string username);
        public ICollection<UserMovie> GetUserMovies(Guid userID);
        public bool CreateUser(User user);
        public bool UpdateUser();
        public bool ValidateLogin(string username, string password);
        public bool UsernameExists(string username);
        public bool UserLikesMovie(Guid userID, int movieID);
        public bool UserWatchedMovie(Guid userID, int movieID, bool watched);
        public bool UserRatedMovie(Guid userID, int movieID, double rating);
        public bool UserIDExists(Guid userID);

        public ICollection<Group> GetUserGroups(Guid userID);
    }
}
