using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ETS2LA.Mapper.Models;
using ETS2LA.Mapper.Services;

namespace ETS2LA.Mapper.Views;

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

    private async void RunParserButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            string gamePath =
                GamePathTextBox.Text?.Trim() ?? string.Empty;

            string modPath =
                ModPathTextBox.Text?.Trim() ?? string.Empty;

            string outputPath =
                OutputPathTextBox.Text?.Trim() ?? string.Empty;

            RunParserButton.IsEnabled = false;

            OutputTextBox.Text =
                "Starting parser..." +
                Environment.NewLine +
                Environment.NewLine;

            string result =
                await Task.Run(() =>
                    ParserService.RunParser(
                        gamePath,
                        modPath,
                        outputPath));

            OutputTextBox.Text = result;
        }
        catch (Exception ex)
        {
            OutputTextBox.Text = ex.ToString();
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
            string outputPath =
                OutputPathTextBox.Text?.Trim() ?? string.Empty;

            bool isEts2 =
                GameComboBox.SelectedIndex == 0;

            RunGeneratorButton.IsEnabled = false;

            OutputTextBox.Text =
                "Starting generator..." +
                Environment.NewLine +
                Environment.NewLine;

            string result =
                await Task.Run(() =>
                    GeneratorService.RunGraphGenerator(
                        outputPath,
                        isEts2));

            OutputTextBox.Text = result;
        }
        catch (Exception ex)
        {
            OutputTextBox.Text = ex.ToString();
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
            string gamePath =
                GamePathTextBox.Text?.Trim() ?? string.Empty;

            string modPath =
                ModPathTextBox.Text?.Trim() ?? string.Empty;

            string outputPath =
                OutputPathTextBox.Text?.Trim() ?? string.Empty;

            bool isEts2 =
                GameComboBox.SelectedIndex == 0;

            RunParserButton.IsEnabled = false;
            RunGeneratorButton.IsEnabled = false;
            RunPipelineButton.IsEnabled = false;

            OutputTextBox.Text =
                "Starting full pipeline..." +
                Environment.NewLine +
                Environment.NewLine +
                "Step 1/2: Running parser..." +
                Environment.NewLine;

            string parserResult =
                await Task.Run(() =>
                    ParserService.RunParser(
                        gamePath,
                        modPath,
                        outputPath));

            OutputTextBox.AppendText(
                Environment.NewLine +
                Environment.NewLine +
                "Parser completed." +
                Environment.NewLine +
                Environment.NewLine +
                "Step 2/2: Running generator..." +
                Environment.NewLine);

            string generatorResult =
                await Task.Run(() =>
                    GeneratorService.RunGraphGenerator(
                        outputPath,
                        isEts2));

            OutputTextBox.AppendText(
                Environment.NewLine +
                Environment.NewLine +
                "Generator completed." +
                Environment.NewLine +
                Environment.NewLine +
                generatorResult);
        }
        catch (Exception ex)
        {
            OutputTextBox.AppendText(
                Environment.NewLine +
                Environment.NewLine +
                ex);
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
        }
        catch
        {
        }
    }
}