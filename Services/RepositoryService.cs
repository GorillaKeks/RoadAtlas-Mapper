using ETS2LA.Mapper.Models;

namespace ETS2LA.Mapper.Services;

public static class RepositoryService
{
    public const string RepositoryUrl =
        "https://github.com/ETS2LA/maps";

    public static bool RepositoryExists()
    {
        string result =
            WslCommandService.ExecuteCommand(
                "test -d ~/maps/.git && echo FOUND");

        return result.Contains("FOUND");
    }

    public static string GetStatus()
    {
        return RepositoryExists()
            ? "Repository Found"
            : "Repository Missing";
    }

    public static string CloneRepository()
    {
        return WslCommandService.ExecuteCommand(
            "mkdir -p ~/maps && " +
            "cd ~/maps && " +
            "git clone --recurse-submodules https://github.com/ETS2LA/maps .");
    }

    public static string UpdateRepository()
    {
        return WslCommandService.ExecuteCommand(
            "cd ~/maps && " +
            "git pull && " +
            "git submodule update --init --recursive");
    }

    public static string GetCurrentBranch()
    {
        if (!RepositoryExists())
        {
            return "Repository Missing";
        }

        return WslCommandService.ExecuteCommand(
            "cd ~/maps && git branch --show-current");
    }

    public static string GetLatestCommit()
    {
        if (!RepositoryExists())
        {
            return "Repository Missing";
        }

        return WslCommandService.ExecuteCommand(
            "cd ~/maps && git rev-parse --short HEAD");
    }
}