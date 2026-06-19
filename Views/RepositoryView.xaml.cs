using System.Windows;
using System.Windows.Controls;
using ETS2LA.Mapper.Services;

namespace ETS2LA.Mapper.Views;

public partial class RepositoryView : UserControl
{
    public RepositoryView()
    {
        InitializeComponent();

        RefreshStatus();
    }

    private void RefreshButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        RefreshStatus();
    }

    private void BuildParserButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        OutputTextBox.Text =
            "Building parser..." +
            Environment.NewLine;

        string result =
            NodeService.BuildParser();

        OutputTextBox.Text = result;

        LoggingService.Info(
            "Parser build executed.");

        RefreshStatus();
    }

    private void RefreshStatus()
    {
        try
        {
            StatusTextBlock.Text =
                RepositoryService.RepositoryExists()
                    ? "✓ Repository Found"
                    : "✗ Repository Missing";

            BranchTextBlock.Text =
                RepositoryService.GetCurrentBranch();

            CommitTextBlock.Text =
                RepositoryService.GetLatestCommit();

            NodeTextBlock.Text =
                NodeService.IsNodeInstalled()
                    ? $"✓ {NodeService.GetNodeVersion()}"
                    : "✗ Node.js not found";

            DependenciesTextBlock.Text =
                NodeService.AreDependenciesInstalled()
                    ? "✓ Installed"
                    : "✗ Missing";

            ParserTextBlock.Text =
                NodeService.IsParserBuilt()
                    ? "✓ Built"
                    : "✗ Not Built";
        }
        catch (Exception ex)
        {
            OutputTextBox.Text =
                ex.ToString();

            LoggingService.Error(
                $"RepositoryView error: {ex.Message}");
        }
    }
}