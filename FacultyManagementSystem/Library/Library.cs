using FacultyManagementSystem.Database;
using FacultyManagementSystem.Database.Interfaces;
using FacultyManagementSystem.Faculty;
using FacultyManagementSystem.Library.Interfaces;
using FacultyManagementSystem.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FacultyManagementSystem.Library
{
    /// <summary>
    /// Represents a library that manages collections of books, members, and borrowing transactions, and provides
    /// operations for borrowing, returning, adding, and removing books.
    /// </summary>
    /// <remarks>The Library class maintains the state of its book, member, and transaction collections in
    /// memory. It provides methods to facilitate typical library operations, such as lending books to members and
    /// processing returns. All operations assume that barcodes uniquely identify books and members. This class is not
    /// thread-safe; concurrent access should be synchronized externally if used in multi-threaded scenarios.</remarks>
    public class Library : ILibrary
    {
        private IMySQLDatabase _database;

        /// <summary>
        /// Gets or sets the collection of people who are members of the group.
        /// </summary>
        public ObservableCollection<Person> Members { get; set; }

        /// <summary>
        /// Gets or sets the collection of books associated with this instance.
        /// </summary>
        public ObservableCollection<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the collection of transactions associated with this instance.
        /// </summary>
        public ObservableCollection<Transaction> Transactions { get; set; }

        public event EventHandler<MessengerEventArgs> ActionFailed;

        /// <summary>
        /// Initializes a new instance of the Library class with empty collections of members, books, and transactions.
        /// </summary>
        public Library(IMySQLDatabase mySQLDatabase)
        {
            _database = mySQLDatabase;
            Members = new ObservableCollection<Person>(_database.GetAllMembers().ToList());
            Books = new ObservableCollection<Book>(_database.GetAllBooks().ToList());
            Transactions = new ObservableCollection<Transaction>(_database.GetAllTransactions().ToList());
        }

        /// <summary>
        /// Attempts to borrow a book for a library member using the specified book and member barcodes.
        /// </summary>
        /// <remarks>A successful borrow operation decreases the number of available copies of the book by
        /// one and records the transaction. The borrowing period is set to 14 days from the date of
        /// borrowing.</remarks>
        /// <param name="bookBarcode">The barcode of the book to be borrowed. Must correspond to an existing book in the library.</param>
        /// <param name="memberBarcode">The barcode of the member borrowing the book. Must correspond to a registered library member.</param>
        /// <returns>true if the book was successfully borrowed; otherwise, false. Returns false if the book does not exist, is
        /// unavailable, or the member is not found.</returns>
        public bool IssueBook(string bookBarcode, string memberBarcode)
        {
            var book = _database.FindBookByBarcode(bookBarcode);

            if (book == null)
            {
                OnActionFailed("Book not found.");
                return false;
            }

            if (book.NumberOfAvailableCopies == 0)
            {
                OnActionFailed("No available copies of the book.");
                return false;
            }

            var member = _database.FindMemberByBarcode(memberBarcode);

            if (member == null)
            {
                OnActionFailed("Member not found.");
                return false;
            }

            book.NumberOfAvailableCopies -= 1;
            Transaction transaction = new Transaction(Guid.NewGuid(), book.Barcode, memberBarcode, DateTime.Now, DateTime.Now.AddDays(14));
            _database.InsertNewTransaction(transaction);
            _database.UpdateBook(book);

            return true;
        }

        /// <summary>
        /// Processes the return of a borrowed book by a library member.
        /// </summary>
        /// <remarks>The method returns false if the specified book or member does not exist, or if there
        /// is no active borrowing transaction for the given book and member.</remarks>
        /// <param name="bookBarcode">The barcode that uniquely identifies the book to be returned. Cannot be null or empty.</param>
        /// <param name="memberBarcode">The barcode that uniquely identifies the member returning the book. Cannot be null or empty.</param>
        /// <returns>true if the book return is successfully processed; otherwise, false.</returns>
        public bool ReturnBook(string bookBarcode, string memberBarcode)
        {
            var book = _database.FindBookByBarcode(bookBarcode);

            if (book == null)
            {
                OnActionFailed("Book not found.");
                return false;
            }
            var member = _database.FindMemberByBarcode(memberBarcode);
            if (member == null)
            {
                OnActionFailed("Member not found.");
                return false;
            }

            var transaction = _database.FindTransaction(bookBarcode, memberBarcode);

            if (transaction == null)
            {
                OnActionFailed("Was not issued.");
                return false;
            }

            transaction.ReturnDate = DateTime.Now;
            book.NumberOfAvailableCopies += 1;
            _database.UpdateTransaction(transaction);
            _database.UpdateBook(book);

            return true;
        }

        /// <summary>
        /// Adds a new book to the collection with the specified details.
        /// </summary>
        /// <param name="title">The title of the book to add. Cannot be null or empty.</param>
        /// <param name="author">The author of the book. Cannot be null or empty.</param>
        /// <param name="description">A description of the book. Can be null or empty if no description is available.</param>
        /// <param name="ISBN">The International Standard Book Number (ISBN) of the book. Cannot be null or empty.</param>
        /// <param name="barcode">The barcode associated with the book. Cannot be null or empty.</param>
        /// <param name="numberOfCopies">The total number of copies of the book to add. Must be greater than or equal to 1.</param>
        public bool AddBook(string title, string author, string description, string ISBN, string barcode, int numberOfCopies)
        {
            if (numberOfCopies < 1 || barcode == null)
            {
                OnActionFailed("Invalid book details provided.");
                return false;
            }

            var existingBook = _database.FindBookByBarcode(barcode);

            if (existingBook != null)
            {
                existingBook.NumberOfCopies += numberOfCopies;
                existingBook.NumberOfAvailableCopies += numberOfCopies;
                _database.UpdateBook(existingBook);
                return true;
            }

            var book = new Book
            {
                Title = title,
                Author = author,
                Description = description,
                ISBN = ISBN,
                Barcode = barcode,
                NumberOfCopies = numberOfCopies,
                NumberOfAvailableCopies = numberOfCopies
            };

            _database.InsertNewBook(book);

            return true;
        }

        /// <summary>
        /// Removes the book with the specified barcode from the collection.
        /// </summary>
        /// <param name="barcode">The barcode of the book to remove. Cannot be null.</param>
        /// <returns>true if a book with the specified barcode was found and removed; otherwise, false.</returns>
        public bool RemoveBook(string barcode)
        {
            var book = _database.FindBookByBarcode(barcode);

            if (book == null)
            {
                OnActionFailed("Book not found.");
                return false;
            }

            _database.DeleteBook(book);

            return true;
        }

        /// <summary>
        /// Removes a single copy of the book identified by the specified barcode from the collection.
        /// </summary>
        /// <remarks>If the specified barcode does not match any book in the collection or if there are no
        /// copies to remove, the method returns false and no changes are made.</remarks>
        /// <param name="barcode">The barcode that uniquely identifies the book copy to remove. Cannot be null.</param>
        /// <returns>true if a copy was successfully removed; otherwise, false.</returns>
        public bool RemoveBookCopy(string barcode)
        {
            var book = _database.FindBookByBarcode(barcode);
            if (book == null || book.NumberOfCopies == 0)
            {
                OnActionFailed("Book not found or no copies to remove.");
                return false;
            }

            book.NumberOfCopies -= 1;

            if (book.NumberOfAvailableCopies > 0)
            {
                book.NumberOfAvailableCopies -= 1;
                _database.UpdateBook(book);
            }
            return true;
        }

        /// <summary>
        /// Searches for books that match the specified criteria.
        /// </summary>
        /// <remarks>All criteria are combined using logical AND. Only books that match every specified
        /// (non-null or non-empty) parameter are included in the result.</remarks>
        /// <param name="title">The title or partial title of the book to search for. If null or empty, this criterion is ignored.</param>
        /// <param name="author">The author or partial author name to search for. If null or empty, this criterion is ignored.</param>
        /// <param name="isbn">The ISBN or partial ISBN to search for. If null or empty, this criterion is ignored.</param>
        /// <param name="barcode">The barcode or partial barcode to search for. If null or empty, this criterion is ignored.</param>
        /// <param name="numberOfAvaiableCopies">The exact number of available copies to match. If null, this criterion is ignored.</param>
        /// <returns>A list of books that match all specified search criteria. The list is empty if no books are found.</returns>
        public List<Book> SearchBooks(string searchString)
        {
            return _database.FindMatchingBooks(searchString);
        }

        protected void OnActionFailed(string message)
        {
            ActionFailed?.Invoke(this, new MessengerEventArgs(message));
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
