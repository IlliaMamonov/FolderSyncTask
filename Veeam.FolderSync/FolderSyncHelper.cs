using System.Security.Cryptography;
using Veeam.FolderSync.Logging;

namespace Veeam.FolderSync;

internal class FolderSyncHelper(ILogger logger)
{
    private readonly ILogger _logger = logger;

    internal void SyncFolders(string source, string destination)
    {
        foreach (var sourceFile in Directory.GetFiles(source, "*.*", SearchOption.TopDirectoryOnly))
        {
            var targetFile = sourceFile.Replace(source, destination);

            if (!File.Exists(targetFile))
            {
                File.Copy(sourceFile, targetFile);
                _logger.Info($"Created new File '{sourceFile}'");
            }
            else if (GetMD5(sourceFile) != GetMD5(targetFile))
            {
                File.Copy(sourceFile, targetFile, true);
                _logger.Info($"Replaced an existing File {targetFile}");
            }
        }

        foreach (var destinationFile in Directory.GetFiles(destination, "*.*", SearchOption.TopDirectoryOnly))
        {
            var sourceFile = destinationFile.Replace(destination, source);

            if (File.Exists(sourceFile))
            {
                continue;
            }

            File.Delete(destinationFile);
            _logger.Info($"Deleted File {destinationFile}");
        }

        foreach (var destinationDirectory in Directory.GetDirectories(destination, "*", SearchOption.TopDirectoryOnly))
        {
            var sourceDirectory = destinationDirectory.Replace(destination, source);

            if (Directory.Exists(sourceDirectory))
            {
                continue;
            }

            Directory.Delete(destinationDirectory, true);
            _logger.Info($"Deleted Directory {destinationDirectory}");
        }

        foreach (var sourceDirectory in Directory.GetDirectories(source, "*", SearchOption.TopDirectoryOnly))
        {
            var targetDirectory = sourceDirectory.Replace(source, destination);

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
                _logger.Info($"Created Directory '{targetDirectory}'");
            }

            SyncFolders(sourceDirectory, targetDirectory);
        }
    }


    private static string GetMD5(string filePath)
    {
        using var algo = MD5.Create();
        using var stream = File.OpenRead(filePath);
        var hash = algo.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}
