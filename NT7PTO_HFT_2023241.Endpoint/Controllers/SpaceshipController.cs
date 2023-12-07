using Microsoft.AspNetCore.Mvc;
using NT7PTO_HFT_2023241.Logic;
using NT7PTO_HFT_2023241.Models;
using System.Collections.Generic;

namespace NT7PTO_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpaceshipController: ControllerBase
    {
        ISpaceshipLogic logic;

        public SpaceshipController(ISpaceshipLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Spaceship> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Spaceship Read(string id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Spaceship value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Spaceship value)
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
