using System;

namespace RpgSimulator.ConsoleApp
{
    internal static class ConsoleUI
    {
        public static void DrawHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("========================================");
            Console.WriteLine($"         {title,-24}");
            Console.WriteLine("========================================");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WriteStatus(string label, string value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(label);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static string ReadOption(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(prompt);
            Console.ResetColor();
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }
}
