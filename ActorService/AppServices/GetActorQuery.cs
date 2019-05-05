using System.Linq;
using ActorService.Controllers;
using ActorService.Model;
using ActorService.Repositories;

namespace ActorService.AppServices
{
    public sealed class GetActorQuery : IQuery<ActorDto>
    {
        public int Id { get; }
        
        public GetActorQuery(int id)
        {
            Id = id;
        }

    }
    
    public sealed class GetActorQueryHandler : IQueryHandler<GetActorQuery, ActorDto>
    {

        private readonly IActorRepository _actorRepository;

        public GetActorQueryHandler(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public ActorDto Handle(GetActorQuery query)
        {
            var actor = _actorRepository.GetActor(query.Id);

            return new ActorDto
            {
                Name = actor.Name,
                Balance = actor.Balance.Name,
                Experience = actor.Experience,
                CurrentHealth = actor.CurrentHealth,
                Health = actor.Health,
                Power = actor.Power,
                Speed = actor.Speed,
                Quality = actor.Quality.Name
            };
        }
    }
}