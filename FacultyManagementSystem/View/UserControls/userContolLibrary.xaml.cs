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
        public userContolLibrary()
        {
            InitializeComponent();
            DataContext = new ViewModel.LibraryViewModel();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
