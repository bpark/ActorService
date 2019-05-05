using System.Collections.Generic;
using ActorService.AppServices;
using ActorService.Model;
using ActorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ActorService.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {

        private readonly IActorRepository _actorRepository;
        private readonly IActorFactory _actorFactory;
        private readonly GetActorListQueryHandler _listQueryHandler;

        public ActorsController(GetActorListQueryHandler listQueryHandler, IActorRepository actorRepository, IActorFactory actorFactory)
        {
            _listQueryHandler = listQueryHandler;
            _actorRepository = actorRepository;
            _actorFactory = actorFactory;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Actor>> Get(int page = 0, int pageSize = 10)
        {
            var resultListDto = _listQueryHandler.Handle(new GetActorListQuery(page, pageSize));
            return Ok(resultListDto);
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