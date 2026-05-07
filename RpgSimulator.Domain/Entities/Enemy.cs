using System;

namespace RpgSimulator.Domain.Entities
{
    public class Enemy : Entity
    {
        public int ExperienceReward { get; private set; }
        private static readonly Random _random = new Random();

        private Enemy(string name, int maxHealth, int expReward) : base(name, maxHealth)
        {
            ExperienceReward = expReward;
        }

        public override void Attack(Entity target)
        {
            int damage = _random.Next(3, 12);
            target.TakeDamage(damage);
        }

        public static class Factory
        {
            private static readonly Random _rand = new Random();

            public static Enemy CreateGoblin() => new Enemy("Гоблін", 30, 50);
            public static Enemy CreateOrc() => new Enemy("Орк", 60, 100);
            public static Enemy CreateTroll() => new Enemy("Троль", 100, 200);

            public static Enemy CreateRandom()
            {
                int r = _rand.Next(3);
                if (r == 0) return CreateGoblin();
                if (r == 1) return CreateOrc();
                return CreateTroll();
            }
        }
    }
}