using Services;

public static class ServiceRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ICsvDownloader, CsvDownloader>();
        services.AddSingleton<IDataService, DataService>();
        services.AddTransient<IDataUpdater, DataUpdater>();
    }
}
