using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacultyManagementSystem.UI.ViewModel.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.UI.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private LibrarySearchBooksViewModel _librarySearchBooksViewModel;
        private LibraryAddBookViewModel _libraryAddBookViewModel;
        private LibraryIssueBookViewModel _libraryIssueBookViewModel;
        private LoginViewModel _loginViewModel;
        private FacultyViewModel _facultyViewModel;
        private StudentViewModel _studentViewModel;

        [ObservableProperty]
		private ObservableObject _selectedViewModel;

        public MainViewModel(
            LibrarySearchBooksViewModel librarySearchBooksViewModel, 
            LibraryAddBookViewModel libraryAddBookViewModel,
            LibraryIssueBookViewModel libraryIssueBookViewModel,
            LoginViewModel loginViewModel, 
            FacultyViewModel facultyViewModel, 
            StudentViewModel studentViewModel)
        {
            _librarySearchBooksViewModel = librarySearchBooksViewModel;
            _libraryAddBookViewModel = libraryAddBookViewModel;
            _libraryIssueBookViewModel = libraryIssueBookViewModel;
            _loginViewModel = loginViewModel;
            _facultyViewModel = facultyViewModel;
            _studentViewModel = studentViewModel;
            SelectedViewModel = _loginViewModel;
        }


        [RelayCommand]
        private void ShowLibrarySearchBooksView()
        {
            SelectedViewModel = _librarySearchBooksViewModel;
        }

        [RelayCommand]
        private void ShowLibraryAddBookView()
        {
            SelectedViewModel = _libraryAddBookViewModel;
        }

        [RelayCommand]
        private void ShowLibraryIssueBookView()
        {
            SelectedViewModel = _libraryIssueBookViewModel;
        }

        [RelayCommand]
        private void ShowLoginView()
        {
            SelectedViewModel = _loginViewModel;
        }

        [RelayCommand]
        private void ShowFacultyView()
        {
            SelectedViewModel = _facultyViewModel;
        }

        [RelayCommand]
        private void ShowStudentView()
        {
            SelectedViewModel = _studentViewModel;
        }
    }
}
