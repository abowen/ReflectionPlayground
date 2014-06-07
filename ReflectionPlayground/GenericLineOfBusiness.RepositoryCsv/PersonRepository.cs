using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using GenericLineOfBusiness.Common.Entities;
using GenericLineOfBusiness.Common.Interfaces;

namespace GenericLineOfBusiness.RepositoryCsv
{
    public class PersonRepository : IPersonRepository
    {
        // http://joshclose.github.io/CsvHelper/

        private static string csvFileName = @".\Person.csv";
        
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
        
        public void Add(Person entity)
        {
            if (entity == null) return;

            var entities = GetAll().ToList();
            entities.Add(entity);

            var cvsvWriter = GetCsvWriter();
            cvsvWriter.WriteRecords(entities);
        }

        public IEnumerable<Person> GetAll()
        {
            var csvReader = GetCsvReader();
            return csvReader.GetRecords<Person>();
        }

        public Person Get(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
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

            var entities = GetAll().ToList();
            if (!entities.Contains(entity)) return;

            entities.Remove(entity);
            var csvWriter = GetCsvWriter();
            csvWriter.WriteRecords(entities);
        }
    }
}
