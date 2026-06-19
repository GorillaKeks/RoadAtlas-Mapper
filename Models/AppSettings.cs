namespace ETS2LA.Mapper.Models;

public class AppSettings
{
    public string ETS2Path { get; set; } = string.Empty;

    public string ATSPath { get; set; } = string.Empty;

    public string ETS2ModPath { get; set; } = string.Empty;

    public string ATSModPath { get; set; } = string.Empty;

    public string OutputPath { get; set; } = string.Empty;

    public string WslDistribution { get; set; } = "Ubuntu";

    public string RepositoryPath { get; set; } = "~/maps";
}