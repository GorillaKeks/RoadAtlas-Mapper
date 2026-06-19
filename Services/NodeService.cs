using ETS2LA.Mapper.Services;

namespace ETS2LA.Mapper.Services;

public static class NodeService
{
    public static bool IsNodeInstalled()
    {
        string result =
            WslCommandService.ExecuteCommand(
                "node --version");

        return result.Contains("v");
    }

    public static string GetNodeVersion()
    {
        return WslCommandService.ExecuteCommand(
            "node --version").Trim();
    }

    public static bool IsCorepackAvailable()
    {
        string result =
            WslCommandService.ExecuteCommand(
                "corepack --version");

        return !string.IsNullOrWhiteSpace(result);
    }

    public static bool AreDependenciesInstalled()
    {
        string result =
            WslCommandService.ExecuteCommand(
                "test -d ~/maps/node_modules && echo OK");

        return result.Contains("OK");
    }

    public static bool IsParserBuilt()
    {
        string result =
            WslCommandService.ExecuteCommand(
                "test -f ~/maps/packages/clis/parser/build/Release/parser.node && echo OK");

        return result.Contains("OK");
    }

    public static string BuildParser()
    {
        return WslCommandService.ExecuteCommand(
            "cd ~/maps && corepack npm run build -w packages/clis/parser");
    }
}