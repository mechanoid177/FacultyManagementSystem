using FacultyManagementSystem.View.UserControls;
using FacultyManagementSystem.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FacultyManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel;

        public MainWindow(MainViewModel mainViewModel, LibraryViewModel libraryViewModel)
        {
            InitializeComponent();
            DataContext = MainViewModel = mainViewModel;
            this.ucLibrary.GetService(libraryViewModel);
        }
    }
}