using FacultyManagementSystem.UI.ViewModel;
using System.Windows;

namespace FacultyManagementSystem.UI
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

        private void btnFaculty_Click(object sender, RoutedEventArgs e)
        {
            this.ucFaculty.Visibility = Visibility.Visible;
            this.ucLibrary.Visibility = Visibility.Collapsed;
            this.ucStudents.Visibility = Visibility.Collapsed;
            this.ucLogin.Visibility = Visibility.Collapsed;
        }

        private void btnLibrary_Click(object sender, RoutedEventArgs e)
        {
            this.ucFaculty.Visibility = Visibility.Collapsed;
            this.ucLibrary.Visibility = Visibility.Visible;
            this.ucStudents.Visibility = Visibility.Collapsed;
            this.ucLogin.Visibility = Visibility.Collapsed;
        }

        private void btnStudents_Click(object sender, RoutedEventArgs e)
        {
            this.ucFaculty.Visibility = Visibility.Collapsed;
            this.ucLibrary.Visibility = Visibility.Collapsed;
            this.ucStudents.Visibility = Visibility.Visible;
            this.ucLogin.Visibility = Visibility.Collapsed;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.ucLogin.Visibility = Visibility.Visible;
            this.ucFaculty.Visibility = Visibility.Collapsed;
            this.ucLibrary.Visibility = Visibility.Collapsed;
            this.ucStudents.Visibility = Visibility.Collapsed;
        }
    }
}