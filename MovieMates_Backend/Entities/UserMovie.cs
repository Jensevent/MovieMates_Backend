using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MovieMates_Backend.Entities
{
    public class UserMovie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }
        public bool? Watched { get; set; }
        public double? UserRating { get; set; }


        //FK User
        [Required]
        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        //FK Movie
        [Required]
        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
