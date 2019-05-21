using System.Collections.Generic;
using ActorService.Model;

namespace ActorService.Repositories
{
    public interface IZoneRepository
    {
        void AddZone(Zone zone);
        
        IEnumerable<Zone> GetZones(int page = 0, int pageSize = 10);
        
        int Count();
        
        bool Save();
    }
}