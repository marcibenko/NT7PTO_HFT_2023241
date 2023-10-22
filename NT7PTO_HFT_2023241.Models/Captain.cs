using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT7PTO_HFT_2023241.Models
{
    public class Captain
    {
        //kapitanyID ,nev, eletkor,  szarmazasi hely
        
        
        [Key]
        string captainID { get; set; }

        string name { get; set; }

        //nem lehet negativ
        int age { get; set; }

        string birthPlace { get; set; }



    }
}
