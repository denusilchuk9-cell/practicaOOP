using RpgSimulator.Domain.Entities;
using System;

namespace RpgSimulator.Domain.Patterns
{
    public interface IAttackStrategy
    {
        void Execute(Player player, Enemy enemy);
    }

    public class NormalAttackStrategy : IAttackStrategy
    {
        public void Execute(Player player, Enemy enemy) => player.Attack(enemy);
    }

    public class CriticalAttackStrategy : IAttackStrategy
    {
        private static readonly Random _random = new Random();

        public void Execute(Player player, Enemy enemy)
        {
            int damage = _random.Next(15, 30) + player.Level * 2;
            enemy.TakeDamage(damage);
        }
    }
}