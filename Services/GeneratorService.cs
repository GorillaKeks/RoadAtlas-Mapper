using ETS2LA.Mapper.Services;

namespace ETS2LA.Mapper.Services;

public static class GeneratorService
{
    public static string RunGraphGenerator(
        string outputPath,
        bool isEts2)
    {
        string outputPathWsl =
            ConvertToWslPath(outputPath);

        string map =
            isEts2
                ? "europe"
                : "usa";

        string command =
            "cd ~/maps && " +
            "export NODE_OPTIONS='--max-old-space-size=32768' && " +
            "npx generator graph " +
            $"-m {map} " +
            $"-i '{outputPathWsl}' " +
            $"-o '{outputPathWsl}'";

        LoggingService.Info(
            $"Running generator: {command}");

        return WslCommandService.ExecuteCommand(command);
    }

    private static string ConvertToWslPath(
        string windowsPath)
    {
        if (string.IsNullOrWhiteSpace(windowsPath))
            return string.Empty;

        string path =
            windowsPath.Replace("\\", "/");

        if (path.Length > 1 &&
            path[1] == ':')
        {
            string drive =
                path[..1].ToLower();

            path =
                "/mnt/" +
                drive +
                path[2..];
        }

        return path;
    }
}