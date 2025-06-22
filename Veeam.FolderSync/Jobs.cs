using Veeam.FolderSync.Logging;
using Timer = System.Timers.Timer;

namespace Veeam.FolderSync;

static class Jobs
{
    internal static void StartFolderSyncJob()
    {
        var timer = new Timer(TimeSpan.FromSeconds(Configuration.SyncIntervalSeconds));

        static void job()
        {
            var folderSyncHelper = new FolderSyncHelper(Configuration.Logger);
            folderSyncHelper.SyncFolders(Configuration.SourceFolder, Configuration.DestinationFolder);
        }

        timer.Elapsed += (s, e) => job();

        timer.Start();
    }
}
