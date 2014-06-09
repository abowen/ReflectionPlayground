using System.Collections.Generic;
using GenericLineOfBusiness.Common.Entities;

namespace GenericLineOfBusiness.Common.Interfaces
{
    public interface IProductRepository
    {
        // CREATE
        void Add(Product entity);

        // READ
        IEnumerable<Product> GetAll();
        Product Get(int id);

        // UPDATE
        void Update(Product entity);

        // DELETE
        void Delete(Product entity);
    }
}
