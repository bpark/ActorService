using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActorService.Model
{
    
    public class Quality {

        public static readonly Quality Poor = new Quality(1.0f, "Poor");
        public static readonly Quality Common = new Quality(1.1f, "Common");
        public static readonly Quality Uncommon = new Quality(1.2f, "Uncommon");
        public static readonly Quality Rare = new Quality(1.3f, "Rare");
        public static readonly Quality Epic = new Quality(1.4f, "Epic");
        public static readonly Quality Legendary = new Quality(1.5f, "Legendary");
            
        public float Multiplier {get; }
        public string Name { get; }

        public static IEnumerable<Quality> Values => new Quality[] { Poor, Common, Uncommon, Rare, Epic, Legendary };

        private Quality(float multiplier, string name) {
            Multiplier = multiplier;
            Name = name;
        }

        public static Quality FromName(string name)
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
    
    public class Balance {

        public static readonly Balance PP = new Balance(0, 20, 0, "PP");
        public static readonly Balance SS = new Balance(0, 0, 20, "SS");
        public static readonly Balance HH = new Balance(20, 0, 0, "HH");
        public static readonly Balance HP = new Balance(9, 9, 0, "HP");
        public static readonly Balance PS = new Balance(0, 9, 9, "PS");
        public static readonly Balance HS = new Balance(9, 0, 9, "HS");
        public static readonly Balance PB = new Balance(4, 9, 4, "PB");
        public static readonly Balance SB = new Balance(4, 4, 9, "SB");
        public static readonly Balance HB = new Balance(9, 4, 4, "HB");
        public static readonly Balance BB = new Balance(5, 5, 5, "BB");
            
        public float Health { get; }
        public float Power { get; }
        public float Speed { get; }
        
        public string Name { get; }
        
        public static Balance[] Values => new Balance[] { PP, SS, HH, HP, PS, HS, PB, SB, HB, BB };

        private Balance(int health, int power, int speed, string name)
        {
            Health = health;
            Power = power;
            Speed = speed;
            Name = name;
        }
        
        public static Balance FromName(string name)
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
    
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public Quality Quality { get; set; }
        
        public int CurrentHealth { get; set; }

        public int BaseHealth { get; set; }
        public int BasePower { get; set; }
        public int BaseSpeed { get; set; }

        public Balance Balance { get; set; }

        public int Experience { get; set; }
        
        public IReadOnlyList<Ability> Abilities { get; set; }

        [NotMapped]
        public int Health => (int)Math.Round((BaseHealth + Balance.Health / 10) * 5 * Level * Quality.Multiplier + 100);

        [NotMapped]
        public int Power => (int) Math.Round((BasePower + Balance.Power / 10) * Level * Quality.Multiplier);
        
        [NotMapped]
        public int Speed => (int) Math.Round((BaseSpeed + Balance.Speed / 10) * Level * Quality.Multiplier);

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