using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDuplicateFinder.ViewModel;

public class DriveViewModel
{
    public DriveViewModel(DriveInfo driveInfo)
    {
        DriveInfo = driveInfo;
        Name = driveInfo.Name[..^1];
    }

    public string Name { get; set; }    
    public bool IsSelected { get; set; }    
    public DriveInfo DriveInfo { get; private set; }
}
