using System;
using System.Collections.Generic;
using RpgSimulator.App.Services;
using RpgSimulator.Domain.Entities;
using RpgSimulator.Domain.Patterns;
using RpgSimulator.Domain.Singleton;
using RpgSimulator.Domain.Decorators;

namespace RpgSimulator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = GameSettings.Instance;

            System.Console.WriteLine("================================");
            System.Console.WriteLine("        RPG SIMULATOR           ");
            System.Console.WriteLine("================================");
            System.Console.WriteLine();

            System.Console.Write("Введіть ім'я героя: ");
            string playerName = System.Console.ReadLine()?.Trim() ?? "Hero";

            var player = new Player(playerName, settings.BaseStrength);
            var combat = new CombatService(msg => System.Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}"));
            var battleService = new BattleService(combat);

            bool hasSword = false;

            while (player.IsAlive)
            {
                System.Console.WriteLine();
                System.Console.WriteLine($"Герой: {player.Name} | Рівень: {player.Level} | HP: {player.Health}/{player.MaxHealth} | Сила: {player.Strength}");
                System.Console.WriteLine($"Досвід: {player.Experience}/{player.Level * settings.ExperiencePerLevel}");
                System.Console.WriteLine();
                System.Console.WriteLine("1 - Бій з випадковим ворогом");
                System.Console.WriteLine("2 - Бій з групою ворогів");
                System.Console.WriteLine("3 - Взяти меч (+10 до атаки)");
                System.Console.WriteLine("4 - Зберегти гру");
                System.Console.WriteLine("5 - Завантажити гру");
                System.Console.WriteLine("0 - Вихід");
                System.Console.Write("Виберіть дію: ");

                string choice = System.Console.ReadLine();

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
                            // SwordDecorator обгортає player, але залишаємо оригінальний player
                            // і застосовуємо декоратор лише для атаки
                            var swordPlayer = new SwordDecorator(player);
                            hasSword = true;
                            System.Console.WriteLine("Ви взяли меч! Тепер атака сильніша!");
                            // Використовуємо swordPlayer для наступного бою
                            var nextEnemy = Enemy.Factory.CreateRandom();
                            combat.StartFight(player, nextEnemy, null);
                        }
                        else
                        {
                            System.Console.WriteLine("Меч вже в вас є!");
                        }
                        break;
                    case "4":
                        SaveService.SaveGame(player);
                        System.Console.WriteLine("Гру збережено!");
                        break;
                    case "5":
                        if (SaveService.LoadGame(out Player loadedPlayer))
                        {
                            player = loadedPlayer;
                            System.Console.WriteLine("Гру завантажено!");
                        }
                        else
                        {
                            System.Console.WriteLine("Немає збереженої гри!");
                        }
                        break;
                    default:
                        System.Console.WriteLine("Невідома команда");
                        break;
                }
            }

            System.Console.WriteLine();
            System.Console.WriteLine("GAME OVER");
            System.Console.ReadKey();
        }
    }
}