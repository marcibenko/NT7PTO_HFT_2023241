using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NT7PTO_HFT_2023241.Models
{
    public class Spaceship
    {
        //hajoId, hajoNeve, tipusa, merete(foben kifejezve), kapitanya(csak egy), 
        //valtozokat angolul!!!!!!!!!!!!!!

        [Key]
        string spaceshipId;

        string shipName;

        //ez lehet enum
        string type;

        int size;
        [ForeignKey("Captain")]
        Captain captain;
    }
}
