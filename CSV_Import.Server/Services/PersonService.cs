
namespace CSV_Import.Server.Services
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Globalization;

    public class PersonService
    {
        private readonly ApplicationContext _context;

        public PersonService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<List<Person>?> UploadCsv(IFormFile file)
        {
            List<Person> records = new List<Person>();
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using (var stream = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            }))
            {
                // Register the class map for validation
                csv.Context.RegisterClassMap<PersonMap>();

                try
                {
                    records = csv.GetRecords<Person>().ToList();

                    // Validate records manually to ensure required fields are filled
                    foreach (var record in records)
                    {
                        if (string.IsNullOrWhiteSpace(record.Name) || string.IsNullOrWhiteSpace(record.Phone) || record.Salary <= 0)
                        {
                            return null;
                        }
                    }

                    // Add records to database
                    await _context.Persons.AddRangeAsync(records);
                    await _context.SaveChangesAsync();
                }
                catch (CsvHelperException ex)
                {
                    return null;
                }
            }

            return records;
        }

        //public async Task<List<Person>?> UploadCsv(IFormFile file)
        //{
        //    List<Person> persons = null;
        //    try
        //    {
        //        if (file == null || file.Length == 0)
        //            return null;

        //        using var reader = new StreamReader(file.OpenReadStream());
        //        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        //        persons = csv.GetRecords<Person>().ToList();

        //        await _context.Persons.AddRangeAsync(persons);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        var str = ex.Message;
        //    }
            

        //    return persons;
        //}

        public async Task<Person> CreatePerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<bool> UpdatePerson(int id, Person person)
        {
            if (id <= 0)
                return false;

            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
                return false;

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
