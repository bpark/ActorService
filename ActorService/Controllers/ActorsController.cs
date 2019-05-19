using System.Collections.Generic;
using ActorService.AppServices;
using ActorService.Model;
using Microsoft.AspNetCore.Mvc;

namespace ActorService.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {

        private readonly GetActorListQueryHandler _listQueryHandler;
        private readonly GetActorQueryHandler _queryHandler;
        private readonly CreateActorCommandHandler _commandHandler;

        public ActorsController(GetActorListQueryHandler listQueryHandler,
                                GetActorQueryHandler queryHandler,
                                CreateActorCommandHandler commandHandler)
        {
            _listQueryHandler = listQueryHandler;
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Actor>> Get(int page = 0, int pageSize = 10)
        {
            var resultListDto = _listQueryHandler.Handle(new GetActorListQuery(page, pageSize));
            return Ok(resultListDto);
        }

        [HttpGet("{id}")]
        public ActionResult<Actor> Get(int id)
        {
            var actorDto = _queryHandler.Handle(new GetActorQuery(id));
            return Ok(actorDto);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            _commandHandler.Handle(new CreateActorCommand());
        }

    }
}