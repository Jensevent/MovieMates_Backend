using MovieMates_Backend.Entities;
using MovieMates_Backend.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieMates_Backend.Containers
{
    public class GroupContainer : IGroupDAL
    {
        readonly IGroupDAL iGroup;

        public GroupContainer(IGroupDAL iGroup)
        {
            this.iGroup = iGroup;
        }

        public bool AddUserToGroup(string joinGroupID, Guid userID)
        {
            return iGroup.AddUserToGroup(joinGroupID, userID);
        }


        public Group CreateGroup(string name)
        {
            return iGroup.CreateGroup(name);
        }

        public ICollection<Group> GetAllGroups()
        {
            return iGroup.GetAllGroups();
        }

        public Group GetGroup(int groupID)
        {
            return iGroup.GetGroup(groupID);
        }

        public ICollection<User> GetGroupMembers(int groupID)
        {
            return iGroup.GetGroupMembers(groupID);
        }

        public ICollection<Movie> GetGroupMovies(int groupID)
        {
            return iGroup.GetGroupMovies(groupID);
        }

        public bool GroupIDExists(int groupID)
        {
            return iGroup.GroupIDExists(groupID);
        }

        public bool JoinGroupIDExists(string groupID)
        {
            return iGroup.JoinGroupIDExists(groupID);
        }
    }
}
