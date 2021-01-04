using MovieMates_Backend.Entities;
using MovieMates_Backend.Interfaces;
using System.Collections.Generic;

namespace MovieMates_Backend.Containers
{
    public class MovieContainer : IMovieDAL
    {
        readonly IMovieDAL iMovie;

        public MovieContainer(IMovieDAL iMovie)
        {
            this.iMovie = iMovie;
        }

        public bool AddMovie(Movie movie)
        {
            return iMovie.AddMovie(movie);
        }

        public ICollection<Movie> GetAllMovies()
        {
            return iMovie.GetAllMovies();
        }

        public Movie GetMovie(int movieID)
        {
            return iMovie.GetMovie(movieID);
        }

        public ICollection<Movie> GetMoviesByGenre(Genre genre)
        {
            return iMovie.GetMoviesByGenre(genre);
        }

        public bool MovieIDExists(int movieID)
        {
            return iMovie.MovieIDExists(movieID);
        }
    }
}
