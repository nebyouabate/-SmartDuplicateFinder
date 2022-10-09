using SmartDuplicateFinder.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace SmartDuplicateFinder.ViewModel;

public class DirectoryViewModel : INotifyPropertyChanged
{

    public static readonly DirectoryViewModel UnExpanded = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    internal DirectoryViewModel(DirectoryInfo directoryInfo) 
        : this()
    {
        DirectoryInfo = directoryInfo;

        DisplayName = DirectoryInfo.Name;
        Name = DirectoryInfo.Name;
    }

    private DirectoryViewModel()
    {
        DirectoryInfo = null!;
        
        DisplayName = "Loading ...";
        Name = "";

        IsSelectable = true;
        IsSelected = false;
        IsExpanded = false;
        Icon = Icons.OpenFolder;

        SubFolders = new ObservableCollection<DirectoryViewModel> { UnExpanded };
    }

    public Icons Icon { get; private set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }    
    public bool IsSelectable { get; set; }
    public bool IsSelected { get; set; }
    public bool IsExpanded { get; set; }
    public DirectoryInfo DirectoryInfo { get; protected set; }
    public ObservableCollection<DirectoryViewModel> SubFolders { get; protected set; }
}

