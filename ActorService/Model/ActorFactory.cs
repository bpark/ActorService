using System;
using System.Collections.Generic;
using System.Linq;

namespace ActorService.Model
{
    public sealed class ActorFactory : IActorFactory
    {

        private readonly Dictionary<DistributionKey, Quality> _qualityDistribution = new Dictionary<DistributionKey, Quality>
        {
            [new DistributionKey(0, 40)] = Quality.Poor,
            [new DistributionKey(40, 70)] = Quality.Common,
            [new DistributionKey(70, 85)] = Quality.Uncommon,
            [new DistributionKey(85, 95)] = Quality.Rare,
            [new DistributionKey(95, 99)] = Quality.Epic,
            [new DistributionKey(99, 100)] = Quality.Legendary,
        };
        
        
        private readonly Random _random = new Random();
        
        public Actor CreateRandomActor()
        {
            var rnd = _random.Next(100);
            var quality = _qualityDistribution.First(k => k.Key.IsInRange(rnd)).Value;

            var actor = new Actor
            {
                Name = "Test Subject " + _random.Next(100),
                Level = 1,
                Experience = 0,
                Quality = quality,
                Balance = Balance.Values[_random.Next(Balance.Values.Length)],
                BaseHealth = _random.Next(1, 50),
                BasePower = _random.Next(1, 10),
                BaseSpeed = _random.Next(1, 10),
                Abilities = createAbilities()
            };

            actor.CurrentHealth = actor.Health;

            return actor;
        }

        private List<Ability> createAbilities()
        {
            var offensive = _random.Next(1, 3);

            var offensiveAbilities = Ability.Values.Where(a => a.AbilityType == AbilityType.Offensive).ToList();
            var defensiveAbilities = Ability.Values.Where(a => a.AbilityType == AbilityType.Defensive).ToList();

            List<Ability> list1; 
            List<Ability> list2; 
            if (offensive == 2)
            {
                list1 = offensiveAbilities;
                list2 = defensiveAbilities;
            }
            else
            {
                list1 = defensiveAbilities;
                list2 = offensiveAbilities;
            }
            
            var abilities = new List<Ability>();
            
            var half = list1.Count / 2;
            abilities.Add(list1[_random.Next(half)]);
            abilities.Add(list1[_random.Next(half, list1.Count)]);
            abilities.Add(list2[_random.Next(list2.Count)]);

            return abilities;
        }
    }

    internal class DistributionKey
    {
        private readonly int _min;
        private readonly int _max;

        public DistributionKey(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public bool IsInRange(int value)
        {
            return value >= _min && value < _max;
        }


        private bool Equals(DistributionKey other)
        {
            return _min == other._min && _max == other._max;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((DistributionKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_min * 397) ^ _max;
            }
        }
    }
}