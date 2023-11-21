using Microsoft.EntityFrameworkCore;
using NT7PTO_HFT_2023241.Models;
using NT7PTO_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace NT7PTO_HFT_2023241.Logic
{
    public class CaptainLogic : ICaptainLogic
    {
        IRepository<Captain> repo;

        public CaptainLogic(IRepository<Captain> repo)
        {
            this.repo = repo;
        }
        public void Create(Captain item)
        {
            this.repo.Create(item);          
        }

        public void Delete(string id)
        {
            this.repo.Delete(id);
        }

        public Captain Read(string id)
        {
            var cap = this.repo.Read(id);
            if(cap == null)
            {
                throw new ArgumentException("Captain not exists");
            }
            return cap;
        }

        public IQueryable<Captain> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Captain item)
        {
            this.repo.Update(item);
        }

        //non cruds
        public IEnumerable<Captain> MostDangerousCaptains()
        {
            return this.repo
                           .ReadAll()
                           .Include(t => t.Spaceships)
                           .Select(t => new
                            {
                             key = t,
                             rating = t.Spaceships.Sum(s => s.size * (int)s.type)
                            })
                           .OrderByDescending(t => t.rating)
                           .Select(t => t.key);
        }

        public IEnumerable<Captain> BiggestShip()
        {
            return this.repo
                        .ReadAll()
                        .Include(t => t.Spaceships)
                        .OrderByDescending(t => t.Spaceships.Count());
        }
    }
}