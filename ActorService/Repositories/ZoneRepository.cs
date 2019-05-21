using System.Collections.Generic;
using System.Linq;
using ActorService.Model;

namespace ActorService.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly ModelContext _modelContext;

        public ZoneRepository(ModelContext modelContext)
        {
            _modelContext = modelContext;
        }

        public void AddZone(Zone zone)
        {
            _modelContext.Zones.Add(zone);
        }

        public IEnumerable<Zone> GetZones(int page = 0, int pageSize = 10)
        {
            return _modelContext.Zones.Skip(page * pageSize).Take(pageSize).ToList();
        }
        
        public int Count()
        {
            return _modelContext.Zones.Count();
        }

        public bool Save()
        {
            return (_modelContext.SaveChanges() >= 0);
        }
    }
}