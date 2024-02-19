namespace Services;

public class UpdateScheduler : IHostedService, IDisposable
{
    private readonly IDataUpdater _dataUpdater;
    private readonly ILogger<UpdateScheduler> _logger;
    private Timer? _timer;

    public UpdateScheduler(IDataUpdater dataUpdater, ILogger<UpdateScheduler> logger)
    {
        _dataUpdater = dataUpdater;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Run the update every day
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        _logger.LogInformation("Update started");
        _dataUpdater.Update().Wait();
        _logger.LogInformation("Update finished");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
