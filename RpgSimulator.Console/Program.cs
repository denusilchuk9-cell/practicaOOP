using System;
using RpgSimulator.App.Services;
using RpgSimulator.Domain.Entities;
using RpgSimulator.Domain.Patterns;

namespace RpgSimulator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine("║     RPG SIMULATOR      ║");
            Console.WriteLine("╚════════════════════════╝\n");

            Console.Write("Ім'я героя: ");
            string playerName = Console.ReadLine()?.Trim() ?? "Hero";

            var player = new Player(playerName, 15);
            var combat = new CombatService(msg => Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}"));

            while (player.IsAlive)
            {
                Console.WriteLine($"\n❤️ {player.Name} | Lv.{player.Level} | HP:{player.Health}/{player.MaxHealth} | Сила:{player.Strength}");
                Console.WriteLine($"📊 Досвід: {player.Experience}/{player.Level * 100}");
                Console.WriteLine("\n1 - Бій | 0 - Вихід");

                var key = Console.ReadKey(true).KeyChar;

                if (key == '0') break;
                if (key == '1')
                {
                    var enemy = Enemy.Factory.CreateRandom();
                    combat.StartFight(player, enemy);
                }
            }

            Console.WriteLine("\n🎮 GAME OVER 🎮");
            Console.ReadKey();
        }
    }
}