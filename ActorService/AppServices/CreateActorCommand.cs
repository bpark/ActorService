using ActorService.Model;
using ActorService.Repositories;
using CSharpFunctionalExtensions;

namespace ActorService.AppServices
{
    public sealed class CreateActorCommand : ICommand
    {
    }
    
    public sealed class CreateActorCommandHandler : ICommandHandler<CreateActorCommand>
    {

        private readonly IActorRepository _actorRepository;
        private readonly IActorFactory _actorFactory;

        public CreateActorCommandHandler(IActorRepository actorRepository, IActorFactory actorFactory)
        {
            _actorRepository = actorRepository;
            _actorFactory = actorFactory;
        }

        public Result Handle(CreateActorCommand command)
        {
            var actor = _actorFactory.CreateRandomActor();
            _actorRepository.AddActor(actor);
            _actorRepository.Save();
            return Result.Ok();
        }
    }
}