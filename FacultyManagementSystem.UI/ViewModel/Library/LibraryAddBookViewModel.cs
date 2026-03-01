using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacultyManagementSystem.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.UI.ViewModel.Library
{
    public partial class LibraryAddBookViewModel : ObservableObject, IDisposable
    {
        private ILibrary _library;

        [ObservableProperty]
        private string _bookTitle;

        [ObservableProperty]
        private string _author;

        [ObservableProperty]
        private string _ISBN;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _barcode;

        [ObservableProperty]
        private int _numberOfCopies;

        public LibraryAddBookViewModel(ILibrary library)
        {
            _library = library;
        }

        [RelayCommand]
        private void AddBook()
        {
            _library.AddBook(BookTitle, Author, Description, ISBN, Barcode, NumberOfCopies);
        }

        [RelayCommand]
        private void RemoveBook()
        {
            _library.RemoveBook(Barcode);
        }

        public void Dispose()
        {
            _library?.Dispose();
        }
    }
}
