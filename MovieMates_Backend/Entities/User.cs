using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMates_Backend.Entities
{
    public class User
    {
        public User()
        {
            this.Groups = new HashSet<Group>();
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required]
        public string Username { get; set; }

        //[JsonIgnore]
        [Required]
        public string PasswordHash { get; set; }

        //[JsonIgnore]
        [Required]
        public string PasswordSalt { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        [JsonIgnore]
        public virtual ICollection<UserMovie> UserMovies { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

    }
}
