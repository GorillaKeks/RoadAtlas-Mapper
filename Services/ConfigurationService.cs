using Newtonsoft.Json;
using RoadAtlas.Mapper.Models;
using System.IO;

namespace RoadAtlas.Mapper.Services;

public static class ConfigurationService
{
    private static readonly string AppFolder =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "RoadAtlas Mapper");

    private static readonly string SettingsFile =
        Path.Combine(AppFolder, "settings.json");

    public static AppSettings Load()
    {
        try
        {
            if (!Directory.Exists(AppFolder))
            {
                Directory.CreateDirectory(AppFolder);
            }

            if (!File.Exists(SettingsFile))
            {
                var settings = new AppSettings();
                Save(settings);
                return settings;
            }

            var json = File.ReadAllText(SettingsFile);

            var settingsObject =
                JsonConvert.DeserializeObject<AppSettings>(json);

            return settingsObject ?? new AppSettings();
        }
        catch
        {
            return new AppSettings();
        }
    }

    public static void Save(AppSettings settings)
    {
        if (!Directory.Exists(AppFolder))
        {
            Directory.CreateDirectory(AppFolder);
        }

        var json = JsonConvert.SerializeObject(
            settings,
            Formatting.Indented);

        File.WriteAllText(SettingsFile, json);
    }
}