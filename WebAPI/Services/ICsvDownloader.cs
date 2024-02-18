namespace Services;

public interface ICsvDownloader
{
    Task DownloadCsvFile(string url, string destination);
}
