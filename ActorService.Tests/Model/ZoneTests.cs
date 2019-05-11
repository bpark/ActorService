using ActorService.Model;
using Xunit;

namespace ActorService.Tests.Model
{
    public class ZonesTest
    {
        private readonly Zone _zone;

        public ZonesTest()
        {
            _zone = new Zone(name: "Test", level: 1, ZoneType.Dungeon);
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
        
        [Fact]
        public void AddInhabitantsTest()
        {
            var inhabitant1 = new Actor
            {
                Id = 1
            };

            var inhabitant2 = new Actor
            {
                Id = 2
            };

            var inhabitants = new Actor[] { inhabitant1, inhabitant2 };

            _zone.AddInhabitants(inhabitants);

            Assert.Equal(inhabitants, _zone.Inhabitants);
        }

        [Fact]
        public void TakeInhabitantsTest()
        {
            var inhabitant1 = new Actor
            {
                Id = 1
            };

            var inhabitant2 = new Actor
            {
                Id = 2
            };

            var inhabitants = new Actor[] { inhabitant1, inhabitant2 };

            _zone.AddInhabitants(inhabitants);
            var taken = _zone.TakeInhabitants(1);

            Assert.Contains(_zone.Inhabitants, a => a.Id == 2);
            Assert.Equal(new Actor[] { inhabitant1 }, taken);
            Assert.Equal(new Actor[] { inhabitant2 }, _zone.Inhabitants);
        }

    }
}