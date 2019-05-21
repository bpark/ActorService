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

        private readonly GetZoneListQueryHandler _getZoneListQueryHandler;

        public ZoneController(CreateZoneCommandHandler zoneCommandHandler, 
                              GetZoneListQueryHandler getZoneListQueryHandler)
        {
            _createZoneCommandHandler = zoneCommandHandler;
            _getZoneListQueryHandler = getZoneListQueryHandler;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Actor>> Get(int page = 0, int pageSize = 10)
        {

            var resultListDto = _getZoneListQueryHandler.Handle(new GetZoneListQuery(page, pageSize));
            return Ok(resultListDto);
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