using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace RoadAtlas.Mapper.Services;

public static class UpdateService
{
    private const string LatestReleaseUrl =
        "https://api.github.com/repos/GorillaKeks/RoadAtlas-Mapper/releases/latest";

    public static string GetCurrentVersion()
    {
        return Assembly
            .GetExecutingAssembly()
            .GetName()
            .Version?
            .ToString(3)
            ?? "0.0.0";
    }

    public static async Task<string?> GetLatestVersionAsync()
    {
        try
        {
            using HttpClient client = new();

            client.DefaultRequestHeaders.Add(
                "User-Agent",
                "RoadAtlas-Mapper");

            string json =
                await client.GetStringAsync(
                    LatestReleaseUrl);

            JObject release =
                JObject.Parse(json);

            return release["tag_name"]?
                .ToString()
                .Replace("v", "");
        }
        catch
        {
            return null;
        }
    }

    public static async Task<bool> UpdateAvailableAsync()
    {
        try
        {
            string currentVersion =
                GetCurrentVersion();

            string? latestVersion =
                await GetLatestVersionAsync();

            if (string.IsNullOrWhiteSpace(latestVersion))
            {
                return false;
            }

            Version current =
                Version.Parse(currentVersion);

            Version latest =
                Version.Parse(latestVersion);

            return latest > current;
        }
        catch
        {
            return false;
        }
    }

    public static async Task<string?> GetDownloadUrlAsync()
    {
        try
        {
            using HttpClient client = new();

            client.DefaultRequestHeaders.Add(
                "User-Agent",
                "RoadAtlas-Mapper");

            string json =
                await client.GetStringAsync(
                    LatestReleaseUrl);

            JObject release =
                JObject.Parse(json);

            JArray? assets =
                release["assets"] as JArray;

            if (assets == null || assets.Count == 0)
            {
                return null;
            }

            return assets[0]?["browser_download_url"]
                ?.ToString();
        }
        catch
        {
            return null;
        }
    }
}