using RpgSimulator.Domain.Entities;

namespace RpgSimulator.Domain.Decorators
{
    public abstract class WeaponDecorator : Entity
    {
        protected Entity _entity;

        protected WeaponDecorator(Entity entity, string name, int maxHealth) : base(name, maxHealth)
        {
            _entity = entity;
        }

        public override void Attack(Entity target)
        {
            _entity.Attack(target);
        }

        public override void TakeDamage(int damage)
        {
            _entity.TakeDamage(damage);
        }
    }

    public class SwordDecorator : WeaponDecorator
    {
        private readonly int _bonusDamage;

        public SwordDecorator(Entity entity) : base(entity, entity.Name + " з мечем", entity.MaxHealth)
        {
            _bonusDamage = 10;
        }

        public override void Attack(Entity target)
        {
            base.Attack(target);
            target.TakeDamage(_bonusDamage);
        }
    }

    public class AxeDecorator : WeaponDecorator
    {
        private readonly int _bonusDamage;

        public AxeDecorator(Entity entity) : base(entity, entity.Name + " з сокирою", entity.MaxHealth)
        {
            _bonusDamage = 15;
        }

        public override void Attack(Entity target)
        {
            base.Attack(target);
            target.TakeDamage(_bonusDamage);
        }
    }
}