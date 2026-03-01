using FacultyManagementSystem.UI.Service;
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

        public MainWindow(MainViewModel mainViewModel, ResolveServices resolve)
        {
            InitializeComponent();
            DataContext = MainViewModel = mainViewModel;
        }
    }
}