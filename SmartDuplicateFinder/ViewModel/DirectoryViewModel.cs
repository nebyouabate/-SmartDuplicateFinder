using SmartDuplicateFinder.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace SmartDuplicateFinder.ViewModel;

public class DirectoryViewModel : INotifyPropertyChanged
{

    public static readonly DirectoryViewModel UnExpanded = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    internal DirectoryViewModel(DirectoryInfo directoryInfo, DirectoryViewModel parent)
        : this()
    {
        _directoryInfo = directoryInfo;
        _parent = parent;

        DisplayName = _directoryInfo.Name;
        Name = _directoryInfo.Name;

        if (HasSubfolders())
        {
            SubFolders.Add(UnExpanded);
        }
    }

    private DirectoryViewModel()
    {
        _directoryInfo = null!;
        DisplayName = "Loading ...";
        Name = "";
        Icon = Icons.OpenFolder;

        SubFolders = new ObservableCollection<DirectoryViewModel>();

        IsSelectable = true;
        IsSelected = false;
        IsExpanded = false;
    }

    public Icons Icon { get; protected set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public bool IsSelectable { get; set; }
    public bool? IsSelected { get; set; }
    public bool IsExpanded { get; set; }
    public ObservableCollection<DirectoryViewModel> SubFolders { get; protected set; }
    public void LoadSubFolders()
    {
        SubFolders.Clear();

        var options = new EnumerationOptions();
        foreach (var directoryInfo in _directoryInfo.GetDirectories("*", options))
        {
            SubFolders.Add(new DirectoryViewModel(directoryInfo, this));
        }
    }

    protected virtual void OnIsSelectedChanged()
    {
        UpdateIcon();

        if (_parent != null)
        {
            var allSiblings = _parent.SubFolders.All(i => i.IsSelected != false);
            var noSiblings = _parent.SubFolders.All(i => i.IsSelected == false);

            _parent.IsSelected = allSiblings ? true : noSiblings ? false : null;
        }

        if (IsSelected == false)
        {
            foreach (var subFolder in SubFolders)
            {
                subFolder.IsSelected = false;
            }
        }
    }

    private void UpdateIcon()
    {
        if (IsSelected == true)
        {
            Icon = IsExpanded ? Icons.SearchOpenFolder : Icons.SearchCloseFolder;
        }
        else
        {
            Icon = IsExpanded ? Icons.OpenFolder : Icons.CloseFolder;
        }
    }

    private bool HasSubfolders() => _directoryInfo.EnumerateDirectories("*", s_Options).Any();

    private static readonly EnumerationOptions s_Options = new();

    private readonly DirectoryInfo _directoryInfo;
    private readonly DirectoryViewModel? _parent;

}

