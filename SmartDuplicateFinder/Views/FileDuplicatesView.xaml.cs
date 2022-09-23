using System.Windows.Controls;

namespace SmartDuplicateFinder.Views
{
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
        }

        private void AddCommandBindings()
        {
            // CommandBindings.Add(new CommandBinding(AppCommands.Exit, (sender, args) => Close()));
        }
    }
}
