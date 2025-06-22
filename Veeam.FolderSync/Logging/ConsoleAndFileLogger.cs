using System.Runtime.CompilerServices;

namespace Veeam.FolderSync.Logging;

internal class ConsoleAndFileLogger : ILogger, IDisposable
{
    private readonly string _outputPath;

    private readonly StreamWriter _outputWriter;

    private static string TimeFormat { get; } = "yyyy-MM-ddTHH:mm:ss-fffffff";


    public ConsoleAndFileLogger()
    {
        _outputPath = Configuration.OutputPath;

        var stream = Directory.CreateDirectory(Path.GetDirectoryName(_outputPath)!);
        _outputWriter = new StreamWriter(_outputPath) { AutoFlush = true };
    }

    public void Info(string message, [CallerMemberName] string callerName = "Unspecified source")
    {
        var ts = DateTime.UtcNow;
        var messageToLog = $"[{ts.ToString(TimeFormat)}][{callerName}] - {message}";
        Console.WriteLine(messageToLog);
        _outputWriter.WriteLine(messageToLog);
    }

    public void Dispose()
    {
        _outputWriter?.Dispose();
    }
}
