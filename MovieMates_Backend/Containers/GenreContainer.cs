using MovieMates_Backend.Entities;
using MovieMates_Backend.Interfaces;
using System.Collections.Generic;

namespace MovieMates_Backend.Containers
{
    public class GenreContainer : IGenreDAL
    {
        readonly IGenreDAL iGenre;

        public GenreContainer(IGenreDAL iGenre)
        {
            this.iGenre = iGenre;
        }

        public bool GenreIDExists(int genreID)
        {
            return iGenre.GenreIDExists(genreID);
        }

        public ICollection<Genre> GetAllGenres()
        {
            return iGenre.GetAllGenres();
        }

        public Genre GetGenre(int genreID)
        {
            return iGenre.GetGenre(genreID);
        }
    }
}
