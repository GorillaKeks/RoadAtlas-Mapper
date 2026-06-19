using System.Windows;
using System.Windows.Controls;
using ETS2LA.Mapper.Models;
using ETS2LA.Mapper.Services;

namespace ETS2LA.Mapper.Views;

public partial class SettingsView : UserControl
{
    private AppSettings _settings = new();

    public SettingsView()
    {
        InitializeComponent();

        LoadSettings();
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

        ETS2PathTextBox.Text = _settings.ETS2Path;
        ATSPathTextBox.Text = _settings.ATSPath;
        ETS2ModPathTextBox.Text = _settings.ETS2ModPath;
        ATSModPathTextBox.Text = _settings.ATSModPath;
        OutputPathTextBox.Text = _settings.OutputPath;

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
        _settings.ETS2Path = ETS2PathTextBox.Text;
        _settings.ATSPath = ATSPathTextBox.Text;
        _settings.ETS2ModPath = ETS2ModPathTextBox.Text;
        _settings.ATSModPath = ATSModPathTextBox.Text;
        _settings.OutputPath = OutputPathTextBox.Text;

        _settings.WslDistribution =
            WslDistributionComboBox.SelectedItem?.ToString()
            ?? "Ubuntu";

        ConfigurationService.Save(_settings);

        MessageBox.Show(
            "Settings saved successfully.",
            "ETS2LA Mapper",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    private void ReloadSettings_Click(
        object sender,
        RoutedEventArgs e)
    {
        LoadSettings();
    }
}