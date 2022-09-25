using Ninject;
using SmartDuplicateFinder.Dialog;
using SmartDuplicateFinder.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartDuplicateFinder;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        AddCommandBindings();

        var view = App.Current.Container.Get<FileDuplicatesView>();
        SetView(view);

        Title = App.Name;
        DataContext = this;
    }

    public bool IsBusy { get; set; }

    public Control CurrentView { get; private set; }

    private void SetView(Control view)
    {
        CurrentView = view;
    }
    
    private void ShowAbout()
    {
        var dialog = new About()
        {
            Owner = this
        };

        dialog.ShowDialog();
    }
    private void AddCommandBindings()
    {
        //
        // File Menu
        //

        CommandBindings.Add(new CommandBinding(AppCommands.Exit, (sender, args) => Close()));

        //
        // Help Menu
        //
        CommandBindings.Add(new CommandBinding(AppCommands.AboutHelp, (sender, args) => ShowAbout()));
    }


}
