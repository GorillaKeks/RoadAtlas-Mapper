using System.Collections.Generic;
using System.IO;

namespace RoadAtlas.Mapper.Services;

public static class DlcDetectionService
{
    private static readonly Dictionary<string, string> MapDlcs =
        new()
        {
            // ETS2
            { "dlc_east", "Going East!" },
            { "dlc_north", "Scandinavia" },
            { "dlc_fr", "Vive la France!" },
            { "dlc_it", "Italia" },
            { "dlc_balt", "Beyond the Baltic Sea" },
            { "dlc_black_sea", "Road to the Black Sea" },
            { "dlc_iberia", "Iberia" },
            { "dlc_balkan_e", "West Balkans" },

            // ATS
            { "dlc_ar", "Arizona" },
            { "dlc_nm", "New Mexico" },
            { "dlc_or", "Oregon" },
            { "dlc_wa", "Washington" },
            { "dlc_ut", "Utah" },
            { "dlc_id", "Idaho" },
            { "dlc_co", "Colorado" },
            { "dlc_wy", "Wyoming" },
            { "dlc_tx", "Texas" },
            { "dlc_ok", "Oklahoma" },
            { "dlc_ks", "Kansas" },
            { "dlc_ne", "Nebraska" },
            { "dlc_mo", "Missouri" },
            { "dlc_arizona", "Arizona" }
        };

    public static List<string> GetInstalledDlcs(
        string gamePath)
    {
        List<string> dlcs = new();

        if (string.IsNullOrWhiteSpace(gamePath))
            return dlcs;

        if (!Directory.Exists(gamePath))
            return dlcs;

        foreach (string file in Directory.GetFiles(
                     gamePath,
                     "dlc_*.scs",
                     SearchOption.TopDirectoryOnly))
        {
            string name =
                Path.GetFileNameWithoutExtension(file);

            if (MapDlcs.TryGetValue(
                    name,
                    out string? displayName))
            {
                dlcs.Add(displayName);
            }
        }

        dlcs.Sort();

        return dlcs;
    }
}