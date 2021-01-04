using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMates_Backend.Entities
{
    public class Genre
    {
        public Genre()
        {
            this.Movies = new HashSet<Movie>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        [Required]
        public string GenreName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
