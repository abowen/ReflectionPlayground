using System.Collections.Generic;
using System.Linq;
using GenericLineOfBusiness.Common.Entities;
using GenericLineOfBusiness.Common.Interfaces;

namespace GenericLineOfBusiness.RepositoryMemory
{
    public class PersonRepository : IPersonRepository
    {
        private readonly List<Person> _cachedEntities = new List<Person>
        {
            new Person { Id = 1, Age = 29, FirstName = "James", LastName = "Brown"},
            new Person { Id = 2, Age = 27, FirstName = "Ann", LastName = "Taylor"},
        };

        public void Add(Person entity)
        {
            if (entity == null) return;

            _cachedEntities.Add(entity);
        }

        public IEnumerable<Person> GetAll()
        {
            return _cachedEntities.AsEnumerable();
        }

        public Person Get(int id)
        {
            return _cachedEntities.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Person entity)
        {
            if (entity == null) return;

            var oldEntity = Get(entity.Id);
            if (oldEntity == null) return;

            Delete(oldEntity);
            Add(entity);
        }

        public void Delete(Person entity)
        {
            if (entity == null) return;
            if (!_cachedEntities.Contains(entity)) return;

            _cachedEntities.Remove(entity);
        }
    }
}
