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

        public bool Save()
        {
            return (_modelContext.SaveChanges() >= 0);
        }
    }
}