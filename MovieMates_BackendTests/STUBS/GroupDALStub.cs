using MovieMates_Backend.Entities;
using MovieMates_Backend.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieMates_BackendTests.STUBS
{
    class GroupDALStub : IGroupDAL
    {
        readonly Group testGroup = new Group { Name = "Action" };
        public bool? TestValue = null;

        public ICollection<Group> GetAllGroups()
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            ICollection<Group> groups = null;
            if (TestValue.Value)
            {
                groups.Add(testGroup);
            }
            return groups;
        }

        public Group GetGroup(int groupID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            Group group = null;
            if (TestValue.Value)
            {
                group = testGroup;
            }
            return group;
        }

        public ICollection<User> GetGroupMembers(int groupID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            ICollection<User> groupMembers = null;
            if (TestValue.Value)
            {
                groupMembers.Add(new User { Username = "Tarzan", Email = "TarzanLovesJane@Jungle.com", PasswordHash = "Lorem Ipsum", PasswordSalt = "Lorem Ipsum" });
            }
            return groupMembers;
        }

        public bool GroupIDExists(int groupID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public bool JoinGroupIDExists(string groupID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public Group CreateGroup(string name)
        {
            throw new NotImplementedException();
        }

        public bool AddUserToGroup(string joinGroupID, Guid userID)
        {
            throw new NotImplementedException();
        }

        public ICollection<Movie> GetGroupMovies(int groupID)
        {
            throw new NotImplementedException();
        }
    }
}
