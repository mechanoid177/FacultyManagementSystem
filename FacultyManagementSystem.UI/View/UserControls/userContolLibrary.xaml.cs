using FacultyManagementSystem.Library.Interfaces;
using FacultyManagementSystem.Utility;
using FacultyManagementSystem.ViewModel;
using Microsoft.Extensions.Hosting;
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

namespace FacultyManagementSystem.View.UserControls
{
    /// <summary>
    /// Interaction logic for userContolLibrary.xaml
    /// </summary>
    public partial class userContolLibrary : UserControl
    {
        public LibraryViewModel LibraryViewModel;

        public userContolLibrary()
        {
            InitializeComponent();
        }

        public void GetService(LibraryViewModel libraryViewModel)
        {
            LibraryViewModel = libraryViewModel;
            DataContext = LibraryViewModel;

            this.listViewSearchedBooks.ItemsSource = LibraryViewModel.SearchResults;

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
