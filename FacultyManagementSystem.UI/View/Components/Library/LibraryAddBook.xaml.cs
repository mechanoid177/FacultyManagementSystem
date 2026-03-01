using FacultyManagementSystem.UI.ViewModel.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for LibraryAddBook.xaml
    /// </summary>
    public partial class LibraryAddBook : UserControl
    {
        public LibraryAddBook()
        {
            InitializeComponent();
        }

        public void GetService(LibraryAddBookViewModel libraryAddBookViewModel)
        {
            DataContext = libraryAddBookViewModel;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
