using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using ETS2LA.Mapper.Models;
using ETS2LA.Mapper.Services;

namespace ETS2LA.Mapper.Views;

public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();

        LoadDashboard();
    }

    private void LoadDashboard()
    {
        StatusPanel.Children.Clear();

        AppSettings settings =
            ConfigurationService.Load();

        var results =
            DependencyCheckService.RunChecks(settings);

        bool hasErrors = false;

        foreach (var result in results)
        {
            bool success = result.StartsWith("✓");

            if (!success)
            {
                hasErrors = true;
            }

            StatusPanel.Children.Add(new TextBlock
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
}