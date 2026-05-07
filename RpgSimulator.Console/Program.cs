using System;
using System.Collections.Generic;
using RpgSimulator.App.Services;
using RpgSimulator.Domain.Entities;
using RpgSimulator.Domain.Patterns;
using RpgSimulator.Domain.Singleton;
using RpgSimulator.Domain.Decorators;

namespace RpgSimulator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = GameSettings.Instance;

            Console.WriteLine("================================");
            Console.WriteLine("        RPG SIMULATOR           ");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.Write("Введіть ім'я героя: ");
            string playerName = Console.ReadLine()?.Trim() ?? "Hero";

            var player = new Player(playerName, settings.BaseStrength);
            var combat = new CombatService(msg => Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}"));
            var battleService = new BattleService(combat);

            bool hasSword = false;

            while (player.IsAlive)
            {
                Console.WriteLine();
                Console.WriteLine($"Герой: {player.Name} | Рівень: {player.Level} | HP: {player.Health}/{player.MaxHealth} | Сила: {player.Strength}");
                Console.WriteLine($"Досвід: {player.Experience}/{player.Level * settings.ExperiencePerLevel}");
                Console.WriteLine();
                Console.WriteLine("1 - Бій з випадковим ворогом");
                Console.WriteLine("2 - Бій з групою ворогів");
                Console.WriteLine("3 - Взяти меч (+10 до атаки)");
                Console.WriteLine("4 - Зберегти гру");
                Console.WriteLine("5 - Завантажити гру");
                Console.WriteLine("0 - Вихід");
                Console.Write("Виберіть дію: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        var enemy = Enemy.Factory.CreateRandom();
                        combat.StartFight(player, enemy);
                        break;
                    case "2":
                        var enemies = new List<Enemy>
                        {
                            Enemy.Factory.CreateGoblin(),
                            Enemy.Factory.CreateGoblin(),
                            Enemy.Factory.CreateOrc()
                        };
                        battleService.StartMultipleFights(player, enemies);
                        break;
                    case "3":
                        if (!hasSword)
                        {
                            player = new SwordDecorator(player) as Player;
                            hasSword = true;
                            Console.WriteLine("Ви взяли меч! Тепер атака сильніша!");
                        }
                        else
                        {
                            Console.WriteLine("Меч вже в вас є!");
                        }
                        break;
                    case "4":
                        SaveService.SaveGame(player);
                        Console.WriteLine("Гру збережено!");
                        break;
                    case "5":
                        if (SaveService.LoadGame(out Player loadedPlayer))
                        {
                            player = loadedPlayer;
                            Console.WriteLine("Гру завантажено!");
                        }
                        else
                        {
                            Console.WriteLine("Немає збереженої гри!");
                        }
                        break;
                    default:
                        Console.WriteLine("Невідома команда");
                        break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("GAME OVER");
            Console.ReadKey();
        }
    }
}