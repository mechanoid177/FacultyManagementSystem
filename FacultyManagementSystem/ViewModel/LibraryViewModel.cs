using CommunityToolkit.Mvvm.Input;
using FacultyManagementSystem.MVVM;
using FMS.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FacultyManagementSystem.ViewModel
{
    public class LibraryViewModel : ViewModelBase
    {
        private Library _library;

        public ICommand AddBookCommand;

        public ICommand RemoveBookCommand;

        public ICommand IssueBookCommand;

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

        private string _scannedBookBarcode;
        public string ScannedBookBarcode
        {
            get { return _scannedBookBarcode; }
            set
            {
                _scannedBookBarcode = value;
                OnPropertyChanged(nameof(ScannedBookBarcode));
            }
        }

        private string _scannedMemberBarcode;
        public string ScannedMemberBarcode
        {
            get { return _scannedMemberBarcode; }
            set
            {
                _scannedMemberBarcode = value;
                OnPropertyChanged(nameof(ScannedMemberBarcode));
            }
        }

        public LibraryViewModel()
        {
            _library = new Library();

            AddBookCommand = new RelayCommand(AddBook);
            RemoveBookCommand = new RelayCommand(RemoveBook);
            IssueBookCommand = new RelayCommand(IssueBook);
        }

        private void AddBook()
        {
            _library.AddBook(BookTitle, Author, Description, ISBN, Barcode, NumberOfCopies);
        }

        private void RemoveBook()
        {
            _library.RemoveBook(_scannedBookBarcode);
        }

        private void IssueBook()
        {
            _library.IssueBook(_scannedBookBarcode, _scannedMemberBarcode);
        }
    }
}