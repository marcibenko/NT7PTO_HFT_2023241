using NT7PTO_HFT_2023241.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NT7PTO_HFT_2023241.Logic
{
    public interface ISpaceshipLogic
    {
        void Create(Spaceship item);
        void Delete(string id);
        Spaceship Read(string id);
        IQueryable<Spaceship> ReadAll();
        void Update(Spaceship item);

        //non cruds

        //returns a list of captains and their number of ships sorted descending by their number of ships
        IEnumerable<SpaceshipLogic.CapAndShips> CaptainsAndShips();


        //returns a list of ships, which havent been used
        IEnumerable<Spaceship> UnusedShips();





        
    }
}