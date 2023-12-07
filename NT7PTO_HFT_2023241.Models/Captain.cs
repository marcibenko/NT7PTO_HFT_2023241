using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NT7PTO_HFT_2023241.Models
{
    public class Captain
    {
        //kapitanyID ,nev, eletkor,  szarmazasi hely
        
        
        [Key]
        [StringLength(6)]
        public  string captainId { get; set; } //pk

        [Required]
        public string name { get; set; }

        
        public int age { get; set; }

        public string birthPlace { get; set; }

        [NotMapped]
        public virtual ICollection<Spaceship> Spaceships { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<SpaceTravel> SpaceTravels { get; set; }

        public Captain()
        {
            this.Spaceships = new HashSet<Spaceship>();
            this.SpaceTravels = new HashSet<SpaceTravel>();
        }

        public Captain(string captainId, string name, int age, string birthPlace)
        {
            this.captainId = captainId;
            this.name = name;
            this.age = age;
            this.birthPlace = birthPlace;
            this.Spaceships = new HashSet<Spaceship>();
            this.SpaceTravels = new HashSet<SpaceTravel>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Captain otherCaptain = (Captain)obj;

            return captainId == otherCaptain.captainId ;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(captainId, name, age, birthPlace);
        }
    }
}
