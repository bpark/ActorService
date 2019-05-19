using ActorService.Model;
using ActorService.Repositories;
using CSharpFunctionalExtensions;

namespace ActorService.AppServices
{
    public sealed class CreateZoneCommand : ICommand
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public ZoneType ZoneType { get; set; }
    }
    
    public sealed class CreateZoneCommandHandler : ICommandHandler<CreateZoneCommand>
    {
        private readonly IZoneRepository _zoneRepository;

        public CreateZoneCommandHandler(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public Result Handle(CreateZoneCommand command)
        {
            var zone = new Zone(command.Name, command.Level, command.ZoneType);
            
            _zoneRepository.AddZone(zone);
            _zoneRepository.Save();
            return Result.Ok();
        }
    }
}