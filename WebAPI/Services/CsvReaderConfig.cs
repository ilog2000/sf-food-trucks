using System.Globalization;
using CsvHelper.Configuration;

namespace Services;

public static class CsvReaderConfig
{
    public static readonly CsvConfiguration DefaultCsvConfig =
        new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null,
            Delimiter = ",",
            TrimOptions = TrimOptions.Trim,
        };
}
