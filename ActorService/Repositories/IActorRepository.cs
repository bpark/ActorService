using System.Collections.Generic;
using ActorService.Model;

namespace ActorService.Repositories
{
    public interface IActorRepository
    {
        void AddActor(Actor actor);

        Actor GetActor(int id);
        
        IReadOnlyList<Actor> GetActors(int page = 0, int pageSize = 10);

        int Count();

        bool Save();
    }
}