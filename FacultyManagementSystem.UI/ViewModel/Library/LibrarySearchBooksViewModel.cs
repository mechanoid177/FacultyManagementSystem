using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacultyManagementSystem.Library;
using FacultyManagementSystem.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FacultyManagementSystem.UI.ViewModel.Library
{
    public partial class LibrarySearchBooksViewModel : ObservableObject, IDisposable
    {
        private ILibrary _library;

        [ObservableProperty]
        private string _searchKeywords;

        public ObservableCollection<Book> SearchResults { get; set; } = new ObservableCollection<Book>();

        public LibrarySearchBooksViewModel(ILibrary library)
        {
            _library = library;
        }

        [RelayCommand]
        private void SearchBook()
        {
            SearchResults.Clear();
            var result = _library.SearchBooks(SearchKeywords);

            if (result == null) return;

            foreach (var book in result)
            {
                SearchResults.Add(book);
            }
        }

        public void Dispose()
        {
            _library?.Dispose();
        }
    }
}
