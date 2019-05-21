using System.Linq;
using ActorService.Controllers;
using ActorService.Model;
using ActorService.Repositories;

namespace ActorService.AppServices
{
    public sealed class GetZoneListQuery : IQuery<ResultListDto<ZoneDto>>
    {
        public int Page { get; }
        public int PageSize { get; }
        
        public GetZoneListQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

    }
    
    public sealed class GetZoneListQueryHandler : IQueryHandler<GetZoneListQuery, ResultListDto<ZoneDto>>
    {

        private readonly IZoneRepository _zoneRepository;

        public GetZoneListQueryHandler(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public ResultListDto<ZoneDto> Handle(GetZoneListQuery query)
        {
            var zones = _zoneRepository.GetZones(query.Page, query.PageSize);
            var zoneDtos = zones.Select(a => new ZoneDto
            {
                Id = a.Id,
                Name = a.Name,
                Level = a.Level,
                ZoneType = a.ZoneType.Name
            }).ToList();
            return  new ResultListDto<ZoneDto>(_zoneRepository.Count(), zoneDtos);
        }
    }
}