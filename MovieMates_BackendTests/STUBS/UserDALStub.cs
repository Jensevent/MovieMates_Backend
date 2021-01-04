using MovieMates_Backend.Entities;
using System;
using System.Collections.Generic;

namespace MovieMates_BackendTests.STUBS
{
    class UserDALStub : MovieMates_Backend.Interfaces.IUserDAL
    {
        readonly User testUser = new User { Username = "test", Email = "test", PasswordHash = "test", PasswordSalt = "test" };
        public bool? TestValue = null;

        public bool CreateUser(User user)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public ICollection<User> GetAllUsers()
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            ICollection<User> users = null;
            if (TestValue.Value)
            {
                users.Add(testUser);
            }
            return users;
        }

        public User GetUser(Guid userID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            User user = null;
            if (TestValue.Value)
            {
                user = testUser;
            }
            return user;
        }
        public User GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser()
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public bool UserIDExists(Guid userID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public bool UserLikesMovie(Guid userID, int movieID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public bool UsernameExists(string username)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public bool UserRatedMovie(Guid userID, int movieID, double rating)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public bool UserWatchedMovie(Guid userID, int movieID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue please");
            }

            return TestValue.Value;
        }

        public bool ValidateLogin(string username, string password)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public bool UserWatchedMovie(Guid userID, int movieID, bool watched)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        

        public ICollection<UserMovie> GetUserMovies(Guid userID)
        {
            throw new NotImplementedException();
        }

        public ICollection<Group> GetUserGroups(Guid userID)
        {
            throw new NotImplementedException();
        }
    }
}
