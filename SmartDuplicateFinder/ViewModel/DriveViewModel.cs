using PropertyChanged;
using SmartDuplicateFinder.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace SmartDuplicateFinder.ViewModel;

[AddINotifyPropertyChangedInterface]
[DebuggerDisplay("{DisplayName}")]
public class DriveViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    static DriveViewModel()
    {
        s_systemDrive = Path.GetPathRoot(Environment.SystemDirectory)!;
    }

    public DriveViewModel(DriveInfo driveInfo)
    {
        DriveInfo = driveInfo;

        Icon = DriveInfo.DriveType switch
        {
            DriveType.Fixed => DriveInfo.Name.Equals(s_systemDrive, StringComparison.InvariantCultureIgnoreCase) ? Icons.WindowsDrive : Icons.HardDrive,
            DriveType.Network => DriveInfo.IsReady ? Icons.NetworkConnectedDrive : Icons.NetworkDisconnectedDrive,
            DriveType.CDRom => Icons.CDRomDrive,
            DriveType.Removable => Icons.RemovableDrive,
            _ => Icons.UnknownDrive
        };

        if (DriveInfo.IsReady)
        {
            IsSelectable = true;

            DisplayName = $"{DriveInfo.VolumeLabel} ({DriveInfo.Name[..^1]})";
            Name = DriveInfo.Name[..^1];
        }
        else
        {
            IsSelectable = false;

            DisplayName = Name = $"({DriveInfo.Name[..^1]})";
        }

        SubFolders = new ObservableCollection<DirectoryViewModel>
        {
            DirectoryViewModel.UnExpanded
        };

        SubFolders.CollectionChanged += SubFolders_CollectionChanged;
    }

    private void SubFolders_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (INotifyPropertyChanged item in e.OldItems)
            {
                item.PropertyChanged -= SubFolderPropertyChanged;
            }
        }

        if (e.NewItems is not null)
        {
            foreach (INotifyPropertyChanged item in e.NewItems)
            {
                item.PropertyChanged += SubFolderPropertyChanged;
            }
        }
    }

    private void SubFolderPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not null)
        {
            var me = (DirectoryViewModel)sender;

            foreach (var item in me.SubFolders)
            {
                item.IsSelected = me.IsSelected;
            }

            IsSelected = me.IsSelected;
        }
    }

    public Icons Icon { get; private set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public bool IsSelectable { get; set; }

    // making it nullable for the third selection
    public bool? IsSelected { get; set; } = false;
    public ObservableCollection<DirectoryViewModel> SubFolders { get; private set; }
    public DriveInfo DriveInfo { get; private set; }

    private static readonly string s_systemDrive;
}
