using System.Collections.Generic;
using RpgSimulator.Domain.Entities;

namespace RpgSimulator.Domain.Composite
{
    public class EnemyGroup : Entity
    {
        private readonly List<Enemy> _enemies = new List<Enemy>();

        public EnemyGroup(string name) : base(name, 0)
        {
        }

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }

        public override void Attack(Entity target)
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.IsAlive)
                {
                    enemy.Attack(target);
                }
            }
        }

        public override void TakeDamage(int damage)
        {
            foreach (var enemy in _enemies)
            {
                enemy.TakeDamage(damage);
            }
        }

        public int AliveCount
        {
            get
            {
                int count = 0;
                foreach (var enemy in _enemies)
                {
                    if (enemy.IsAlive) count++;
                }
                return count;
            }
        }

        public bool AllDead
        {
            get
            {
                foreach (var enemy in _enemies)
                {
                    if (enemy.IsAlive) return false;
                }
                return true;
            }
        }
    }
}