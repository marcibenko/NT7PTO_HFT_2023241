using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace NT7PTO_HFT_2023241.Models
{
    public class Spaceship
    {
        public enum SpaceshipType
        {
            Fighter = 150,
            Cruiser = 80,
            Destroyer = 200,
            Battleship = 170,
            Transport = 100
        }

        [Key]
        [StringLength(6)]
        public string spaceshipId { get; set; } //pk

        [StringLength(40)]
        [Required]
        public string shipName { get; set; }

        public SpaceshipType type { get; set; }

        public int size { get; set; } //carry capacity of a ship

        [ForeignKey("Captain")]
        public string captainId { get; set; } //fk to captain

        [JsonIgnore]
        [NotMapped]
        public virtual Captain captain { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<SpaceTravel> SpaceTravels { get; set; }

        public Spaceship()
        {
            this.SpaceTravels = new HashSet<SpaceTravel>();
        }

        public Spaceship(string spaceshipId, string shipName, SpaceshipType type, int size, string captainId)
        {
            this.spaceshipId = spaceshipId;
            this.shipName = shipName;
            this.type = type;
            this.size = size;
            this.captainId = captainId;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Spaceship otherShip = (Spaceship)obj;

            return spaceshipId == otherShip.spaceshipId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(spaceshipId,shipName,size);
        }
    }
}
