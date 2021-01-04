using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMates_Backend.Entities
{
    public class Group
    {
        public Group()
        {
            this.Members = new HashSet<User>();
            TimeCreated = DateTime.Now;
            JoinID = GenerateCode();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(6)]
        [Index(IsUnique = true)]
        public string JoinID { get; set; }

        public DateTime TimeCreated { get; set; }
        public virtual ICollection<User> Members { get; set; }



        private string GenerateCode()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }

    }
}
