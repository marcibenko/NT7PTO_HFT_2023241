using System;
using System.Linq;
using NT7PTO_HFT_2023241.Models;

namespace NT7PTO_HFT_2023241.Repository
{
    public class CaptainRepository : Repository<Captain>, IRepository<Captain>
    {
        public CaptainRepository(SpaceTravelsDbContext ctx): base(ctx)
        {

        }

        public override Captain Read(string id)
        {
            return ctx.Captains.FirstOrDefault(c => c.captainId == id);
        }

        public override void Update(Captain item)
        {
            var old = Read(item.captainId);
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

