using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using RoadAtlas.Mapper.Models;
using RoadAtlas.Mapper.Services;

namespace RoadAtlas.Mapper.Views;

public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();

        Loaded += DashboardView_Loaded;
    }

    private async void DashboardView_Loaded(
        object sender,
        RoutedEventArgs e)
    {
        LoadDashboard();

        await CheckForUpdates();
    }

    private void LoadDashboard()
    {
        StatusPanel.Children.Clear();

        AppSettings settings =
            ConfigurationService.Load();

        ETS2PathText.Text =
            $"ETS2 Path: {settings.ETS2Path}";

        ATSPathText.Text =
            $"ATS Path: {settings.ATSPath}";

        OutputFolderText.Text =
            $"Output Folder: {settings.OutputPath}";

        RepositoryText.Text =
            $"Repository: {settings.RepositoryPath}";

        GitHubStatusText.Text =
            "Connected";

        RepositoryStatusText.Text =
            "Ready";

        LastExtractionText.Text =
            "Never";

        LastGeneratorText.Text =
            "Never";

        LastRepositorySyncText.Text =
            "Never";

        var results =
            DependencyCheckService.RunChecks(settings);

        bool hasErrors = false;

        foreach (var result in results)
        {
            bool success =
                result.StartsWith("✓");

            if (!success)
            {
                hasErrors = true;
            }

            StatusPanel.Children.Add(
                new TextBlock
                {
                    Text = result,
                    FontSize = 14,
                    Margin = new Thickness(0, 3, 0, 3),
                    Foreground = success
                        ? Brushes.Green
                        : Brushes.Red
                });
        }

        OverallStatusText.Text =
            hasErrors
                ? "Overall Status: ATTENTION REQUIRED"
                : "Overall Status: READY";

        OverallStatusText.Foreground =
            hasErrors
                ? Brushes.Red
                : Brushes.Green;
    }

    private async Task CheckForUpdates()
    {
        try
        {
            string currentVersion =
                UpdateService.GetCurrentVersion();

            VersionText.Text =
                $"Version: {currentVersion}";

            bool updateAvailable =
                await UpdateService.UpdateAvailableAsync();

            if (updateAvailable)
            {
                string latestVersion =
                    await UpdateService.GetLatestVersionAsync()
                    ?? "Unknown";

                UpdateStatusText.Text =
                    $"⚠ Update available: {latestVersion}";

                UpdateStatusText.Foreground =
                    Brushes.Orange;

                OpenReleasePageButton.Visibility =
                    Visibility.Visible;
            }
            else
            {
                UpdateStatusText.Text =
                    "✔ You are running the latest version";

                UpdateStatusText.Foreground =
                    Brushes.Green;
            }
        }
        catch
        {
            UpdateStatusText.Text =
                "Unable to check for updates.";

            UpdateStatusText.Foreground =
                Brushes.Red;
        }
    }

    private void OpenReleasePageButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        Process.Start(
            new ProcessStartInfo
            {
                FileName =
                    "https://github.com/GorillaKeks/RoadAtlas-Mapper/releases",
                UseShellExecute = true
            });
    }

    private void OpenOutputFolderButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            AppSettings settings =
                ConfigurationService.Load();

            if (!string.IsNullOrWhiteSpace(settings.OutputPath))
            {
                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = settings.OutputPath,
                        UseShellExecute = true
                    });
            }
        }
        catch
        {
        }
    }

    private void OpenRepositoryButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            AppSettings settings =
                ConfigurationService.Load();

            if (!string.IsNullOrWhiteSpace(settings.RepositoryPath))
            {
                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = settings.RepositoryPath,
                        UseShellExecute = true
                    });
            }
        }
        catch
        {
        }
    }

    private async void CheckUpdatesButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        await CheckForUpdates();
    }
}