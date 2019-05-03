namespace ActorService.Controllers
{
    public class ActorDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Quality { get; set; }
        
        public int CurrentHealth { get; set; }

        public int Health { get; set; }
        public int Power { get; set; }
        public int Speed { get; set; }

        public string Balance { get; set; }

        public int Experience { get; set; }
    }
}