using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ETS2LA.Mapper.Views;

public partial class LogsView : UserControl
{
    public LogsView()
    {
        InitializeComponent();

        LoadLogs();
    }

    private void RefreshButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        LoadLogs();
    }

    private void LoadLogs()
    {
        try
        {
            string appData =
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData);

            string logDirectory =
                Path.Combine(
                    appData,
                    "ETS2LA Mapper",
                    "Logs");

            if (!Directory.Exists(logDirectory))
            {
                LogTextBox.Text =
                    "No log directory found.";

                return;
            }

            var newestLog =
                Directory.GetFiles(logDirectory, "log-*.log")
                    .OrderByDescending(File.GetLastWriteTime)
                    .FirstOrDefault();

            if (newestLog == null)
            {
                LogTextBox.Text =
                    "No log file found.";

                return;
            }

            using var stream =
                new FileStream(
                    newestLog,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite);

            using var reader =
                new StreamReader(stream);

            LogTextBox.Text =
                reader.ReadToEnd();

            LogTextBox.ScrollToEnd();
        }
        catch (Exception ex)
        {
            LogTextBox.Text =
                $"Error loading logs:{Environment.NewLine}{ex}";
        }
    }
}