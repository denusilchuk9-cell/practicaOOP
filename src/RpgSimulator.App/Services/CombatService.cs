using System;
using System.Threading;
using RpgSimulator.Domain.Entities;
using RpgSimulator.Domain.Patterns;

namespace RpgSimulator.App.Services
{
    public class CombatService
    {
        private readonly Action<string> _logger;

        public CombatService(Action<string> logger)
        {
            _logger = logger;
        }

        public void StartFight(Player player, Enemy enemy, IAttackStrategy? strategy = null)
        {
            strategy = strategy ?? new NormalAttackStrategy();
            _logger($"Бій: {player.Name} vs {enemy.Name}!");

            while (player.IsAlive && enemy.IsAlive)
            {
                strategy.Execute(player, enemy);
                if (!enemy.IsAlive) break;
                enemy.Attack(player);
                Thread.Sleep(300);
            }

            if (player.IsAlive)
            {
                player.AddExperience(enemy.ExperienceReward);
                _logger($"{player.Name} переміг! +{enemy.ExperienceReward} досвіду!");
            }
        }
    }
}