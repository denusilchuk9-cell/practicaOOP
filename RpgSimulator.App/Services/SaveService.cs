using System;
using System.IO;
using System.Text.Json;
using RpgSimulator.Domain.Entities;

namespace RpgSimulator.App.Services
{
    public static class SaveService
    {
        private static readonly string SavePath = "savegame.json";

        public static void SaveGame(Player player)
        {
            var data = new
            {
                player.Name,
                player.Level,
                player.Experience,
                player.Health,
                player.Strength,
                player.MaxHealth
            };
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(SavePath, json);
        }

        public static bool LoadGame(out Player player)
        {
            player = null;
            if (!File.Exists(SavePath)) return false;

            string json = File.ReadAllText(SavePath);
            using JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            string name = root.GetProperty("Name").GetString();
            int strength = root.GetProperty("Strength").GetInt32();

            player = new Player(name, strength);
            return true;
        }
    }
}