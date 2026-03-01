using FacultyManagementSystem.UI.View;
using FacultyManagementSystem.UI.View.Components;
using FacultyManagementSystem.UI.ViewModel;
using FacultyManagementSystem.UI.ViewModel.Library;
using Microsoft.Extensions.Logging;
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
            LibrarySearchBooksViewModel librarySearchBooksViewModel, LibrarySearchBooks librarySearchBooksView,
            LibraryAddBookViewModel libraryAddBookViewModel, LibraryAddBook libraryAddBookView,
            LibraryIssueBookViewModel libraryIssueBookViewModel, LibraryIssueBook libraryIssueBookView
            ) 
        { 
            studentView.GetService(studentViewModel);
            facultyView.GetService(facultyViewModel);
            loginView.GetService(loginViewModel);
            librarySearchBooksView.GetService(librarySearchBooksViewModel);
            libraryAddBookView.GetService(libraryAddBookViewModel);
            libraryIssueBookView.GetService(libraryIssueBookViewModel);
        }
    }
}
