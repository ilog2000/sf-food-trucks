namespace Services;

public class CsvDownloader : ICsvDownloader
{
    public async Task DownloadCsvFile(string url, string csvFile)
    {
        using var client = new HttpClient();
        using var response = await client.GetAsync(url);
        await using var stream = await response.Content.ReadAsStreamAsync();
        await using var fileStream = new FileStream(csvFile, FileMode.Create, FileAccess.Write, FileShare.None);
        await stream.CopyToAsync(fileStream);
    }
}
