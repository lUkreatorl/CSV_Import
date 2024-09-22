namespace CSV_Import.Server.Models
{
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;
    using CsvHelper;
    using System.Globalization;

    public class CustomDateTimeConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var formats = new[]
            {
            "dd/MM/yyyy",
            "MM/dd/yyyy",
            "yyyy-MM-dd",
            "dd.MM.yyyy"
        };

            if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            throw new InvalidOperationException($"Invalid date format: '{text}'. Expected formats are: {string.Join(", ", formats)}");
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return ((DateTime)value).ToString("dd/MM/yyyy");
        }
    }

}
