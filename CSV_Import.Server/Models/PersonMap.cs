namespace CSV_Import.Server.Models
{
    using CsvHelper.Configuration;
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.Name)
                .Name("Name")
                .Validate(args => !string.IsNullOrWhiteSpace(args.Field));

            Map(m => m.DateOfBirth)
                .Name("DateOfBirth")
                 .TypeConverter<CustomDateTimeConverter>();

            Map(m => m.Married)
                .Name("Married");

            Map(m => m.Phone)
                .Name("Phone")
                .Validate(args => !string.IsNullOrWhiteSpace(args.Field));

            Map(m => m.Salary)
                .Name("Salary")
                .Validate(args => decimal.TryParse(args.Field, out decimal salary) && salary > 0);
        }

    }
}
