using System.Windows;
using RoadAtlas.Mapper.Services;

namespace RoadAtlas.Mapper;

public partial class App : Application
{
    protected override void OnStartup(
        StartupEventArgs e)
    {
        LoggingService.Initialize();

        base.OnStartup(e);
    }

    protected override void OnExit(
        ExitEventArgs e)
    {
        LoggingService.Shutdown();

        base.OnExit(e);
    }
}