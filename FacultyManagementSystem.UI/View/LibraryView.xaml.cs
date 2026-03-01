using FacultyManagementSystem.UI.ViewModel;
using FacultyManagementSystem.Utility;
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

namespace FacultyManagementSystem.UI.View
{
    /// <summary>
    /// Interaction logic for LibraryView.xaml
    /// </summary>
    public partial class LibraryView : UserControl
    {
        public LibraryViewModel LibraryViewModel;

        public LibraryView()
        {
            InitializeComponent();
        }

        public void GetService(LibraryViewModel libraryViewModel)
        {
            LibraryViewModel = libraryViewModel;
            DataContext = LibraryViewModel;

            //this.listViewSearchedBooks.ItemsSource = LibraryViewModel.SearchResults;
            //this.listBoxSearchedBooks.ItemsSource = LibraryViewModel.SearchResults;

            LibraryViewModel.MessageReceived += OnMessageReceived;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void OnMessageReceived(object sender, MessengerEventArgs e)
        {
            MessageBox.Show(e.Message, "Library Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
