using Globals;

namespace Services;

public class DataUpdater : IDataUpdater
{
    private readonly IDataService _dataService;
    private readonly IConfiguration _configuration;
    private readonly ICsvDownloader _downloader;
    private readonly ILogger<DataUpdater> _logger;

    public DataUpdater(IDataService dataService, IConfiguration configuration, ICsvDownloader downloader, ILogger<DataUpdater> logger)
    {
        _dataService = dataService;
        _configuration = configuration;
        _downloader = downloader;
        _logger = logger;
    }

    public async Task Update()
    {
        var url = _configuration[Constants.DatasetUrl];
        if (string.IsNullOrEmpty(url))
        {
            var msg = "Dataset URL is not set";
            _logger.LogError(msg);
            throw new InvalidOperationException(msg);
        }
        await _downloader.DownloadCsvFile(url, Constants.CsvFileName);
        _dataService.Load();
    }
}
