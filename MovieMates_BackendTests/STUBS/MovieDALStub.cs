using MovieMates_Backend.Entities;
using System;
using System.Collections.Generic;

namespace MovieMates_BackendTests.STUBS
{
    class MovieDALStub : MovieMates_Backend.Interfaces.IMovieDAL
    {
        readonly Movie testMovie = new Movie { Title = "Avengers: Endgame", Description = "Lorem Ipsum", Platform = Platforms.DisneyPlus, IMDb = 8.4, ReleaseYear = new DateTime(2019, 4, 24), RunTime = "3.01", MovieGenres = new List<Genre> { new Genre { GenreName = "Action" } } };
        public bool? TestValue = null;

        public ICollection<Movie> GetAllMovies()
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            ICollection<Movie> movies = null;
            if (TestValue.Value)
            {
                movies.Add(testMovie);
            }
            return movies;
        }

        public Movie GetMovie(int movieID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            Movie movie = null;
            if (TestValue.Value)
            {
                movie = testMovie;
            }
            return movie;
        }

        public ICollection<Movie> GetMoviesByGenre(Genre genre)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            ICollection<Movie> movies = null;
            if (TestValue.Value)
            {
                movies.Add(testMovie);
            }
            return movies;
        }

        public bool MovieIDExists(int movieID)
        {
            if (TestValue == null)
            {
                throw new NullReferenceException("Invalid use of stub code. First set field TestValue");
            }

            return TestValue.Value;
        }

        public bool AddMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
