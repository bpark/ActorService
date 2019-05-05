using System.Linq;
using ActorService.Controllers;
using ActorService.Model;
using ActorService.Repositories;

namespace ActorService.AppServices
{
    public sealed class GetActorListQuery : IQuery<ResultListDto<ActorDto>>
    {
        public int Page { get; }
        public int PageSize { get; }
        
        public GetActorListQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

    }
    
    public sealed class GetActorListQueryHandler : IQueryHandler<GetActorListQuery, ResultListDto<ActorDto>>
    {

        private readonly IActorRepository _actorRepository;

        public GetActorListQueryHandler(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public ResultListDto<ActorDto> Handle(GetActorListQuery query)
        {
            var actors = _actorRepository.GetActors(query.Page, query.PageSize);
            var actorDtos = actors.Select(a => new ActorDto
            {
                Name = a.Name,
                Balance = a.Balance.Name,
                Experience = a.Experience,
                CurrentHealth = a.CurrentHealth,
                Health = a.Health,
                Power = a.Power,
                Speed = a.Speed,
                Quality = a.Quality.Name
            }).ToList();
            return  new ResultListDto<ActorDto>(_actorRepository.Count(), actorDtos);
        }
    }
}