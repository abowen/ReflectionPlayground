using System.Collections.Generic;
using GenericLineOfBusiness.Common.Entities;

namespace GenericLineOfBusiness.Common.Interfaces
{
    public interface IPersonRepository
    {
        // CREATE
        void Add(Person entity);

        // READ
        IEnumerable<Person> GetAll();
        Person Get(int id);

        // UPDATE
        void Update(Person entity);

        // DELETE
        void Delete(Person entity);
    }
}
