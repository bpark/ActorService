using System;
using CSharpFunctionalExtensions;

namespace ActorService.Model
{
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
}