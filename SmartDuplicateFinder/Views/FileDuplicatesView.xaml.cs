using SmartDuplicateFinder.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

        DataContext = this;

        Drives = new ObservableCollection<DriveViewModel>();

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
}
