using System;

namespace RpgSimulator.Domain.Exceptions
{
    public class GameException : Exception
    {
        public GameException(string message) : base(message) { }
    }

    public class InvalidHealthException : GameException
    {
        public InvalidHealthException(int health) : base($"Неправильне значення здоров'я: {health}") { }
    }

    public class InvalidDamageException : GameException
    {
        public InvalidDamageException(int damage) : base($"Неправильне значення шкоди: {damage}") { }
    }
}