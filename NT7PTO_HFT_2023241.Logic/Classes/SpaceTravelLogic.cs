﻿using NT7PTO_HFT_2023241.Models;
using NT7PTO_HFT_2023241.Repository;
using System;
using System.Linq;
using System.Collections.Generic;

namespace NT7PTO_HFT_2023241.Logic
{
    public class SpaceTravelLogic : ISpaceTravelLogic
    {
        IRepository<SpaceTravel> repo;

        public SpaceTravelLogic(IRepository<SpaceTravel> repo)
        {
            this.repo = repo;
        }
        public void Create(SpaceTravel item)
        {
            this.repo.Create(item);
        }

        public void Delete(string id)
        {
            this.repo.Delete(id);
        }

        public SpaceTravel Read(string id)
        {
            var travel = this.repo.Read(id);
            if(travel == null)
            {
                throw new ArgumentException("Spacetravel not exists");
            }
            return travel;
        }

        public IQueryable<SpaceTravel> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(SpaceTravel item)
        {
            this.repo.Update(item);
        }

        //non cruds
        IEnumerable<CaptainsTravels> MostTravels()
        {
            return this.repo
                        .ReadAll()
                        .GroupBy(t => t.captain)
                        .Select(t => new CaptainsTravels
                        {
                            Captain = t.Key,
                            NumberOfTravels = t.Count()
                        })
                        .OrderBy(t => t.NumberOfTravels);
        }

        public class CaptainsTravels
        {
            public Captain Captain { get; set; }
            public int NumberOfTravels { get; set; }
        }
    }
}