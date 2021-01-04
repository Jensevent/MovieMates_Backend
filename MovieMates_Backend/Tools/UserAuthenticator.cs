using MovieMates_Backend.Entities;
using System;
using System.Text.RegularExpressions;

namespace MovieMates_Backend.Tools
{
    public class UserAuthenticator
    {
        public string errorMessage;

        public bool ValidateUser(User user)
        {
            if (!ValidateUsername(user.Username) || !ValidateEmail(user.Email) || !ValidatePassword(user.PasswordHash, user.PasswordSalt))
            {
                return false;
            }
            return true;
        }

        public bool ValidateEmail(string email)
        {

            if (String.IsNullOrEmpty(email))
            {
                errorMessage = "Please enter an email.";
                return false;
            }

            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            if (!regex.IsMatch(email))
            {
                errorMessage = "This is not an email. Please enter a correct email.";
                return false;
            }

            return true;
        }

        public bool ValidateUsername(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                errorMessage = "Please enter an username.";
                return false;
            }

            if (username.Length < 4)
            {
                errorMessage = "Your username must contain atleast 4 characters.";
                return false;
            }

            if (OnlyNumbers(username))
            {
                errorMessage = "Your username must have atleast 1 number.";
                return false;
            }

            return true;
        }

        public bool ValidatePassword(string password, string repeatPassword)
        {
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(repeatPassword))
            {
                errorMessage = "Please enter a password.";
                return false;
            }

            if (password != repeatPassword)
            {
                errorMessage = "The passwords dont match.";
                return false;
            }

            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
            if (!regex.IsMatch(password))
            {
                errorMessage = "This password does not suffice.";
                return false;
            }

            return true;
        }

        //Regex
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
