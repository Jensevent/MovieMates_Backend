using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieMates_Backend.Containers;
using MovieMates_BackendTests.STUBS;

namespace MovieMates_Backend.Tools.Tests
{
    [TestClass()]
    public class IDValidatorTests
    {
        IDValidator validator;

        UserDALStub userStub;
        UserContainer uc;

        MovieDALStub movieStub;
        MovieContainer mc;

        GenreDALStub genreStub;
        GenreContainer genrec;

        GroupDALStub groupStub;
        GroupContainer groupc;

        [TestInitialize]
        public void TestInitialize()
        {
            validator = new IDValidator();

            userStub = new UserDALStub();
            uc = new UserContainer(userStub);

            movieStub = new MovieDALStub();
            mc = new MovieContainer(movieStub);

            genreStub = new GenreDALStub();
            genrec = new GenreContainer(genreStub);

            groupStub = new GroupDALStub();
            groupc = new GroupContainer(groupStub);
        }


        //Validate errorMessage
        [TestMethod()]
        public void GetErrorMessage()
        {
            Assert.IsNull(validator.errorMessage);

            userStub.TestValue = false;
            validator.ValidateUserID(uc, "f2c331db-5e13-4e49-b7f2-8dc47aafe049");
            Assert.IsNotNull(validator.errorMessage);
        }




        // Validate UserID
        [TestMethod()]
        public void UserIDEmptyOrNull()
        {
            userStub.TestValue = true;
            Assert.IsFalse(validator.ValidateUserID(uc, ""));
            Assert.IsFalse(validator.ValidateUserID(uc, null));
        }

        [TestMethod()]
        public void UserIDWrongFormat()
        {
            userStub.TestValue = true;
            Assert.IsFalse(validator.ValidateUserID(uc, "1234567890987654321234567890"));
        }

        [TestMethod()]
        public void UserIDNotExist()
        {
            userStub.TestValue = false;
            Assert.IsFalse(validator.ValidateUserID(uc, "f2c331db-5e13-4e49-b7f2-8dc47aafe049"));
        }

        [TestMethod()]
        public void UserIDCorrect()
        {
            userStub.TestValue = true;
            Assert.IsTrue(validator.ValidateUserID(uc, "f2c331db-5e13-4e49-b7f2-8dc47aafe049"));
        }



        //Validate MovieID
        [TestMethod()]
        public void MovieIDEmptyOrNull()
        {
            movieStub.TestValue = true;
            Assert.IsFalse(validator.ValidateMovieID(mc, ""));
            Assert.IsFalse(validator.ValidateMovieID(mc, null));
        }

        [TestMethod()]
        public void MovieIDWrongFormat()
        {
            movieStub.TestValue = true;
            Assert.IsFalse(validator.ValidateMovieID(mc, "abcdefg"));
            Assert.IsFalse(validator.ValidateMovieID(mc, "abc123"));
        }

        [TestMethod()]
        public void movieIDNotExist()
        {
            movieStub.TestValue = false;
            Assert.IsFalse(validator.ValidateMovieID(mc, "1"));
        }

        [TestMethod()]
        public void movieIDCorrect()
        {
            movieStub.TestValue = true;
            Assert.IsTrue(validator.ValidateMovieID(mc, "1"));
        }



        //Validate GenreID
        [TestMethod()]
        public void GenreIDEmptyOrNull()
        {
            genreStub.TestValue = true;
            Assert.IsFalse(validator.ValidateGenreID(genrec, ""));
            Assert.IsFalse(validator.ValidateGenreID(genrec, null));
        }

        [TestMethod()]
        public void GenreIDWrongFormat()
        {
            genreStub.TestValue = true;
            Assert.IsFalse(validator.ValidateGenreID(genrec, "abcdefg"));
            Assert.IsFalse(validator.ValidateGenreID(genrec, "abc123"));
        }

        [TestMethod()]
        public void GenreIDNotExist()
        {
            genreStub.TestValue = false;
            Assert.IsFalse(validator.ValidateGenreID(genrec, "1"));
        }

        [TestMethod()]
        public void GenreIDCorrect()
        {
            genreStub.TestValue = true;
            Assert.IsTrue(validator.ValidateGenreID(genrec, "1"));
        }



        //Validate GroupID
        [TestMethod()]
        public void GroupIDEmptyOrNull()
        {
            groupStub.TestValue = true;
            Assert.IsFalse(validator.ValidateGroupID(groupc, ""));
            Assert.IsFalse(validator.ValidateGroupID(groupc, null));
        }

        [TestMethod()]
        public void GroupIDWrongFormat()
        {
            groupStub.TestValue = true;
            Assert.IsFalse(validator.ValidateGroupID(groupc, "abcdefg"));
            Assert.IsFalse(validator.ValidateGroupID(groupc, "abc123"));
        }

        [TestMethod()]
        public void GroupIDNotExist()
        {
            groupStub.TestValue = false;
            Assert.IsFalse(validator.ValidateGroupID(groupc, "1"));
        }

        [TestMethod()]
        public void GroupIDCorrect()
        {
            groupStub.TestValue = true;
            Assert.IsTrue(validator.ValidateGroupID(groupc, "1"));
        }



        //Validate JoinGroupID
        [TestMethod()]
        public void JoinGroupIDEmptyOrNull()
        {
            groupStub.TestValue = true;
            Assert.IsFalse(validator.ValidateJoinGroupID(groupc, ""));
            Assert.IsFalse(validator.ValidateJoinGroupID(groupc, null));
        }

        [TestMethod()]
        public void JoinGroupIDWrongFormat()
        {
            groupStub.TestValue = true;
            Assert.IsFalse(validator.ValidateJoinGroupID(groupc, "abcdefg"));
            Assert.IsFalse(validator.ValidateJoinGroupID(groupc, "abc123"));
        }

        [TestMethod()]
        public void JoinGroupIDNotExist()
        {
            groupStub.TestValue = false;
            Assert.IsFalse(validator.ValidateJoinGroupID(groupc, "123456"));
        }

        [TestMethod()]
        public void JoinGroupIDCorrect()
        {
            groupStub.TestValue = true;
            Assert.IsTrue(validator.ValidateJoinGroupID(groupc, "123456"));
        }
    }
}