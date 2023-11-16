using NT7PTO_HFT_2023241.Repository;
using System;
using System.Linq;

namespace NT7PTO_HFT_2023241.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected SpaceTravelsDbContext ctx;

        public Repository(SpaceTravelsDbContext ctx)
        {
            this.ctx = ctx;
        }
        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public void Delete(string id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }

        public abstract T Read(string id);
        public abstract void Update(T item);
        
    }
}

