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
    }

    //public event PropertyChangedEventHandler? PropertyChanged
    //{
    //    add
    //    {
    //        ((INotifyPropertyChanged)SubFolders).PropertyChanged += value;
    //    }

    //    remove
    //    {
    //        ((INotifyPropertyChanged)SubFolders).PropertyChanged -= value;
    //    }
    //}

    public Icons Icon { get; private set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public bool IsSelectable { get; set; }

    // making it nullable for the third selection
    public bool? IsSelected { get; set; } = null;
    public ObservableCollection<DirectoryViewModel> SubFolders { get; private set; }
    public DriveInfo DriveInfo { get; private set; }

    private static readonly string s_systemDrive;   
}
