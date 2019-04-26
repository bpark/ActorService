using System;

namespace ActorService.Model
{
    public enum AbilityType
    {
        Offensive = 0,
        Defensive = 1
    }

    public abstract class Ability
    {
        public static readonly Ability Slash = new Slash();
        public static readonly Ability BowShot = new BowShot();
        public static readonly Ability HolyFaith = new HolyFaith();
        public static readonly Ability HolyStrike = new HolyStrike();
        public static readonly Ability BlockingShield = new BlockingShield();
        public static readonly Ability IncompleteShield = new IncompleteShield();
        public static readonly Ability FireBolt = new FireBolt();
        public static readonly Ability Blizzard = new Blizzard();

        public static Ability[] Values => new Ability[]
            {Slash, BowShot, HolyFaith, HolyStrike, BlockingShield, IncompleteShield, FireBolt, Blizzard};

        protected Ability()
        {
        }

        public static Ability FromName(string name)
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

        public virtual string Name { get; }
        public virtual string Description { get; }
        public virtual AbilityType AbilityType { get; }
        public virtual int Cooldown { get; }
    }

    public class Slash : Ability
    {
        public override string Name => "Slash";
        public override string Description => $"Slashes into the enemy, dealing {Damage} damage.";
        public override AbilityType AbilityType => AbilityType.Offensive;
        public override int Cooldown => 1;

        public int Damage => 35;
    }

    public class BowShot : Ability
    {
        public override string Name => "Bow Shot";
        public override string Description => $"Fires a Bow Shot, dealing {Damage} damage.";
        public override AbilityType AbilityType => AbilityType.Offensive;
        public override int Cooldown => 1;

        public int Damage => 34;
    }

    public class HolyFaith : Ability
    {
        public override string Name => "Holy Faith";
        public override string Description => $"Restores {Heal} health.";
        public override AbilityType AbilityType => AbilityType.Defensive;
        public override int Cooldown => 4;

        public int Heal => 89;
    }

    public class HolyStrike : Ability
    {
        public override string Name => "Holy Strike";
        public override string Description => $"Restores {Heal} health and deals {Damage}.";
        public override AbilityType AbilityType => AbilityType.Offensive;
        public override int Cooldown => 1;

        public int Heal => 9;
        public int Damage => 24;
    }

    public class BlockingShield : Ability
    {
        public override string Name => "Blocking Shield";
        public override string Description => "A surrounding shield, blocks all attacks.";
        public override AbilityType AbilityType => AbilityType.Defensive;
        public override int Cooldown => 3;
    }

    public class IncompleteShield : Ability
    {
        public override string Name => "Incomplete Shield";
        public override string Description => "Blocks 25% damage.";
        public override AbilityType AbilityType => AbilityType.Defensive;
        public override int Cooldown => 4;
    }

    public class FireBolt : Ability
    {
        public override string Name => "Fire Bolt";

        public override string Description =>
            $"Fires a bolt into the enemy and deals {Damage} damage and 12 for the next 3 rounds.";

        public override AbilityType AbilityType => AbilityType.Offensive;
        public override int Cooldown => 3;

        public int Damage => 22;
    }

    public class Blizzard : Ability
    {
        public override string Name => "Blizzard";
        public override string Description => $"Deals {Damage} Frost damage and causes a Blizzard for 3 rounds.";
        public override AbilityType AbilityType => AbilityType.Offensive;
        public override int Cooldown => 3;

        public int Damage => 12;
    }
}