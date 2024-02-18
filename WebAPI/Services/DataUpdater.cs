using Globals;

namespace Services;

public class DataUpdater : IDataUpdater
{
    private readonly IDataService _dataService;
    private readonly IConfiguration _configuration;
    private readonly ICsvDownloader _downloader;

    public DataUpdater(IDataService dataService, IConfiguration configuration, ICsvDownloader downloader)
    {
        _dataService = dataService;
        _configuration = configuration;
        _downloader = downloader;
    }

    public async Task Update()
    {
        var url = _configuration[Constants.DatasetUrl];
        if (string.IsNullOrEmpty(url))
        {
            throw new InvalidOperationException("Dataset URL is not set");
        }
        await _downloader.DownloadCsvFile(url, Constants.CsvFileName);
        _dataService.Load();
    }
}
