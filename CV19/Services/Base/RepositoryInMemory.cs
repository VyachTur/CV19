using CV19.Models.Interfaces;
using CV19.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CV19.Services.Base
{
    abstract class RepositoryInMemory<T> : IRepository<T> where T : IEntity
    {
        private readonly List<T> _entities = new List<T>();
        private int _lastId;

        protected RepositoryInMemory() { }

        protected RepositoryInMemory(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public void Add(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            if (_entities.Contains(entity)) return;

            entity.Id = ++_lastId;
            _entities.Add(entity);
        }

        public IEnumerable<T> GetAll() => _entities;

        public bool Remove(T entity) => _entities.Remove(entity);

        public void Update(int id, T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), id, "Индекс не может быть меньше 1");

            if (_entities.Contains(entity)) return;

            var db_entity = ((IRepository<T>)this).Get(id);

            if (db_entity is null) throw new InvalidOperationException("Редактируемый элемент не найден в репозитории");

            Update(entity, db_entity);
        }

        protected abstract void Update(T source, T destination);
    }
}
