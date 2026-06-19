using System.Diagnostics;
using System.Text;

namespace RoadAtlas.Mapper.Services;

public static class WslCommandService
{
    public static string ExecuteCommand(string command)
    {
        try
        {
            var process = new Process();

            process.StartInfo.FileName = "wsl.exe";

            string escapedCommand =
            command.Replace("\"", "\\\"");

            process.StartInfo.Arguments =
                $"bash -ic \"{escapedCommand}\"";

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();

            StringBuilder output =
                new();

            output.AppendLine(
                process.StandardOutput.ReadToEnd());

            string error =
                process.StandardError.ReadToEnd();

            if (!string.IsNullOrWhiteSpace(error))
            {
                output.AppendLine(error);
            }

            process.WaitForExit();

            return output.ToString();
        }
        catch (Exception ex)
        {
            return $"Error: {ex}";
        }
    }
}