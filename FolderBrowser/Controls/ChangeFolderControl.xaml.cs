namespace FolderBrowser.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.WindowsAPICodePack.Dialogs;

    public partial class ChangeFolderControl : UserControl
    {
        public static readonly DependencyProperty DirectoryProperty =
            DependencyProperty.Register("Directory", typeof (string), typeof (ChangeFolderControl),
                new FrameworkPropertyMetadata
                {
                    DefaultValue = string.Empty,
                    BindsTwoWayByDefault = true
                });

        public ChangeFolderControl()
        {
            this.InitializeComponent();
        }

        public string Directory
        {
            get { return (string)this.GetValue(DirectoryProperty); }
            set { this.SetValue(DirectoryProperty, value); }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog
            {
                Title = Properties.Resources.ChangeFolderControl_ChangeFolder,
                IsFolderPicker = true,
                InitialDirectory = Environment.CurrentDirectory,
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = Environment.CurrentDirectory,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.Directory = dlg.FileName;
            }
        }
    }
}