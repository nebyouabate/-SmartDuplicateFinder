using SmartDuplicateFinder.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        var parent = (DirectoryViewModel)treeViewItem.DataContext;

        parent.LoadSubFolders();
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
        CommandBindings.Add(new CommandBinding(AppCommands.Refresh, (sender, args) => OnRefreshDrives()));
        // CommandBindings.Add(new CommandBinding(AppCommands.Xxxxxxx, (sender, args) => Onxxxxxx()));
    }
}
