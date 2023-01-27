using SmartDuplicateFinder.ViewModel;
using System;
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

        Drives = null!;
        Init();

        OnRefreshDrives();

        DataContext = this;
    }

    public ObservableCollection<DriveViewModel> Drives { get; set; }

    private void Init() => Drives = new ObservableCollection<DriveViewModel>();

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

    private void WalkTree(Func<DirectoryViewModel, bool> process, Action<DirectoryViewModel> action)
    {
        var treeNodes = new Stack<DirectoryViewModel>();

        foreach (DriveViewModel drive in Drives.Reverse())
        {
            if (process(drive))
            {
                treeNodes.Push(drive);
            }
        }

        while (treeNodes.TryPop(out var folder))
        {
            foreach (DirectoryViewModel item in folder.SubFolders.Reverse())
            {
                if (process(item))
                {
                    treeNodes.Push(item);
                }
            }

            action(folder);
        }
    }

    private void OnClearAll()
    {
        WalkTree(d => d.IsSelected != false, d => d.IsSelected = false);
    }

    private void AddCommandBindings()
    {
        CommandBindings.Add(new CommandBinding(AppCommands.Refresh, (sender, args) => OnRefreshDrives()));
        CommandBindings.Add(new CommandBinding(AppCommands.ClearAll, (sender, args) => OnClearAll()));

        // CommandBindings.Add(new CommandBinding(AppCommands.Xxxxxxx, (sender, args) => Onxxxxxx()));
    }


}
