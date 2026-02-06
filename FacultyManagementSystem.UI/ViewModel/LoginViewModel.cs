using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacultyManagementSystem.Database;
using FacultyManagementSystem.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace FacultyManagementSystem.UI.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        private IDatabaseManager _databaseManager;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _username;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private SecureString? _password;

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        private bool _isViewVisible = true;

        public LoginViewModel(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private void Login() 
        { 
            bool isValidUser = _databaseManager.AuthenticateUser(new System.Net.NetworkCredential(Username, Password));
            if (isValidUser)
            {
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }

        [RelayCommand]
        private void RecoverPassword() { }

        [RelayCommand]
        private void ShowPassword() { }

        [RelayCommand]
        private void RememberPassword() { }

        private bool CanLogin()
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) ||
                Username.Length < 3 ||
                Password == null ||
                Password.Length < 3)
            {
                validData = false;
            }
            else
            {
                validData = true;
            }
            return validData;
        }
    }
}
