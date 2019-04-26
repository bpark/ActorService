using ActorService.Model;

namespace ActorService.Repositories
{
    public interface IActorRepository
    {
        void AddActor(Actor actor);

        Actor GetActor(int id);

        bool Save();
    }
}