using ETS2LA.Mapper.Services;

namespace ETS2LA.Mapper.Services;

public static class ParserService
{
    public static string RunParser(
        string gamePath,
        string modPath,
        string outputPath)
    {
        string gamePathWsl =
            ConvertToWslPath(gamePath);

        string modPathWsl =
            ConvertToWslPath(modPath);

        string outputPathWsl =
            ConvertToWslPath(outputPath);

        string command =
    "cd ~/maps && " +
    "export NODE_OPTIONS='--max-old-space-size=32768' && " +
    "./node_modules/.bin/tsx " +
    "packages/clis/parser/index.ts " +
    $"-g '{gamePathWsl}' ";

        if (!string.IsNullOrWhiteSpace(modPathWsl))
        {
            command +=
                $"-m '{modPathWsl}' ";
        }

        command +=
            $"-o '{outputPathWsl}'";

        LoggingService.Info(
            $"Running parser: {command}");

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