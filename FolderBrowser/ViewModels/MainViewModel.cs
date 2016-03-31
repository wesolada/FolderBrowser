namespace FolderBrowser.ViewModels
{
    using System.Collections.Generic;
    using System.IO;
    using Helpers;
    using Models;
    using MVVMClasses;

    public class MainViewModel : BaseViewModel
    {
        private string directory;
        private List<FileDescription> filesList;
        private List<FileDescription> filesSelectedList;

        public MainViewModel()
        {
            this.SaveCommand = new DelegateCommand(this.Save, this.CanSave);
        }

        public DelegateCommand SaveCommand { get; set; }

        public List<FileDescription> FilesList
        {
            get { return this.filesList; }
            set
            {
                if (this.filesList != value)
                {
                    this.filesList = value;
                    this.OnPropertyChanged(() => this.FilesList);
                }
            }
        }

        public string Directory
        {
            get { return this.directory; }
            set
            {
                if (this.directory != value)
                {
                    this.directory = value;
                    this.OnPropertyChanged(() => this.Directory);
                    this.StartWatcher();
                    this.LoadFileList();
                    this.SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public List<FileDescription> FilesSelectedList
        {
            get { return this.filesSelectedList; }
            set
            {
                if (this.filesSelectedList != value)
                {
                    this.filesSelectedList = value;
                    this.SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private void StartWatcher()
        {
            this.CloseWatcher();
            FolderListener.Run(this.directory);
            FolderListener.Watcher.Changed += this.Watcher_OnChanged;
            FolderListener.Watcher.Created += this.Watcher_OnChanged;
            FolderListener.Watcher.Deleted += this.Watcher_OnChanged;
            FolderListener.Watcher.Renamed += this.Watcher_OnRenamed;
        }

        private void CloseWatcher()
        {
            if (FolderListener.Watcher != null)
            {
                FolderListener.Watcher.Changed -= this.Watcher_OnChanged;
                FolderListener.Watcher.Created -= this.Watcher_OnChanged;
                FolderListener.Watcher.Deleted -= this.Watcher_OnChanged;
                FolderListener.Watcher.Renamed -= this.Watcher_OnRenamed;
                FolderListener.Close();
            }
        }

        private void Watcher_OnRenamed(object sender, RenamedEventArgs e)
        {
            this.LoadFileList();
        }

        private void Watcher_OnChanged(object sender, FileSystemEventArgs e)
        {
            this.LoadFileList();
        }

        private void Save(object parameter)
        {
            FolderHelper.SaveFile(this.FilesSelectedList, this.Directory);
        }

        private bool CanSave(object parameter)
        {
            return !string.IsNullOrEmpty(this.Directory)
                   && this.FilesSelectedList != null
                   && this.FilesSelectedList.Count > 0;
        }

        private void LoadFileList()
        {
            this.FilesList = FolderHelper.GetFilesList(this.Directory);
        }
    }
}