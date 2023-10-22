using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT7PTO_HFT_2023241.Models
{
    class SpaceTravel
    {
           
        [Key]
        string travelId;

        [ForeignKey("Captain")]
        string CaptainId;

        [ForeignKey("Spaceship")]
        string spaceshipId;


        string travelFrom;

        string travelTo;

        DateTime travelStartDate;



    }
}
