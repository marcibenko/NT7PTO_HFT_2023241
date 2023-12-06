using Microsoft.AspNetCore.Mvc;
using NT7PTO_HFT_2023241.Logic;
using NT7PTO_HFT_2023241.Models;
using System.Collections;
using System.Collections.Generic;

namespace NT7PTO_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    public class CaptainController: ControllerBase
    {
        ICaptainLogic logic;

        public CaptainController(ICaptainLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Captain> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Captain Read(string id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Captain value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Captain value)
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
