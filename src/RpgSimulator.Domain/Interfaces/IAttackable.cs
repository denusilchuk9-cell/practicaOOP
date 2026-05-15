namespace RpgSimulator.Domain.Interfaces
{
    public interface IAttackable
    {
        void TakeDamage(int damage);
        bool IsAlive { get; }
    }
}