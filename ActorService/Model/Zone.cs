using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ActorService.Model
{
   
    public class ZoneType {

        public static readonly ZoneType Town = new ZoneType("Town");
        public static readonly ZoneType Dungeon = new ZoneType("Dungeon");
            
        public string Name { get; }

        public static IEnumerable<ZoneType> Values => new ZoneType[] { Town, Dungeon };

        private ZoneType(string name) {
            Name = name;
        }

        public static ZoneType FromName(string name)
        {
            foreach (var value in Values)
            {
                if (value.Name == name)
                {
                    return value;
                }
            }
            throw new ArgumentException("Invalid name: " + name);
        }

    }
    
    public class Zone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public ZoneType ZoneType { get; set; }

        public ReadOnlyCollection<Actor> Actors => _actors.AsReadOnly();
        public ReadOnlyCollection<Actor> Inhabitants => _inhabitants.AsReadOnly();

        private readonly List<Actor> _actors = new List<Actor>();
        private readonly List<Actor> _inhabitants = new List<Actor>();
        
        public void AddActor(Actor actor)
        {
            _actors.Add(actor);    
        }

        public void RemoveActor(Actor actor)
        {
            _actors.Remove(actor);
        }

        public IEnumerable<Actor> TakeInhabitants(int amount)
        {
            var taken = _inhabitants.Take(amount);
            _inhabitants.RemoveAll(a => taken.Contains(a));
            return taken;
        }

        public void AddInhabitants(IEnumerable<Actor> inhabitants)
        {
            _inhabitants.AddRange(inhabitants);
        }
    }
}