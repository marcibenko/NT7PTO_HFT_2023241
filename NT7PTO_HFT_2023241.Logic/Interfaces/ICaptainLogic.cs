using NT7PTO_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace NT7PTO_HFT_2023241.Logic
{
    public interface ICaptainLogic
    {
        void Create(Captain item);
        void Delete(string id);
        Captain Read(string id);
        IQueryable<Captain> ReadAll();
        void Update(Captain item);

        //non crud

        ////returns a list of captains, sorted ascending by their number of travels 
        //IEnumerable<Captain> MostTravels();

        //returns a list of captains, sorted descending by their level of threat
        //formula is (spaceshipType * size) summed for all their ships
        IEnumerable<Captain> MostDangerousCaptains();

        //returns a list of captains, sorted descending by their max ship size
        IEnumerable<Captain> BiggestShip();
    }
}