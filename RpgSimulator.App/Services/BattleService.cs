using System;
using System.Collections.Generic;
using System.Linq;
using RpgSimulator.Domain.Entities;

namespace RpgSimulator.App.Services
{
    public class BattleService
    {
        private readonly CombatService _combatService;

        public BattleService(CombatService combatService)
        {
            _combatService = combatService;
        }

        public void StartMultipleFights(Player player, List<Enemy> enemies)
        {
            var aliveEnemies = enemies.Where(e => e.IsAlive).ToList();
            var enemiesByHealth = aliveEnemies.OrderByDescending(e => e.Health).ToList();

            foreach (var enemy in enemiesByHealth)
            {
                if (!player.IsAlive) break;
                _combatService.StartFight(player, enemy);
            }
        }

        public List<Enemy> GetStrongEnemies(List<Enemy> enemies, int minHealth)
        {
            return enemies.Where(e => e.Health >= minHealth && e.IsAlive).ToList();
        }

        public int GetTotalExperienceReward(List<Enemy> enemies)
        {
            return enemies.Where(e => !e.IsAlive).Sum(e => e.ExperienceReward);
        }
    }
}