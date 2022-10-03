using SmartDuplicateFinder.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SmartDuplicateFinder.Views;

/// <summary>
/// Interaction logic for FileDuplicatesView.xaml
/// </summary>
public partial class FileDuplicatesView : UserControl
{
    public FileDuplicatesView()
    {
        InitializeComponent();
        AddCommandBindings();


        Drives = new ObservableCollection<DriveViewModel>();

        OnRefreshDrives();

        DataContext = this;
    }

    private void TreeViewItem_OnExpanded(object sender, RoutedEventArgs e)
    {
        var treeViewItem = (TreeViewItem)e.OriginalSource;

        if (treeViewItem.DataContext is DirectoryViewModel parent)
        {
            if (!(parent.SubFolders.Count == 1 && parent.SubFolders[0] == DirectoryViewModel.UnExpanded))
                return;

            parent.SubFolders.Clear();

            var options = new EnumerationOptions();
            foreach (var directoryInfo in parent.DirectoryInfo.GetDirectories("*", options))
            {
                parent.SubFolders.Add(new DirectoryViewModel(directoryInfo));
            }
        }
        else if (treeViewItem.DataContext is DriveViewModel drive)
        {
            if (!(drive.SubFolders.Count == 1 && drive.SubFolders[0] == DirectoryViewModel.UnExpanded))
                return;

            drive.SubFolders.Clear();

            var options = new EnumerationOptions();
            foreach (var directoryInfo in drive.DriveInfo.RootDirectory.GetDirectories("*", options))
            {
                drive.SubFolders.Add(new DirectoryViewModel(directoryInfo));
            }
        }
    }
    private void OnRefreshDrives()
    {
        Drives.Clear();

        var drives = DriveInfo.GetDrives().Where(d => d.IsReady).Select(d => new DriveViewModel(d));
        foreach (var drive in drives)
        {
            Drives.Add(drive);
        }
    }

    public ObservableCollection<DriveViewModel> Drives { get; set; }
    private void AddCommandBindings()
    {
        // CommandBindings.Add(new CommandBinding(AppCommands.Xxxxxxx, (sender, args) => Onxxxxxx(args)));
        // CommandBindings.Add(new CommandBinding(AppCommands.Xxxxxxx, (sender, args) => Onxxxxxx()));
    }

    private void TreeView_Expanded(object sender, System.Windows.RoutedEventArgs e)
    {

    }
}
