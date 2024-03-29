using Endpoints;
using Globals;
using Services;

var builder = WebApplication.CreateBuilder(args);
// Add CORS service
builder.Services.AddCors();
// Add JSON console logging service
builder.Logging.AddJsonConsole();
// Add custom services
builder.Services.RegisterServices();

// Configure the host to ignore exceptions from background services
builder.Services.Configure<HostOptions>(hostOptions =>
{
    hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});
// Add hosted service to update the dataset on schedule
builder.Services.AddHostedService<UpdateScheduler>();

var app = builder.Build();

// Configure CORS with no restrictions
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseHttpsRedirection();
app.UseStaticFiles();

RootEndpoints.Map(app);
ApiEndpoints.Map(app);

// If no CSV file exists, download it
if (!File.Exists(Constants.CsvFileName))
{
    app.Services.GetService<IDataUpdater>()?.Update().Wait();
}

app.Run();
