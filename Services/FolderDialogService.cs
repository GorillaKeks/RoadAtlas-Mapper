using Ookii.Dialogs.Wpf;
using System.IO;

namespace RoadAtlas.Mapper.Services;

public static class FolderDialogService
{
    public static string? SelectFolder(
        string title,
        string? initialFolder = null)
    {
        try
        {
            var dialog = new VistaFolderBrowserDialog
            {
                Description = title,
                UseDescriptionForTitle = true
            };

            if (!string.IsNullOrWhiteSpace(initialFolder)
                && Directory.Exists(initialFolder))
            {
                dialog.SelectedPath = initialFolder;
            }

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                return dialog.SelectedPath;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public static string DetectETS2Path()
    {
        var candidates = new List<string>();

        foreach (var drive in DriveInfo.GetDrives())
        {
            if (!drive.IsReady)
                continue;

            candidates.Add(
                Path.Combine(
                    drive.RootDirectory.FullName,
                    "SteamLibrary",
                    "steamapps",
                    "common",
                    "Euro Truck Simulator 2"));

            candidates.Add(
                Path.Combine(
                    drive.RootDirectory.FullName,
                    "Program Files (x86)",
                    "Steam",
                    "steamapps",
                    "common",
                    "Euro Truck Simulator 2"));
        }

        foreach (var path in candidates)
        {
            if (Directory.Exists(path))
            {
                return path;
            }
        }

        return string.Empty;
    }

    public static string DetectATSPath()
    {
        var candidates = new List<string>();

        foreach (var drive in DriveInfo.GetDrives())
        {
            if (!drive.IsReady)
                continue;

            candidates.Add(
                Path.Combine(
                    drive.RootDirectory.FullName,
                    "SteamLibrary",
                    "steamapps",
                    "common",
                    "American Truck Simulator"));

            candidates.Add(
                Path.Combine(
                    drive.RootDirectory.FullName,
                    "Program Files (x86)",
                    "Steam",
                    "steamapps",
                    "common",
                    "American Truck Simulator"));
        }

        foreach (var path in candidates)
        {
            if (Directory.Exists(path))
            {
                return path;
            }
        }

        return string.Empty;
    }

    public static string DetectETS2ModFolder()
    {
        string path =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "Euro Truck Simulator 2",
                "mod");

        return Directory.Exists(path)
            ? path
            : string.Empty;
    }

    public static string DetectATSModFolder()
    {
        string path =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "American Truck Simulator",
                "mod");

        return Directory.Exists(path)
            ? path
            : string.Empty;
    }

    public static string DetectOutputFolder()
    {
        string path =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "RoadAtlas Mapper",
                "Output");

        Directory.CreateDirectory(path);

        return path;
    }
}