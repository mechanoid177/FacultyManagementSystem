using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FMS.Library;
using System.Collections.ObjectModel;

namespace FacultyManagementSystem.ViewModel
{
    public partial class LibraryViewModel : ObservableObject
    {
        private Library _library;

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
        private string _searchKeyword;

        [ObservableProperty]
        private string _searchTitle;

        [ObservableProperty]
        private string _searchAuthor;

        [ObservableProperty]
        private string _searchISBN;

        [ObservableProperty]
        private string _searchBarcode;

        [ObservableProperty]
        private int _searchNumberOfCopies;

        public ObservableCollection<Book> SearchResults;

        public ObservableCollection<Book> Books;

        public LibraryViewModel()
        {
            _library = new Library();

            SearchResults = new ObservableCollection<Book>();

            Books = new ObservableCollection<Book>(_library.Books.ToList());
            _library.Books.CollectionChanged += (s, e) => CopyListBooks();
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
            var result = _library.SearchBooks(_searchTitle, _searchAuthor, _searchISBN, _searchBarcode, _searchNumberOfCopies);
            foreach (var book in result)
            {
                SearchResults.Add(book);
            }
        }

        private void CopyListBooks()
        {
            Books.Clear();
            foreach (var book in _library.Books)
            {
                Books.Add(book);
            }
        }
    }
}