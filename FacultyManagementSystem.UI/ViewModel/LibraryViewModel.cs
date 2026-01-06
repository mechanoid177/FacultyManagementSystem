using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacultyManagementSystem.Library;
using System.Collections.ObjectModel;
using FacultyManagementSystem.Library.Interfaces;
using FacultyManagementSystem.Utility;

namespace FacultyManagementSystem.ViewModel
{
    public partial class LibraryViewModel : ObservableObject, IDisposable
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

        [ObservableProperty]
        private string _scannedBookBarcode;

        [ObservableProperty]
        private string _scannedMemberBarcode;

        [ObservableProperty]
        private string _searchKeywords;

        public ObservableCollection<Book> SearchResults;

        public event EventHandler<MessengerEventArgs> MessageReceived;

        public LibraryViewModel(ILibrary library)
        {
            _library = library;

            _library.ActionFailed += (s, e) => OnMessageReceived(e.Message);

            SearchResults = new ObservableCollection<Book>();
        }

        [RelayCommand]
        private void AddBook()
        {
            _library.AddBook(BookTitle, Author, Description, ISBN, Barcode, NumberOfCopies);
        }

        [RelayCommand]
        private void RemoveBook()
        {
            _library.RemoveBook(_scannedBookBarcode);
        }

        [RelayCommand]
        private void IssueBook()
        {
            _library.IssueBook(_scannedBookBarcode, _scannedMemberBarcode);
        }

        [RelayCommand]
        private void SearchBook()
        {
            SearchResults.Clear();
            var result = _library.SearchBooks(_searchKeywords);

            if (result == null) return;

            foreach (var book in result)
            {
                SearchResults.Add(book);
            }
        }

        [RelayCommand]
        private void ReturnBook()
        {
            _library.ReturnBook(_scannedBookBarcode, _scannedMemberBarcode);
        }

        protected void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, new MessengerEventArgs(message));
        }

        public void Dispose()
        {
            _library?.Dispose();
        }
    }
}