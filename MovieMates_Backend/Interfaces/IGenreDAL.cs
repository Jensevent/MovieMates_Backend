using MovieMates_Backend.Entities;
using System.Collections.Generic;

namespace MovieMates_Backend.Interfaces
{
    public interface IGenreDAL
    {
        public ICollection<Genre> GetAllGenres();

        public Genre GetGenre(int genreID);

        public bool GenreIDExists(int genreID);
    }
}
