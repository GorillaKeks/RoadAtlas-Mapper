using Serilog;
using System.IO;

namespace ETS2LA.Mapper.Services;

public static class LoggingService
{
    private static bool _initialized;

    public static void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        string appFolder =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                "ETS2LA Mapper");

        string logFolder =
            Path.Combine(
                appFolder,
                "Logs");

        Directory.CreateDirectory(appFolder);
        Directory.CreateDirectory(logFolder);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(
                Path.Combine(logFolder, "log-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                shared: true)
            .CreateLogger();

        _initialized = true;

        Log.Information("========================================");
        Log.Information("ETS2LA Mapper started");
        Log.Information("Application folder: {AppFolder}", appFolder);
        Log.Information("Log folder: {LogFolder}", logFolder);
        Log.Information("========================================");
    }

    public static void Info(string message)
    {
        EnsureInitialized();

        Log.Information(message);
    }

    public static void Error(string message)
    {
        EnsureInitialized();

        Log.Error(message);
    }

    public static void Error(string message, Exception exception)
    {
        EnsureInitialized();

        Log.Error(exception, message);
    }

    public static void Shutdown()
    {
        Log.Information("ETS2LA Mapper shutting down");

        Log.CloseAndFlush();
    }

    private static void EnsureInitialized()
    {
        if (!_initialized)
        {
            Initialize();
        }
    }
}