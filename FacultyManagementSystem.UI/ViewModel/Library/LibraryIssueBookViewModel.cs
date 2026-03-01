using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacultyManagementSystem.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.UI.ViewModel.Library
{
    public partial class LibraryIssueBookViewModel : ObservableObject, IDisposable
    {
        private ILibrary _library;

        [ObservableProperty]
        private string _scannedBookBarcode;

        [ObservableProperty]
        private string _scannedMemberBarcode;

        public LibraryIssueBookViewModel(ILibrary library)
        {
            _library = library;
        }

        [RelayCommand]
        private void IssueBook()
        {
            _library.IssueBook(ScannedBookBarcode, ScannedMemberBarcode);
        }

        [RelayCommand]
        private void ReturnBook()
        {
            _library.ReturnBook(ScannedBookBarcode, ScannedMemberBarcode);
        }

        public void Dispose()
        {
            _library?.Dispose();
        }
    }
}
