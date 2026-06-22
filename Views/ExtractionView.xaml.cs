using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Ookii.Dialogs.Wpf;
using RoadAtlas.Mapper.Models;
using RoadAtlas.Mapper.Services;

namespace RoadAtlas.Mapper.Views;

public partial class ExtractionView : UserControl
{
    public ExtractionView()
    {
        InitializeComponent();

        Loaded += ExtractionView_Loaded;
    }

    private void ExtractionView_Loaded(
        object sender,
        RoutedEventArgs e)
    {
        ResetPipelineStatus();
        LoadSettings();
    }

    private void GameComboBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (!IsLoaded)
            return;

        LoadSettings();
    }

    private void BrowseGamePathButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        var dialog = new VistaFolderBrowserDialog();

        if (dialog.ShowDialog() == true)
        {
            GamePathTextBox.Text =
                dialog.SelectedPath;
        }
    }

    private void BrowseModPathButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        var dialog = new VistaFolderBrowserDialog();

        if (dialog.ShowDialog() == true)
        {
            ModPathTextBox.Text =
                dialog.SelectedPath;
        }
    }

    private void BrowseOutputPathButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        var dialog = new VistaFolderBrowserDialog();

        if (dialog.ShowDialog() == true)
        {
            OutputPathTextBox.Text =
                dialog.SelectedPath;
        }
    }

    private void ResetPipelineStatus()
    {
        PipelineStatusText.Text = "Ready";
        ParserStatusText.Text = "Idle";
        GeneratorStatusText.Text = "Idle";

        PipelineProgressBar.Value = 0;
    }

    private void LoadDetectedContent()
    {
        DetectedDlcsListBox.Items.Clear();
        DetectedModsListBox.Items.Clear();

        var dlcs =
    DlcDetectionService.GetInstalledDlcs(
        GamePathTextBox.Text);

        OutputTextBox.Text =
            $"GamePath: {GamePathTextBox.Text}" +
            Environment.NewLine +
            $"Found DLCs: {dlcs.Count}";

        foreach (var dlc in dlcs)
        {
            DetectedDlcsListBox.Items.Add(
                $"✓ {dlc}");
        }

        if (dlcs.Count == 0)
        {
            DetectedDlcsListBox.Items.Add(
                "No DLCs detected");
        }

        DetectedModsListBox.Items.Add(
            "Mod detection coming soon...");

        RoadsTextBlock.Text = "-";
        CitiesTextBlock.Text = "-";
        CompaniesTextBlock.Text = "-";
        PrefabsTextBlock.Text = "-";
    }

    private async void RunParserButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            RunParserButton.IsEnabled = false;

            ParserStatusText.Text = "Running";
            PipelineStatusText.Text = "Running";

            PipelineProgressBar.Value = 25;

            OutputTextBox.Text =
                "Starting parser..." +
                Environment.NewLine +
                Environment.NewLine;

            string result =
                await Task.Run(() =>
                    ParserService.RunParser(
                        GamePathTextBox.Text,
                        ModPathTextBox.Text,
                        OutputPathTextBox.Text));

            OutputTextBox.Text = result;

            ParserStatusText.Text = "Completed";
            PipelineStatusText.Text = "Ready";

            PipelineProgressBar.Value = 100;
        }
        catch (Exception ex)
        {
            OutputTextBox.Text = ex.ToString();

            ParserStatusText.Text = "Failed";
            PipelineStatusText.Text = "Failed";
        }
        finally
        {
            RunParserButton.IsEnabled = true;
        }
    }

    private async void RunGeneratorButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            RunGeneratorButton.IsEnabled = false;

            GeneratorStatusText.Text = "Running";
            PipelineStatusText.Text = "Running";

            PipelineProgressBar.Value = 50;

            bool isEts2 =
                GameComboBox.SelectedIndex == 0;

            OutputTextBox.Text =
                "Starting generator..." +
                Environment.NewLine +
                Environment.NewLine;

            string result =
                await Task.Run(() =>
                    GeneratorService.RunGraphGenerator(
                        OutputPathTextBox.Text,
                        isEts2));

            OutputTextBox.Text = result;

            GeneratorStatusText.Text = "Completed";
            PipelineStatusText.Text = "Ready";

            PipelineProgressBar.Value = 100;
        }
        catch (Exception ex)
        {
            OutputTextBox.Text = ex.ToString();

            GeneratorStatusText.Text = "Failed";
            PipelineStatusText.Text = "Failed";
        }
        finally
        {
            RunGeneratorButton.IsEnabled = true;
        }
    }

    private async void RunPipelineButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            RunParserButton.IsEnabled = false;
            RunGeneratorButton.IsEnabled = false;
            RunPipelineButton.IsEnabled = false;

            PipelineStatusText.Text = "Running";
            ParserStatusText.Text = "Running";
            GeneratorStatusText.Text = "Waiting";

            PipelineProgressBar.Value = 10;

            OutputTextBox.Text =
                "Starting full pipeline..." +
                Environment.NewLine +
                Environment.NewLine;

            await Task.Run(() =>
                ParserService.RunParser(
                    GamePathTextBox.Text,
                    ModPathTextBox.Text,
                    OutputPathTextBox.Text));

            ParserStatusText.Text = "Completed";
            GeneratorStatusText.Text = "Running";

            PipelineProgressBar.Value = 60;

            bool isEts2 =
                GameComboBox.SelectedIndex == 0;

            string generatorResult =
                await Task.Run(() =>
                    GeneratorService.RunGraphGenerator(
                        OutputPathTextBox.Text,
                        isEts2));

            OutputTextBox.AppendText(
                Environment.NewLine +
                Environment.NewLine +
                generatorResult);

            GeneratorStatusText.Text = "Completed";
            PipelineStatusText.Text = "Completed";

            PipelineProgressBar.Value = 100;
        }
        catch (Exception ex)
        {
            OutputTextBox.AppendText(
                Environment.NewLine +
                Environment.NewLine +
                ex);

            PipelineStatusText.Text = "Failed";
            ParserStatusText.Text = "Failed";
            GeneratorStatusText.Text = "Failed";
        }
        finally
        {
            RunParserButton.IsEnabled = true;
            RunGeneratorButton.IsEnabled = true;
            RunPipelineButton.IsEnabled = true;
        }
    }

    private void ClearOutputButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        OutputTextBox.Clear();

        ResetPipelineStatus();
    }

    private void LoadSettings()
    {
        try
        {
            AppSettings settings =
                ConfigurationService.Load();

            bool isEts2 =
                GameComboBox.SelectedIndex == 0;

            GamePathTextBox.Text =
                isEts2
                    ? settings.ETS2Path
                    : settings.ATSPath;

            ModPathTextBox.Text =
                isEts2
                    ? settings.ETS2ModPath
                    : settings.ATSModPath;

            OutputPathTextBox.Text =
                settings.OutputPath;

            LoadDetectedContent(); // HIER EINFÜGEN
        }
        catch
        {
        }
    }
}