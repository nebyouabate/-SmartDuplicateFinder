using SmartDuplicateFinder.Util;
using System.Windows;
using System.Windows.Input;

namespace SmartDuplicateFinder.Dialog
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            AddCommandBindings();

            GetVersionInfo();

            DataContext = this;
        }
        public string Version { get; private set; } = "";

        private void GetVersionInfo()
        {
            Version = CoreAssembly.Version.ToString(4);
        }
        private void AddCommandBindings()
        {
            CommandBindings.Add(new CommandBinding(AppCommands.Okay, (sender, args) => Close()));
        }
    }
}
