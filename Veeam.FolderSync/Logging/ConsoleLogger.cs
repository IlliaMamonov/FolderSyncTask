using System.Runtime.CompilerServices;

namespace Veeam.FolderSync.Logging;

internal class ConsoleLogger : ILogger
{
    private static string TimeFormat { get; } = "yyyy-MM-ddTHH:mm:ss-fffffff";

    public void Info(string message, [CallerMemberName] string callerName = "Unspecified Source")
    {
        var ts = DateTime.UtcNow;
        Console.WriteLine($"[{ts.ToString(TimeFormat)}][{callerName}] - {message}");
    }
}
