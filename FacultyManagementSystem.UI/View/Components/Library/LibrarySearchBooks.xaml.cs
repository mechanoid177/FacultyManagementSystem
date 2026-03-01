using FacultyManagementSystem.Library;
using FacultyManagementSystem.UI.ViewModel;
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
    /// Interaction logic for LibrarySearchBooks.xaml
    /// </summary>
    public partial class LibrarySearchBooks : UserControl
    {
        public LibrarySearchBooks()
        {
            InitializeComponent();
        }

        public void GetService(LibrarySearchBooksViewModel librarySearchBooksViewModel)
        {
            DataContext = librarySearchBooksViewModel;

            this.listBoxSearchedBooks.ItemsSource = librarySearchBooksViewModel.SearchResults;
        }
    }
}
