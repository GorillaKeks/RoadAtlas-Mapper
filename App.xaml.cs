using System.Windows;
using ETS2LA.Mapper.Services;

namespace ETS2LA.Mapper;

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