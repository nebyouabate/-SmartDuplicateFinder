﻿using PropertyChanged;
using SmartDuplicateFinder.Utils;
using System;
using System.Diagnostics;
using System.IO;

namespace SmartDuplicateFinder.ViewModel;

[AddINotifyPropertyChangedInterface]
[DebuggerDisplay("{DisplayName}")]
public class DriveViewModel : DirectoryViewModel
{
    static DriveViewModel() => s_systemDrive = Path.GetPathRoot(Environment.SystemDirectory)!;

    public DriveViewModel(DriveInfo driveInfo) :
        base(driveInfo.RootDirectory, null!)
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
        //SubFolders.CollectionChanged += SubFolders_CollectionChanged;
    }

    public DriveInfo DriveInfo { get; private set; }

    private static readonly string s_systemDrive;
}
