using System.Collections.Generic;
using System.Linq;
using ActorService.Model;

namespace ActorService.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly ModelContext _modelContext;

        public ActorRepository(ModelContext modelContext)
        {
            _modelContext = modelContext;
        }

        public void AddActor(Actor actor)
        {
            _modelContext.Actors.Add(actor);
        }

        public Actor GetActor(int id)
        {
            return _modelContext.Actors.FirstOrDefault(a => a.Id == id);
        }

        public IReadOnlyList<Actor> GetActors(int page = 0, int pageSize = 10)
        {
            return _modelContext.Actors.Skip(page * pageSize).Take(pageSize).ToList();
        }

        public int Count()
        {
            return _modelContext.Actors.Count();
        }

        public bool Save()
        {
            return (_modelContext.SaveChanges() >= 0);
        }
    }
}