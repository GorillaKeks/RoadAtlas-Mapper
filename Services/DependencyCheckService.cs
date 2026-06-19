using RoadAtlas.Mapper.Models;
using System.Diagnostics;
using System.IO;

namespace RoadAtlas.Mapper.Services;

public static class DependencyCheckService
{
    public static List<string> RunChecks(AppSettings settings)
    {
        var results = new List<string>();

        results.Add(CheckWsl());
        results.Add(CheckWslDistribution());
        results.Add(CheckPython());
        results.Add(CheckPip());
        results.Add(CheckGit());
        results.Add(CheckETS2(settings));
        results.Add(CheckATS(settings));
        results.Add(CheckETS2ModFolder(settings));
        results.Add(CheckATSModFolder(settings));
        results.Add(CheckOutputFolder(settings));

        return results;
    }

    private static string CheckWsl()
    {
        try
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "wsl.exe",
                Arguments = "--version",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            process?.WaitForExit();

            return "✓ WSL Installed";
        }
        catch
        {
            return "✗ WSL Not Installed";
        }
    }

    private static string CheckWslDistribution()
    {
        try
        {
            var distributions = WslService.GetDistributions();

            return distributions.Count > 0
                ? "✓ WSL Distribution Found"
                : "✗ No WSL Distribution Found";
        }
        catch
        {
            return "✗ No WSL Distribution Found";
        }
    }

    private static string CheckPython()
    {
        return ExecuteWslCheck(
            "python3 --version",
            "✓ Python Installed",
            "✗ Python Missing");
    }

    private static string CheckPip()
    {
        return ExecuteWslCheck(
            "pip3 --version",
            "✓ Pip Installed",
            "✗ Pip Missing");
    }

    private static string CheckGit()
    {
        return ExecuteWslCheck(
            "git --version",
            "✓ Git Installed",
            "✗ Git Missing");
    }

    private static string ExecuteWslCheck(
        string command,
        string success,
        string failure)
    {
        try
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "wsl.exe",
                Arguments = command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            process?.WaitForExit();

            return process?.ExitCode == 0
                ? success
                : failure;
        }
        catch
        {
            return failure;
        }
    }

    private static string CheckETS2(AppSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.ETS2Path))
            return "✗ ETS2 Path Not Set";

        bool baseScs =
            File.Exists(Path.Combine(settings.ETS2Path, "base.scs"));

        bool defScs =
            File.Exists(Path.Combine(settings.ETS2Path, "def.scs"));

        return baseScs && defScs
            ? "✓ ETS2 Installation Found"
            : "✗ ETS2 Installation Invalid";
    }

    private static string CheckATS(AppSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.ATSPath))
            return "✗ ATS Path Not Set";

        bool baseScs =
            File.Exists(Path.Combine(settings.ATSPath, "base.scs"));

        bool defScs =
            File.Exists(Path.Combine(settings.ATSPath, "def.scs"));

        return baseScs && defScs
            ? "✓ ATS Installation Found"
            : "✗ ATS Installation Invalid";
    }

    private static string CheckETS2ModFolder(AppSettings settings)
    {
        return Directory.Exists(settings.ETS2ModPath)
            ? "✓ ETS2 Mod Folder Found"
            : "✗ ETS2 Mod Folder Missing";
    }

    private static string CheckATSModFolder(AppSettings settings)
    {
        return Directory.Exists(settings.ATSModPath)
            ? "✓ ATS Mod Folder Found"
            : "✗ ATS Mod Folder Missing";
    }

    private static string CheckOutputFolder(AppSettings settings)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(settings.OutputPath))
                return "✗ Output Folder Not Set";

            Directory.CreateDirectory(settings.OutputPath);

            string testFile =
                Path.Combine(settings.OutputPath, "write_test.tmp");

            File.WriteAllText(testFile, "test");

            File.Delete(testFile);

            return "✓ Output Folder Accessible";
        }
        catch
        {
            return "✗ Output Folder Not Writable";
        }
    }
}