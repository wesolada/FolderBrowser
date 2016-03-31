namespace FolderBrowser.Helpers
{
    using System.IO;
    using System.Security.Permissions;

    public static class FolderListener
    {
        public static FileSystemWatcher Watcher { get; private set; }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Run(string directory)
        {
            Watcher = new FileSystemWatcher
            {
                Path = directory,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                EnableRaisingEvents = true
            };
        }

        public static void Close()
        {
            Watcher = null;
        }
    }
}