using Veeam.FolderSync.Logging;

namespace Veeam.FolderSync;

internal class FolderSync()
{
    internal static void Main(string[] args)
    {
        var sourceFolder = args[0];
        var destinationFolder = args[1];
        var outputFile  = args[2];
        var interval = int.Parse(args[3]);

        Console.WriteLine("Arguments provided:");
        Console.WriteLine($"Source: {sourceFolder}");
        Console.WriteLine($"Destination: {destinationFolder}");
        Console.WriteLine($"Output file: {outputFile}");
        Console.WriteLine($"Interval in seconds: {interval}");
        Console.WriteLine("Proceed? (y/n)");

        var key = Console.ReadKey();

        if (char.ToLower(key.KeyChar) != 'y')
        {
            return;
        }

        Configuration.Initialize(sourceFolder, destinationFolder, outputFile, interval);
        using var logger = new ConsoleAndFileLogger();
        Configuration.Logger = logger;

        Jobs.StartFolderSyncJob();

        Console.WriteLine("\nFolder sync job started. Press Enter to exit.");
        Console.ReadLine();
    }
}