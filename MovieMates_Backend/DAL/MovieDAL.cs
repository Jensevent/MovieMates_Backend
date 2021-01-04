using Microsoft.Extensions.Configuration;
using MovieMates_Backend.Entities;
using MovieMates_Backend.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MovieMates_Backend.DAL
{
    public class MovieDAL : IMovieDAL
    {
        readonly DatabaseContext db;

        public MovieDAL(IConfiguration config, string connName)
        {
            db = new DatabaseContext(config, connName);
        }


        public bool AddMovie(Movie movie)
        {
            if (db.Movies.Where(m => m.Title == movie.Title).FirstOrDefault() != null)
            {
                return false;
            }


            db.Movies.Add(movie);
            db.SaveChanges();
            return true;
        }

        public bool MovieIDExists(int movieID)
        {
            if (db.Movies.Where(m => m.ID == movieID).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }

        ICollection<Movie> IMovieDAL.GetAllMovies()
        {
            return db.Movies.ToList();
        }

        Movie IMovieDAL.GetMovie(int movieID)
        {
            return db.Movies.Where(m => m.ID == movieID).FirstOrDefault();
        }

        ICollection<Movie> IMovieDAL.GetMoviesByGenre(Genre genre)
        {
            ICollection<Movie> movies = db.Movies.Where(m => m.MovieGenres.Count != 0 && m.MovieGenres.Any(g => g.ID == genre.ID)).ToList();

            foreach (Movie movie in movies)
            {
                foreach (Genre thisGenre in movie.MovieGenres)
                {
                    thisGenre.Movies.Clear();
                }
            }
            return movies;
        }
    }
}
