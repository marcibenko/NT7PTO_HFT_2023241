using System;
using System.Linq;
using NT7PTO_HFT_2023241.Models;

namespace NT7PTO_HFT_2023241.Repository
{
    public class SpaceTravelRepository : Repository<SpaceTravel>, IRepository<SpaceTravel>
    {
        public SpaceTravelRepository(SpaceTravelsDbContext ctx): base(ctx)
        {

        }

        public override SpaceTravel Read(string id)
        {
            return ctx.SpaceTravels.FirstOrDefault(s => s.travelId == id);
        }

        public override void Update(SpaceTravel item)
        {
            var old = Read(item.travelId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
