using MovieMates_Backend.Containers;
using System;
using System.Text.RegularExpressions;

namespace MovieMates_Backend.Tools
{
    public class IDValidator
    {
        public string errorMessage { get; private set; }


        public bool ValidateUserID(UserContainer uc, string userID)
        {
            if (String.IsNullOrEmpty(userID))
            {
                errorMessage = "The userID has not been entered.";
                return false;
            }

            try
            {
                _ = new Guid(userID);
            }
            catch (FormatException)
            {
                errorMessage = "This userID is not formatted correctly.";
                return false;
            }

            if (!uc.UserIDExists(new Guid(userID)))
            {
                errorMessage = "This userID does not exist.";
                return false;
            }

            return true;
        }

        public bool ValidateGroupID(GroupContainer gc, string groupID)
        {
            if (String.IsNullOrEmpty(groupID))
            {
                errorMessage = "The movieID has not been entered.";
                return false;
            }

            if (!OnlyNumbers(groupID))
            {
                errorMessage = "This movieID is not formatted correctly.";
                return false;
            }

            if (!gc.GroupIDExists(Convert.ToInt32(groupID)))
            {
                errorMessage = "This movieID does not exist.";
                return false;
            }

            return true;
        }

        public bool ValidateJoinGroupID(GroupContainer gc, string joinGroupID)
        {
            if (String.IsNullOrEmpty(joinGroupID))
            {
                errorMessage = "The groupID has not been entered.";
                return false;
            }

            if (!OnlyNumbers(joinGroupID))
            {
                errorMessage = "This groupID is not formatted correctly.";
                return false;
            }

            if (!gc.JoinGroupIDExists(joinGroupID))
            {
                errorMessage = "This groupID does not exist.";
                return false;
            }

            return true;
        }




        public bool ValidateMovieID(MovieContainer mc, string movieID)
        {
            if (String.IsNullOrEmpty(movieID))
            {
                errorMessage = "The movieID has not been entered.";
                return false;
            }

            if (!OnlyNumbers(movieID))
            {
                errorMessage = "This movieID is not formatted correctly.";
                return false;
            }

            if (!mc.MovieIDExists(Convert.ToInt32(movieID)))
            {
                errorMessage = "This movieID does not exist.";
                return false;
            }

            return true;
        }



        public bool ValidateGenreID(GenreContainer gc, string genreID)
        {
            if (String.IsNullOrEmpty(genreID))
            {
                errorMessage = "The genreID has not been entered.";
                return false;
            }
            if (!OnlyNumbers(genreID))
            {
                errorMessage = "This genreID has not been formatted correctly.";
                return false;
            }
            if (!gc.GenreIDExists(Convert.ToInt32(genreID)))
            {
                errorMessage = "This genreID does not exist.";
                return false;
            }

            return true;
        }



        private bool OnlyNumbers(string text)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (regex.IsMatch(text))
            {
                return true;
            }
            return false;
        }
    }



}
