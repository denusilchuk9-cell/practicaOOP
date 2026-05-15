namespace RpgSimulator.Domain.Singleton
{
    public sealed class GameSettings
    {
        private static GameSettings _instance;
        private static readonly object _lock = new object();

        public int MaxHealth { get; set; } = 100;
        public int BaseStrength { get; set; } = 15;
        public int ExperiencePerLevel { get; set; } = 100;

        private GameSettings() { }

        public static GameSettings Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameSettings();
                    }
                    return _instance;
                }
            }
        }
    }
}