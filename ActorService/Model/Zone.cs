using System;
using System.Collections.Generic;
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
    
    /// <summary>
    /// Represents a Zone.
    ///
    /// Name, Level and ZoneType are immutable.
    /// </summary>
    public class Zone
    {
        
        public Zone(string name, int level, ZoneType zoneType)
        {
            Name = name;
            Level = level;
            ZoneType = zoneType;
        }
        
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public ZoneType ZoneType { get; private set; }

        public IEnumerable<Actor> Actors => _actors;
        public IEnumerable<Actor> Inhabitants => _inhabitants;

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
            var taken = _inhabitants.Take(amount).ToArray();
            _inhabitants.RemoveAll(a => taken.Contains(a));
            return taken;
        }

        public void AddInhabitants(IEnumerable<Actor> inhabitants)
        {
            _inhabitants.AddRange(inhabitants);
        }
    }
}