using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NT7PTO_HFT_2023241.Models
{
    public class Spaceship
    {
        public enum SpaceshipType
        {
            Fighter,
            Cruiser,
            Destroyer,
            Battleship,
            Transport
        }
        //hajoId, hajoNeve, tipusa, merete(foben kifejezve), kapitanya(csak egy), 
        //valtozokat angolul!!!!!!!!!!!!!!

        [Key]
        [StringLength(6)]
        public string spaceshipId { get; set; } 

        [StringLength(40)]
        [Required]
        public string shipName { get; set; }

        public SpaceshipType type { get; set; }

        public int size { get; set; } //carry capacity of a ship

        [ForeignKey("Captain")]
        public string captainId { get; set; }

        public virtual Captain captain { get; set; }
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
    }
}
