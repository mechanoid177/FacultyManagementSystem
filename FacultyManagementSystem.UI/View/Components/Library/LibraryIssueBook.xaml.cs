using FacultyManagementSystem.UI.ViewModel.Library;
using System;
using System.Collections.Generic;
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

namespace FacultyManagementSystem.UI.View.Components
{
    /// <summary>
    /// Interaction logic for LibraryIssueBook.xaml
    /// </summary>
    public partial class LibraryIssueBook : UserControl
    {
        public LibraryIssueBook()
        {
            InitializeComponent();
        }

        public void GetService(LibraryIssueBookViewModel libraryIssueBookViewModel)
        {
            DataContext = libraryIssueBookViewModel;
        }
    }
}
