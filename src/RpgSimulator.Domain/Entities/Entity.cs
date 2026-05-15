using System;
using System.Collections.Generic;
using RpgSimulator.Domain.Interfaces;

namespace RpgSimulator.Domain.Entities
{
    public abstract class Entity : IAttackable, IObservable
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public bool IsAlive => Health > 0;

        protected Entity(string name, int maxHealth)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        public virtual void TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - damage);
        }

        public abstract void Attack(Entity target);

        public void Attach(IObserver observer) => _observers.Add(observer);
        public void Detach(IObserver observer) => _observers.Remove(observer);

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}