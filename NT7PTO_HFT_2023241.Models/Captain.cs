﻿using System;
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
        [StringLength(6)]
        public  string captainId { get; set; }

        [Required]
        public string name { get; set; }

        
        public int age { get; set; }

        public string birthPlace { get; set; }

        public virtual ICollection<Spaceship> Spaceships { get; set; }
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
        }
    }
}
