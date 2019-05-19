using System.Collections.Generic;
using ActorService.AppServices;
using ActorService.Model;
using Microsoft.AspNetCore.Mvc;

namespace ActorService.Controllers
{
    [Route("api/zones")]
    [ApiController]
    public class ZoneController : ControllerBase
    {

        private readonly CreateZoneCommandHandler _createZoneCommandHandler;

        public ZoneController(CreateZoneCommandHandler zoneCommandHandler)
        {
            _createZoneCommandHandler = zoneCommandHandler;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Actor>> Get(int page = 0, int pageSize = 10)
        {
            /*
            var resultListDto = _listQueryHandler.Handle(new GetActorListQuery(page, pageSize));
            return Ok(resultListDto);
            */
            return null;
        }
        
        [HttpPost]
        public void Post([FromBody] CreateZoneDto createZoneDto)
        {
            var zoneCommand = new CreateZoneCommand()
            {
                Level = createZoneDto.Level,
                Name = createZoneDto.Name,
                ZoneType = ZoneType.FromName(createZoneDto.ZoneType)
            };
            _createZoneCommandHandler.Handle(zoneCommand);
        }
        
    }
}