using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieMates_Backend.Entities;

namespace MovieMates_Backend.Tools.Tests
{
    [TestClass()]
    public class UserAuthenticatorTests
    {
        UserAuthenticator validator;


        [TestInitialize]
        public void TestInitialize()
        {
            validator = new UserAuthenticator();
        }


        //Validate Username
        [TestMethod()]
        public void UserNameEmptyOrNull()
        {
            Assert.IsFalse(validator.ValidateUsername(""));
            Assert.IsFalse(validator.ValidateUsername(null));
        }

        [TestMethod()]
        public void UserNameTooShort()
        {
            Assert.IsFalse(validator.ValidateUsername("a"));
            Assert.IsFalse(validator.ValidateUsername("ab"));
            Assert.IsFalse(validator.ValidateUsername("abc"));
        }

        [TestMethod()]
        public void UserNameWrongFormat()
        {
            Assert.IsFalse(validator.ValidateUsername("12345654321"));
        }

        [TestMethod()]
        public void UserNameCorrect()
        {
            Assert.IsTrue(validator.ValidateUsername("Jensevent"));
            Assert.IsTrue(validator.ValidateUsername("Jens12345"));
        }



        //Validate Email
        [TestMethod()]
        public void EmailEmptyOrNull()
        {
            Assert.IsFalse(validator.ValidateEmail(""));
            Assert.IsFalse(validator.ValidateEmail(null));
        }

        [TestMethod()]
        public void EmailWrongFormat()
        {
            // No @ or TLD (.com /.nl / .be etc.)
            Assert.IsFalse(validator.ValidateEmail("test"));

            // No TLD
            Assert.IsFalse(validator.ValidateEmail("test@gmail"));

            // TLD is too short / too long (min 2 char, max 4)
            Assert.IsFalse(validator.ValidateEmail("Test@Gmail.t"));
            Assert.IsFalse(validator.ValidateEmail("Test@Gmail.tests"));
        }

        [TestMethod()]
        public void EmailCorrect()
        {
            Assert.IsTrue(validator.ValidateEmail("test@gmail.com"));
        }



        //Validate Password
        [TestMethod()]
        public void PasswordEmptyOrNull()
        {
            Assert.IsFalse(validator.ValidatePassword("", ""));
            Assert.IsFalse(validator.ValidatePassword("a", ""));
            Assert.IsFalse(validator.ValidatePassword("", "b"));

            Assert.IsFalse(validator.ValidatePassword(null, ""));
            Assert.IsFalse(validator.ValidatePassword(null, "a"));
            Assert.IsFalse(validator.ValidatePassword("", null));
            Assert.IsFalse(validator.ValidatePassword("a", null));

            Assert.IsFalse(validator.ValidatePassword(null, null));
        }

        [TestMethod()]
        public void PasswordsDontMatch()
        {
            Assert.IsFalse(validator.ValidatePassword("DwarfStar12", "Welkom12345"));
            Assert.IsFalse(validator.ValidatePassword("Welkom12345", "DwarfStar12"));
        }

        [TestMethod()]
        public void PasswordWrongFormat()
        {
            /* Password must contain :
               - At least 8 characters
               - At least one capital letter and one lowercase letter  
               - At least one number
            */

            Assert.IsFalse(validator.ValidatePassword("Dwarf", "Dwarf"));
            Assert.IsFalse(validator.ValidatePassword("dwarfstar", "dwarfstar"));
            Assert.IsFalse(validator.ValidatePassword("DWARFSTAR", "DWARFSTAR"));
            Assert.IsFalse(validator.ValidatePassword("DwarfStar", "DwarfStar"));
        }

        [TestMethod()]
        public void PasswordCorrect()
        {
            Assert.IsTrue(validator.ValidatePassword("DwarfStar12", "DwarfStar12"));
        }



        //Validate User
        [TestMethod()]
        public void UsernameIncorrect()
        {
            User testuser = new User { Username = "a", Email = "dwarfstar@gmail.com", PasswordHash = "DwarfStar12", PasswordSalt = "DwarfStar12" };

            Assert.IsFalse(validator.ValidateUser(testuser));
        }

        [TestMethod()]
        public void EmailIncorrect()
        {
            User testuser = new User { Username = "DwarfStar", Email = "test", PasswordHash = "DwarfStar12", PasswordSalt = "DwarfStar12" };

            Assert.IsFalse(validator.ValidateUser(testuser));
        }

        [TestMethod()]
        public void PasswordsIncorrect()
        {
            User testuser = new User { Username = "DwarfStar", Email = "dwarfstar@gmail.com", PasswordHash = "a", PasswordSalt = "a" };

            Assert.IsFalse(validator.ValidateUser(testuser));
        }

        [TestMethod()]
        public void UserCorrect()
        {
            User testuser = new User { Username = "DwarfStar", Email = "dwarfstar@gmail.com", PasswordHash = "DwarfStar12", PasswordSalt = "DwarfStar12" };

            Assert.IsTrue(validator.ValidateUser(testuser));
        }
    }
}