using ActorService.Model;

namespace ActorService.Repositories
{
    public interface IZoneRepository
    {
        void AddZone(Zone zone);
        
        bool Save();
    }
}