using System.IO;

namespace ETS2LA.Mapper.Services;

public static class PathDetectionService
{
    public static string GetETS2Path()
    {
        string path =
            @"C:\Program Files (x86)\Steam\steamapps\common\Euro Truck Simulator 2";

        return Directory.Exists(path)
            ? path
            : string.Empty;
    }

    public static string GetATSPath()
    {
        string path =
            @"C:\Program Files (x86)\Steam\steamapps\common\American Truck Simulator";

        return Directory.Exists(path)
            ? path
            : string.Empty;
    }

    public static string GetETS2ModPath()
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

    public static string GetATSModPath()
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

    public static string GetOutputPath()
    {
        string path =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments),
                "ETS2LA Mapper",
                "Output");

        Directory.CreateDirectory(path);

        return path;
    }
}