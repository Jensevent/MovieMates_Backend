using MovieMates_Backend.Entities;
using System.Collections.Generic;

namespace MovieMates_Backend.Interfaces
{
    public interface IMovieDAL
    {
        public ICollection<Movie> GetAllMovies();
        public Movie GetMovie(int movieID);
        public ICollection<Movie> GetMoviesByGenre(Genre genre);
        public bool MovieIDExists(int movieID);
        public bool AddMovie(Movie movie);
    }
}
