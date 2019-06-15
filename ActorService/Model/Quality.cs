using System;
using System.Collections.Generic;
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
}