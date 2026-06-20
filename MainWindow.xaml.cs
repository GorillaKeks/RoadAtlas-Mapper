using System.Windows;
using RoadAtlas.Mapper.Views;

namespace RoadAtlas.Mapper;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ShowDashboard();
    }

    private void DashboardButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        ShowDashboard();
    }

    private void SettingsButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        ShowSettings();
    }

    private void ExtractionButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        ShowExtraction();
    }

    private void RepositoryButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        ShowRepository();
    }

    private void LogsButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        ShowLogs();
    }

    private void ShowDashboard()
    {
        MainContent.Content =
            new DashboardView();
    }

    private void ShowSettings()
    {
        MainContent.Content =
            new SettingsView();
    }

    private void ShowExtraction()
    {
        MainContent.Content =
            new ExtractionView();
    }

    private void ShowRepository()
    {
        MainContent.Content =
            new RepositoryView();
    }

    private void ShowLogs()
    {
        MainContent.Content =
            new LogsView();
    }
}