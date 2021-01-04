using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieMates_Backend.Entities
{
    public enum Platforms
    {
        Netflix, DisneyPlus
    }


    public class Movie
    {
        public Movie()
        {
            this.MovieGenres = new HashSet<Genre>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public double IMDb { get; set; }

        [Required]
        public string RunTime { get; set; }
        public DateTime ReleaseYear { get; set; } = new DateTime();

        [Required]
        public Platforms Platform { get; set; }

        public int Amount { get; set; }



        public virtual ICollection<Genre> MovieGenres { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserMovie> UserMovies { get; set; }
    }
}
