using FacultyManagementSystem.UI.View;
using FacultyManagementSystem.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.UI.Service
{
    public class ResolveServices
    {
        public ResolveServices(
            StudentViewModel studentViewModel, StudentView studentView,
            FacultyViewModel facultyViewModel, FacultyView facultyView,
            LoginViewModel loginViewModel, LoginView loginView,
            LibraryViewModel libraryViewModel, LibraryView libraryView
            ) 
        { 
            studentView.GetService(studentViewModel);
            facultyView.GetService(facultyViewModel);
            loginView.GetService(loginViewModel);
            libraryView.GetService(libraryViewModel);
        }
    }
}
