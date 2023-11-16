using System;
using System.Linq;
using NT7PTO_HFT_2023241.Models;

namespace NT7PTO_HFT_2023241.Repository
{
    public class SpaceshipRepository: Repository<Spaceship>, IRepository<Spaceship>
    {
        public SpaceshipRepository(SpaceTravelsDbContext ctx): base(ctx)
        {

        }

        public override Spaceship Read(string id)
        {
            return ctx.Spaceships.FirstOrDefault(s => s.spaceshipId == id);
        }

        public override void Update(Spaceship item)
        {
            var old = Read(item.spaceshipId);
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

