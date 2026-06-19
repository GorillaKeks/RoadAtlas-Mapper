using System.Diagnostics;

namespace ETS2LA.Mapper.Services;

public static class WslService
{
    public static List<string> GetDistributions()
    {
        try
        {
            var process = new Process();

            process.StartInfo.FileName = "wsl.exe";
            process.StartInfo.Arguments = "-l -q";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            var distributions = output
                .Split(
                    Environment.NewLine,
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (distributions.Count == 0)
            {
                distributions.Add("Ubuntu");
            }

            return distributions;
        }
        catch
        {
            return new List<string>
            {
                "Ubuntu"
            };
        }
    }
}