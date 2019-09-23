using System;
using System.Collections.Generic;

namespace ActorService.Model
{

    internal class PlayerAbility
    {
        public int Cooldown { get; set; }
        public int Current { get; set; }
        public int Effect { get; set; }
        public int Target { get; set; }
        
    }

    internal class State
    {
        public int Health { get; set; }
        public int EnemyHealth { get; set; }
        public (int, int, int) Ability { get; set; }

        public override string ToString()
        {
            return $"{nameof(Health)}: {Health}, {nameof(EnemyHealth)}: {EnemyHealth}";
        }
    }
    
    public static class SimpleLearning
    {
        private static void GenerateStates()
        {
            var states = new List<State>();
            for (var i = 0; i <= 10; i++)
            {
                var state = new State { Health = i, Ability = (0, 0, -10) };
                for (var j = 0; j <= 10; j++)
                {
                    state.EnemyHealth = j;
                    Console.WriteLine($"{state}");
                }
            }
        }

        private static void Main(string[] args)
        {
            SimpleLearning.GenerateStates();   
        }
    }
    
}