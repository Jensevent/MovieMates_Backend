using MovieMates_Backend.Entities;
using System;
using System.Collections.Generic;

namespace MovieMates_BackendTests.STUBS
{
    class GenreDALStub : MovieMates_Backend.Interfaces.IGenreDAL
    {
        readonly Genre testGenre = new Genre { GenreName = "Action" };
        public bool? TestValue = null;

        public bool GenreIDExists(int genreID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public ICollection<Genre> GetAllGenres()
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            ICollection<Genre> genres = null;
            if (TestValue.Value)
            {
                genres.Add(testGenre);
            }
            return genres;
        }

        public Genre GetGenre(int genreID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            Genre genre = null;
            if (TestValue.Value)
            {
                genre = testGenre;
            }
            return genre;
        }
    }
}
