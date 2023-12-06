using Microsoft.AspNetCore.Mvc;
using NT7PTO_HFT_2023241.Logic;
using NT7PTO_HFT_2023241.Models;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using static NT7PTO_HFT_2023241.Logic.SpaceshipLogic;
using static NT7PTO_HFT_2023241.Logic.SpaceTravelLogic;

namespace NT7PTO_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NonCrudController : ControllerBase
    {
        ICaptainLogic captainLogic;
        ISpaceshipLogic SpaceshipLogic;
        ISpaceTravelLogic SpaceTravelLogic;

        public NonCrudController(ICaptainLogic captainLogic, ISpaceshipLogic SpaceshipLogic, ISpaceTravelLogic SpaceTravelLogic)
        {
            this.captainLogic = captainLogic;
            this.SpaceshipLogic = SpaceshipLogic;
            this.SpaceTravelLogic = SpaceTravelLogic;
        }

        //st
        [HttpGet]
        public IEnumerable<CaptainsTravels> MostTravels()
        {
            return this.SpaceTravelLogic.MostTravels();
        }

        //cap
        [HttpGet]
        public IEnumerable<Captain> MostDangerousCaptains()
        {
            return this.captainLogic.MostDangerousCaptains();
        }

        [HttpGet]
        public IEnumerable<Captain> BiggestShip()
        {
            return this.captainLogic.BiggestShip();
        }
        //ship
        [HttpGet]
        public IEnumerable<Spaceship> UnusedShips()
        {
            return this.SpaceshipLogic.UnusedShips();
        }

        [HttpGet]
        public IEnumerable<CapAndShips> CaptainsAndShips()
        {
            return this.SpaceshipLogic.CaptainsAndShips();
        }
    }
}
