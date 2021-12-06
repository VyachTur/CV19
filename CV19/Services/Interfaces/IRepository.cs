using CV19.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CV19.Services.Interfaces
{
    internal interface IRepository<T> where T : IEntity
    {
        void Add(T entity);

        IEnumerable<T> GetAll();

        T Get(int id) => GetAll().FirstOrDefault(entity => entity.Id == id);

        bool Remove(T entity);

        void Update(int id, T entity);
    }
}
