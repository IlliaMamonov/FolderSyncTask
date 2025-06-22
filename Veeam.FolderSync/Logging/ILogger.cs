using System.Runtime.CompilerServices;

namespace Veeam.FolderSync.Logging;

internal interface ILogger
{
    void Info(string message, [CallerMemberName] string callerName = "");
}
