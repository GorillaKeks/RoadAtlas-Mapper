using System.IO;

namespace RoadAtlas.Mapper.Services;

public static class StatisticsService
{
    public static string GetFileSize(string file)
    {
        if (!File.Exists(file))
            return "-";

        long size =
            new FileInfo(file).Length;

        if (size >= 1024 * 1024)
        {
            return $"{size / 1024d / 1024d:N1} MB";
        }

        if (size >= 1024)
        {
            return $"{size / 1024d:N1} KB";
        }

        return $"{size:N0} B";
    }
}