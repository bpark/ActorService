using ActorService.Model;
using Xunit;

namespace ActorService.Tests.Model
{
    public class ZonesTest
    {
        private readonly Zone _zone;

        public ZonesTest()
        {
            _zone = new Zone();
        }

        
        [Fact]
        public void TestAddActor()
        {
            var actor = new Actor
            {
                Id = 1
            };
            
            _zone.AddActor(actor);

            Assert.Contains(_zone.Actors, a => a.Id == actor.Id);
        }
        
        [Fact]
        public void TestRemoveActor()
        {
            var actor = new Actor
            {
                Id = 2
            };
            
            _zone.AddActor(actor);
            _zone.RemoveActor(actor);

            Assert.DoesNotContain(_zone.Actors, a => a.Id == actor.Id);
        }
    }
}