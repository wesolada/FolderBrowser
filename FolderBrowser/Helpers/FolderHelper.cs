namespace FolderBrowser.Helpers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Microsoft.WindowsAPICodePack.Dialogs;
    using Models;
    using Properties;

    public static class FolderHelper
    {
        public static List<FileDescription> GetFilesList(string directory)
        {
            if (Directory.Exists(directory))
            {
                string[] list = Directory.GetFiles(directory);

                return list.Select(file => new FileInfo(file))
                    .Select(fileInfo => new FileDescription
                    {
                        Name = fileInfo.Name,
                        Size = fileInfo.Length,
                        Date = fileInfo.CreationTime
                    }).ToList();
            }

            return null;
        }

        public static void SaveFile(List<FileDescription> selectedFiles, string directory)
        {
            string fileName = GetFileName(directory);

            if (!string.IsNullOrEmpty(fileName))
            {
                var xmlSerializer = new XmlSerializer(typeof (List<FileDescription>));
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    xmlSerializer.Serialize(fileStream, selectedFiles);
                }
            }
        }

        private static string GetFileName(string directory)
        {
            var dlg = new CommonOpenFileDialog
            {
                Title = Resources.FolderHelper_EnterFileName,
                IsFolderPicker = false,
                InitialDirectory = directory,
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = directory,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                DefaultExtension = "xml"
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dlg.FileName;
            }

            return string.Empty;
        }
    }
}