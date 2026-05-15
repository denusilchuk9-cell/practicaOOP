using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgSimulator.Domain.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly Dictionary<int, T> _storage = new Dictionary<int, T>();
        private int _nextId = 1;

        public T GetById(int id)
        {
            _storage.TryGetValue(id, out T entity);
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _storage.Values.ToList();
        }

        public void Add(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null && idProperty.CanWrite)
            {
                idProperty.SetValue(entity, _nextId);
            }
            _storage[_nextId] = entity;
            _nextId++;
        }

        public void Remove(T entity)
        {
            var kvp = _storage.FirstOrDefault(x => x.Value == entity);
            if (kvp.Key != 0)
            {
                _storage.Remove(kvp.Key);
            }
        }

        public int Count()
        {
            return _storage.Count;
        }
    }
}