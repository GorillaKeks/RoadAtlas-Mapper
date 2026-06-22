using System.Windows;
using System.Windows.Controls;
using RoadAtlas.Mapper.Models;
using RoadAtlas.Mapper.Services;

namespace RoadAtlas.Mapper.Views;

public partial class SettingsView : UserControl
{
    private AppSettings _settings = new();

    public SettingsView()
    {
        InitializeComponent();

        LoadSettings();

        AutoDetectButton.Click += AutoDetectButton_Click;
    }

    private void LoadSettings()
    {
        _settings = ConfigurationService.Load();

        if (string.IsNullOrWhiteSpace(_settings.ETS2Path))
            _settings.ETS2Path = PathDetectionService.GetETS2Path();

        if (string.IsNullOrWhiteSpace(_settings.ATSPath))
            _settings.ATSPath = PathDetectionService.GetATSPath();

        if (string.IsNullOrWhiteSpace(_settings.ETS2ModPath))
            _settings.ETS2ModPath = PathDetectionService.GetETS2ModPath();

        if (string.IsNullOrWhiteSpace(_settings.ATSModPath))
            _settings.ATSModPath = PathDetectionService.GetATSModPath();

        if (string.IsNullOrWhiteSpace(_settings.OutputPath))
            _settings.OutputPath = PathDetectionService.GetOutputPath();

        RepositoryPathTextBox.Text =
            _settings.RepositoryPath;

        ETS2PathTextBox.Text =
            _settings.ETS2Path;

        ATSPathTextBox.Text =
            _settings.ATSPath;

        ETS2ModPathTextBox.Text =
            _settings.ETS2ModPath;

        ATSModPathTextBox.Text =
            _settings.ATSModPath;

        OutputPathTextBox.Text =
            _settings.OutputPath;

        WslDistributionComboBox.Items.Clear();

        foreach (var distro in WslService.GetDistributions())
        {
            WslDistributionComboBox.Items.Add(distro);
        }

        WslDistributionComboBox.SelectedItem =
            _settings.WslDistribution;
    }

    private void SaveSettings_Click(
        object sender,
        RoutedEventArgs e)
    {
        _settings.RepositoryPath =
            RepositoryPathTextBox.Text;

        _settings.ETS2Path =
            ETS2PathTextBox.Text;

        _settings.ATSPath =
            ATSPathTextBox.Text;

        _settings.ETS2ModPath =
            ETS2ModPathTextBox.Text;

        _settings.ATSModPath =
            ATSModPathTextBox.Text;

        _settings.OutputPath =
            OutputPathTextBox.Text;

        _settings.WslDistribution =
            WslDistributionComboBox.SelectedItem?.ToString()
            ?? "Ubuntu";

        ConfigurationService.Save(_settings);

        MessageBox.Show(
            "Settings saved successfully.",
            "RoadAtlas Mapper",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    private void ReloadSettings_Click(
        object sender,
        RoutedEventArgs e)
    {
        LoadSettings();
    }

    private void AutoDetectButton_Click(
        object? sender,
        RoutedEventArgs e)
    {
        ETS2PathTextBox.Text =
            PathDetectionService.GetETS2Path();

        ATSPathTextBox.Text =
            PathDetectionService.GetATSPath();

        ETS2ModPathTextBox.Text =
            PathDetectionService.GetETS2ModPath();

        ATSModPathTextBox.Text =
            PathDetectionService.GetATSModPath();

        OutputPathTextBox.Text =
            PathDetectionService.GetOutputPath();
    }
}