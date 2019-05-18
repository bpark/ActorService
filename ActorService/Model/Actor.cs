using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CSharpFunctionalExtensions;

namespace ActorService.Model
{
    public class Quality : ValueObject<Quality>
    {
        public static readonly Quality Poor = new Quality(1.0f, "Poor");
        public static readonly Quality Common = new Quality(1.1f, "Common");
        public static readonly Quality Uncommon = new Quality(1.2f, "Uncommon");
        public static readonly Quality Rare = new Quality(1.3f, "Rare");
        public static readonly Quality Epic = new Quality(1.4f, "Epic");
        public static readonly Quality Legendary = new Quality(1.5f, "Legendary");

        public float Multiplier { get; }
        public string Name { get; }

        public static IReadOnlyList<Quality> Values => new[] {Poor, Common, Uncommon, Rare, Epic, Legendary};

        private Quality(float multiplier, string name)
        {
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

        protected override bool EqualsCore(Quality other)
        {
            return base.Equals(other) && Multiplier.Equals(other.Multiplier) && string.Equals(Name, other.Name);
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ Multiplier.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }

    public class Balance : ValueObject<Balance>
    {
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

        public static Balance[] Values => new[] {PP, SS, HH, HP, PS, HS, PB, SB, HB, BB};

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

        protected override bool EqualsCore(Balance other)
        {
            return base.Equals(other) && Health.Equals(other.Health) && Power.Equals(other.Power) &&
                   Speed.Equals(other.Speed) && string.Equals(Name, other.Name);
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ Health.GetHashCode();
                hashCode = (hashCode * 397) ^ Power.GetHashCode();
                hashCode = (hashCode * 397) ^ Speed.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

    }

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