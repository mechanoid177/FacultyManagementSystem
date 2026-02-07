using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.UI.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private LibraryViewModel _libraryViewModel;
        private LoginViewModel _loginViewModel;
        private FacultyViewModel _facultyViewModel;
        private StudentViewModel _studentViewModel;

        [ObservableProperty]
		private ObservableObject _selectedViewModel;

        public MainViewModel(LibraryViewModel libraryViewModel, LoginViewModel loginViewModel, FacultyViewModel facultyViewModel, StudentViewModel studentViewModel)
        {
            _libraryViewModel = libraryViewModel;
            _loginViewModel = loginViewModel;
            _facultyViewModel = facultyViewModel;
            _studentViewModel = studentViewModel;
            SelectedViewModel = _loginViewModel;
        }


        [RelayCommand]
        private void ShowLibraryView()
        {
            SelectedViewModel = _libraryViewModel;
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
