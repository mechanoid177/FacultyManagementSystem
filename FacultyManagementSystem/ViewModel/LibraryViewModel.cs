using FacultyManagementSystem.MVVM;
using FMS.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FacultyManagementSystem.ViewModel
{
    public class LibraryViewModel : ViewModelBase
    {
        private Library _library;

        private string _bookTitle;
        public string BookTitle
        {
            get { return _bookTitle; }
            set
            {
                _bookTitle = value;
                OnPropertyChanged(nameof(BookTitle));
            }
        }

        private string _author;
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        private string _ISBN;
        public string ISBN
        {
            get { return _ISBN; }
            set 
            {
                _ISBN = value;
                OnPropertyChanged(nameof(ISBN));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _barcode;
        public string Barcode
        {
            get { return _barcode; }
            set 
            { 
                _barcode = value;
                OnPropertyChanged(nameof(Barcode));
            }
        }

        private int _numberOfCopies;
        public int NumberOfCopies
        {
            get { return _numberOfCopies; }
            set
            {
                _numberOfCopies = value;
                OnPropertyChanged(nameof(NumberOfCopies));
            }
        }

        public LibraryViewModel()
        {
            _library = new Library();
        }

        public RelayCommand AddBookCommand => new RelayCommand((obj) =>
        {
            _library.AddBook(_bookTitle, _author, _description, _ISBN, _barcode, _numberOfCopies);
        });

    }
}
