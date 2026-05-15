using System;

namespace RpgSimulator.Domain.Entities
{
    public class Player : Entity
    {
        private int _level = 1;
        private int _experience = 0;
        private int _strength;
        private static readonly Random _random = new Random();

        public int Level => _level;
        public int Experience => _experience;
        public int Strength => _strength;

        public Player(string name, int strength) : base(name, 100)
        {
            if (strength < 1 || strength > 100)
                throw new ArgumentException("Сила має бути 1-100");
            _strength = strength;
        }

        public Player(string name, int strength, int level, int experience, int health, int maxHealth) : base(name, maxHealth)
        {
            if (strength < 1 || strength > 100)
                throw new ArgumentException("Сила має бути 1-100");
            if (level < 1)
                throw new ArgumentException("Рівень має бути не менше 1");

            _strength = strength;
            _level = level;
            _experience = experience;
            MaxHealth = maxHealth;
            Health = Math.Clamp(health, 0, maxHealth);
        }

        public void AddExperience(int amount)
        {
            _experience += amount;
            int required = _level * 100;
            if (_experience >= required)
            {
                _level++;
                _experience -= required;
                MaxHealth += 20;
                Health = MaxHealth;
                _strength += 5;
            }
        }

        public override void Attack(Entity target)
        {
            int damage = _random.Next(5, 15) + _strength / 5;
            target.TakeDamage(damage);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (!IsAlive) Notify($"{Name} загинув!");
        }
    }
}