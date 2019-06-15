using System;
using System.Collections.Generic;

namespace ActorService.Model
{
    public class Actor
    {
        public Actor(int baseHealth, int basePower, int baseSpeed)
        {
            BaseHealth = baseHealth;
            BasePower = basePower;
            BaseSpeed = baseSpeed;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Quality Quality { get; set; }

        public int CurrentHealth { get; set; }

        public int BaseHealth { get; private set;  }
        public int BasePower { get; private set; }
        public int BaseSpeed { get; private set; }
        
        public int Health { get; private set; }
        public int Power { get; private set; }
        public int Speed { get; private set; }
        
        public int Level { get; private set; }

        public Balance Balance { get; set; }

        public int Experience { get; set; }

        public IReadOnlyList<Ability> Abilities { get; set; }

        public void LevelUp(int level)
        {
            Level = level;
            Health = (int) Math.Round((BaseHealth + Balance.Health / 10) * 5 * Level * Quality.Multiplier + 100);
            Power = (int) Math.Round((BasePower + Balance.Power / 10) * Level * Quality.Multiplier);
            Speed = (int) Math.Round((BaseSpeed + Balance.Speed / 10) * Level * Quality.Multiplier);
            CurrentHealth = Health;
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Actor) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        private bool Equals(Actor other)
        {
            return Id == other.Id;
        }
    }
}