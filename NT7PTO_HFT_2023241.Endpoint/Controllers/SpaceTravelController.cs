using Microsoft.AspNetCore.Mvc;
using NT7PTO_HFT_2023241.Logic;
using NT7PTO_HFT_2023241.Models;
using System.Collections.Generic;

namespace NT7PTO_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpaceTravelController: ControllerBase
    {
        ISpaceTravelLogic logic;

        public SpaceTravelController(ISpaceTravelLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<SpaceTravel> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public SpaceTravel Read(string id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] SpaceTravel value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] SpaceTravel value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            this.logic.Delete(id);
        }
    }
}
