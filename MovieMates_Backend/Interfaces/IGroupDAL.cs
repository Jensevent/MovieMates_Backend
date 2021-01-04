using MovieMates_Backend.Entities;
using System;
using System.Collections.Generic;

namespace MovieMates_Backend.Interfaces
{
    public interface IGroupDAL
    {
        public ICollection<Group> GetAllGroups();
        public ICollection<User> GetGroupMembers(int groupID);
        public Group GetGroup(int groupID);
        public bool GroupIDExists(int groupID);
        public bool JoinGroupIDExists(string groupID);
        public Group CreateGroup(string name);
        public bool AddUserToGroup(string joinGroupID, Guid userID);
        public ICollection<Movie> GetGroupMovies(int groupID);

    }
}
