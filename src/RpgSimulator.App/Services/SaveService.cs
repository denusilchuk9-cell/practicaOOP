using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using RpgSimulator.Domain.Entities;

namespace RpgSimulator.App.Services
{
    public static class SaveService
    {
        private static readonly string SavePath = "savegame.json";

        private sealed class PlayerSaveData
        {
            public string Name { get; set; } = string.Empty;
            public int Level { get; set; }
            public int Experience { get; set; }
            public int Health { get; set; }
            public int Strength { get; set; }
            public int MaxHealth { get; set; }
        }

        public static void SaveGame(Player player)
        {
            var data = new PlayerSaveData
            {
                Name = player.Name,
                Level = player.Level,
                Experience = player.Experience,
                Health = player.Health,
                Strength = player.Strength,
                MaxHealth = player.MaxHealth
            };
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SavePath, json);
        }

        public static bool LoadGame([NotNullWhen(true)] out Player? player)
        {
            player = null;
            if (!File.Exists(SavePath)) return false;

            string json = File.ReadAllText(SavePath);
            var data = JsonSerializer.Deserialize<PlayerSaveData>(json);
            if (data is null) return false;

            player = new Player(data.Name, data.Strength, data.Level, data.Experience, data.Health, data.MaxHealth);
            return true;
        }
    }
}