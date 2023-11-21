using NT7PTO_HFT_2023241.Models;
using NT7PTO_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NT7PTO_HFT_2023241.Logic
{
    public class SpaceshipLogic : ISpaceshipLogic
    {
        IRepository<Spaceship> repo;

        public SpaceshipLogic(IRepository<Spaceship> repo)
        {
            this.repo = repo;
        }
        public void Create(Spaceship item)
        {
            this.repo.Create(item);
        }

        public void Delete(string id)
        {
            this.repo.Delete(id);
        }

        public Spaceship Read(string id)
        {
            var ship = this.repo.Read(id);
            if (ship == null)
            {
                throw new ArgumentException("Spaceship not exists");
            }
            return ship;
        }

        public IQueryable<Spaceship> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Spaceship item)
        {
            this.repo.Update(item);
        }

        //non cruds
        public IEnumerable<Spaceship> UnusedShips()
        {
            return this.repo
                    .ReadAll()
                    .Where(t => t.SpaceTravels.Count() == 0);
        }

        public IEnumerable<CapAndShips> CaptainsAndShips()
        {
            return this.repo
                            .ReadAll()
                            .GroupBy(t => t.captain)
                            .OrderByDescending(t => t.Count())
                            .Select(t => new CapAndShips { Captain = t.Key, Spaceships = t.ToList()});
        }


        public class CapAndShips
        {
            public Captain Captain { get; set; }
            public List<Spaceship> Spaceships { get; set; }

        }
    }
}