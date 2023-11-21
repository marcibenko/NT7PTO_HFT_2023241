using NT7PTO_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace NT7PTO_HFT_2023241.Logic
{
    public interface ISpaceTravelLogic
    {
        void Create(SpaceTravel item);
        void Delete(string id);
        SpaceTravel Read(string id);
        IQueryable<SpaceTravel> ReadAll();
        void Update(SpaceTravel item);

        //non cruds

        //returns a list of captains and their number of travels, sorted ascending by travels
        IEnumerable<SpaceTravelLogic.CaptainsTravels> MostTravels();

    }
}