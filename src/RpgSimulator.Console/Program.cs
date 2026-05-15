using System;
using System.Collections.Generic;
using RpgSimulator.App.Services;
using RpgSimulator.Domain.Entities;
using RpgSimulator.Domain.Patterns;
using RpgSimulator.Domain.Singleton;

namespace RpgSimulator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = GameSettings.Instance;

            ConsoleUI.DrawHeader("RPG SIMULATOR");
            ConsoleUI.WriteLine("Ласкаво просимо до RPG Simulator!", ConsoleColor.Green);
            ConsoleUI.WriteLine(string.Empty);
            string playerName = ConsoleUI.ReadOption("Введіть ім'я героя: ");
            if (string.IsNullOrWhiteSpace(playerName)) playerName = "Hero";

            var player = new Player(playerName, settings.BaseStrength);
            var combat = new CombatService(msg => ConsoleUI.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg ?? string.Empty}", ConsoleColor.Magenta));
            var battleService = new BattleService(combat);

            bool hasSword = false;

            while (player.IsAlive)
            {
                ConsoleUI.DrawHeader("Головне меню");
                ConsoleUI.WriteStatus("Герой: ", player.Name);
                ConsoleUI.WriteStatus("Рівень: ", player.Level.ToString());
                ConsoleUI.WriteStatus("HP: ", $"{player.Health}/{player.MaxHealth}");
                ConsoleUI.WriteStatus("Сила: ", player.Strength.ToString());
                ConsoleUI.WriteStatus("Досвід: ", $"{player.Experience}/{player.Level * settings.ExperiencePerLevel}");
                ConsoleUI.WriteStatus("Озброєння: ", hasSword ? "Меч" : "Без зброї");
                ConsoleUI.WriteLine(string.Empty);
                ConsoleUI.WriteLine("1 - Бій з випадковим ворогом", ConsoleColor.Yellow);
                ConsoleUI.WriteLine("2 - Бій з групою ворогів", ConsoleColor.Yellow);
                ConsoleUI.WriteLine("3 - Взяти меч (+10 до атаки)", ConsoleColor.Yellow);
                ConsoleUI.WriteLine("4 - Зберегти гру", ConsoleColor.Yellow);
                ConsoleUI.WriteLine("5 - Завантажити гру", ConsoleColor.Yellow);
                ConsoleUI.WriteLine("0 - Вихід", ConsoleColor.Yellow);
                string choice = ConsoleUI.ReadOption("Виберіть дію: ");

                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        var enemy = Enemy.Factory.CreateRandom();
                        combat.StartFight(player, enemy, hasSword ? new SwordAttackStrategy() : null);
                        ConsoleUI.Pause();
                        break;
                    case "2":
                        var enemies = new List<Enemy>
                        {
                            Enemy.Factory.CreateGoblin(),
                            Enemy.Factory.CreateGoblin(),
                            Enemy.Factory.CreateOrc()
                        };
                        battleService.StartMultipleFights(player, enemies);
                        ConsoleUI.Pause();
                        break;
                    case "3":
                        if (!hasSword)
                        {
                            hasSword = true;
                            ConsoleUI.WriteLine("Ви взяли меч! Тепер атака сильніша!", ConsoleColor.Cyan);
                        }
                        else
                        {
                            ConsoleUI.WriteLine("Меч вже в вас є!", ConsoleColor.DarkYellow);
                        }
                        ConsoleUI.Pause();
                        break;
                    case "4":
                        SaveService.SaveGame(player);
                        ConsoleUI.WriteLine("Гру збережено!", ConsoleColor.Green);
                        ConsoleUI.Pause();
                        break;
                    case "5":
                        if (SaveService.LoadGame(out Player? loadedPlayer))
                        {
                            player = loadedPlayer;
                            ConsoleUI.WriteLine("Гру завантажено!", ConsoleColor.Green);
                        }
                        else
                        {
                            ConsoleUI.WriteLine("Немає збереженої гри!", ConsoleColor.Red);
                        }
                        ConsoleUI.Pause();
                        break;
                    default:
                        ConsoleUI.WriteLine("Невідома команда", ConsoleColor.Red);
                        ConsoleUI.Pause();
                        break;
                }
            }

            ConsoleUI.DrawHeader("GAME OVER");
            ConsoleUI.WriteLine("Ви програли", ConsoleColor.Red);
            ConsoleUI.Pause();
        }
    }
}