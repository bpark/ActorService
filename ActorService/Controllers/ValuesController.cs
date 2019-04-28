using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActorService.Model;
using ActorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ActorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IActorRepository _actorRepository;
        private readonly IActorFactory _actorFactory;

        public ValuesController(IActorRepository actorRepository, IActorFactory actorFactory)
        {
            _actorRepository = actorRepository;
            _actorFactory = actorFactory;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Actor>> Get()
        {
            return Ok(_actorRepository.GetActors());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Actor> Get(int id)
        {
            return _actorRepository.GetActor(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var actor = _actorFactory.CreateRandomActor();
            
            _actorRepository.AddActor(actor);
            _actorRepository.Save();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}