using Veeam.FolderSync.Logging;

namespace Veeam.FolderSync;

internal static class Configuration
{
    internal static string SourceFolder { get; private set; } = null!;

    internal static string DestinationFolder { get; private set; } = null!;

    internal static string OutputPath { get; private set; } = null!;

    internal static ILogger Logger { get; set; } = null!;

    internal static int SyncIntervalSeconds { get; private set; }

    internal static void Initialize(string sourceFolder,
        string destinationFolder,
        string outputFolder,
        int interval)
    {
        SourceFolder = sourceFolder;
        DestinationFolder = destinationFolder;
        OutputPath = outputFolder;
        SyncIntervalSeconds = interval;
    }
}
