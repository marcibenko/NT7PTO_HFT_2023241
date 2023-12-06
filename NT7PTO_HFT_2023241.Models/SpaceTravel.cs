using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NT7PTO_HFT_2023241.Models
{
    public class SpaceTravel
    {
           
        [Key]
        [StringLength(10)]
        public string travelId { get; set; } //pk

        [ForeignKey("Captain")]
        public string captainId { get; set; } //fk to captain

        [ForeignKey("Spaceship")]
        public string spaceshipId { get; set; } //fk to ship

        [StringLength(40)]
        public string travelFrom { get; set; }

        [StringLength(40)]
        public string travelTo { get; set; }

        public int travelStartYear { get; set; }

        public virtual Captain captain { get; set; }
        public virtual Spaceship spaceship { get; set; }

        public SpaceTravel()
        {

        }

        public SpaceTravel(string travelId, string captainId, string spaceshipId, string travelFrom, string travelTo, int travelStartYear)
        {
            this.travelId = travelId;
            this.captainId = captainId;
            this.spaceshipId = spaceshipId;
            this.travelFrom = travelFrom;
            this.travelTo = travelTo;
            this.travelStartYear = travelStartYear;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            SpaceTravel otherTravel = (SpaceTravel)obj;

            return travelId == otherTravel.travelId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(travelId,travelFrom,travelTo);
        }
    }
}
