using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using GenericLineOfBusiness.Common.Entities;
using GenericLineOfBusiness.Common.Interfaces;

namespace GenericLineOfBusiness.RepositoryCsv
{
    public class ProductRepository : IProductRepository
    {
        // http://joshclose.github.io/CsvHelper/

        private static string csvFileName = @".\Product.csv";
        
        private static CsvReader GetCsvReader()
        {            
            var textReader = new StreamReader(csvFileName);
            var csvReader = new CsvReader(textReader);
            return csvReader;
        }

        private static CsvWriter GetCsvWriter()
        {
            var textWriter = new StreamWriter(csvFileName);
            var csvWriter = new CsvWriter(textWriter);
            return csvWriter;
        }

        public void Add(Product entity)
        {
            if (entity == null) return;

            var entities = GetAll().ToList();
            entities.Add(entity);

            var cvsvWriter = GetCsvWriter();
#pragma warning disable 618
            cvsvWriter.WriteRecords(entities);
#pragma warning restore 618
        }

        public IEnumerable<Product> GetAll()
        {
            var csvReader = GetCsvReader();
            return csvReader.GetRecords<Product>();
        }

        public Product Get(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public void Update(Product entity)
        {
            if (entity == null) return;

            var oldEntity = Get(entity.Id);
            if (oldEntity == null) return;

            Delete(oldEntity);
            Add(entity);
        }

        public void Delete(Product entity)
        {
            if (entity == null) return;

            var entities = GetAll().ToList();
            if (!entities.Contains(entity)) return;

            entities.Remove(entity);
            var csvWriter = GetCsvWriter();
#pragma warning disable 618
            csvWriter.WriteRecords(entities);
#pragma warning restore 618
        }
    }
}
