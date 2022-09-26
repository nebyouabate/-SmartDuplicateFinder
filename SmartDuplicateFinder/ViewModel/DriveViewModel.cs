using PropertyChanged;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace SmartDuplicateFinder.ViewModel;

[AddINotifyPropertyChangedInterface]
[DebuggerDisplay("{DisplayName}")]
public class DriveViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public DriveViewModel(DriveInfo driveInfo)
    {
        DriveInfo = driveInfo;       
        Name = driveInfo.Name[..^1];
        DisplayName = $"{driveInfo.VolumeLabel} ({Name})";
    }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    public bool IsSelected { get; set; }    

    public DriveInfo DriveInfo { get; private set; }   
}
