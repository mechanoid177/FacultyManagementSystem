using FMS.Faculty;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Library
{
    /// <summary>
    /// Represents a library that manages collections of books, members, and borrowing transactions, and provides
    /// operations for borrowing, returning, adding, and removing books.
    /// </summary>
    /// <remarks>The Library class maintains the state of its book, member, and transaction collections in
    /// memory. It provides methods to facilitate typical library operations, such as lending books to members and
    /// processing returns. All operations assume that barcodes uniquely identify books and members. This class is not
    /// thread-safe; concurrent access should be synchronized externally if used in multi-threaded scenarios.</remarks>
    public class Library
    {
        /// <summary>
        /// Gets or sets the collection of people who are members of the group.
        /// </summary>
        public List<Person> Members { get; set; }

        /// <summary>
        /// Gets or sets the collection of books associated with this instance.
        /// </summary>
        public List<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the collection of transactions associated with this instance.
        /// </summary>
        public List<Transaction> Transactions { get; set; }

        /// <summary>
        /// Initializes a new instance of the Library class with empty collections of members, books, and transactions.
        /// </summary>
        public Library()
        {
            Members = new List<Person>();
            Books = new List<Book>();
            Transactions = new List<Transaction>();
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
            var book = Books.SingleOrDefault(x => x.Barcode.Equals(bookBarcode));

            if (book == null)
            {
                return false;
            }

            if (book.NumberOfAvailableCopies == 0)
            {
                return false;
            }

            var member = Members.SingleOrDefault(x => x.Barcode.Equals(memberBarcode));

            if (member == null)
            {
                return false;
            }

            book.NumberOfAvailableCopies -= 1;
            Transactions.Add(new Transaction(book.Barcode, memberBarcode, DateTime.Now, DateTime.Now.AddDays(14)));

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
            var book = Books.SingleOrDefault(x => x.Barcode.Equals(bookBarcode));
            if (book == null)
            {
                return false;
            }
            var member = Members.SingleOrDefault(x => x.Barcode.Equals(memberBarcode));
            if (member == null)
            {
                return false;
            }

            var transaction = Transactions.SingleOrDefault(x => x.BookBarcode.Equals(bookBarcode) && x.MemberBarcode.Equals(memberBarcode) && x.ReturnDate == null);

            if (transaction == null)
            {
                return false;
            }

            transaction.ReturnDate = DateTime.Now;
            book.NumberOfAvailableCopies += 1;

            return true;
        }

        /// <summary>
        /// Adds a new book to the collection with the specified details.
        /// </summary>
        /// <param name="title">The title of the book to add. Cannot be null or empty.</param>
        /// <param name="author">The author of the book. Cannot be null or empty.</param>
        /// <param name="description">A description of the book. Can be null or empty if no description is available.</param>
        /// <param name="ISBN">The International Standard Book Number (ISBN) of the book. Cannot be null or empty.</param>
        /// <param name="Barcode">The barcode associated with the book. Cannot be null or empty.</param>
        /// <param name="numberOfCopies">The total number of copies of the book to add. Must be greater than or equal to 1.</param>
        public void AddBook(string title, string author, string description, string ISBN, string Barcode, int numberOfCopies)
        { 
            Books.Add(new Book
            {
                Title = title,
                Author = author,
                Description = description,
                ISBN = ISBN,
                Barcode = Barcode,
                NumberOfCopies = numberOfCopies,
                NumberOfAvailableCopies = numberOfCopies
            });
        }

        /// <summary>
        /// Removes the book with the specified barcode from the collection.
        /// </summary>
        /// <param name="Barcode">The barcode of the book to remove. Cannot be null.</param>
        /// <returns>true if a book with the specified barcode was found and removed; otherwise, false.</returns>
        public bool RemoveBook(string Barcode) 
        {
            var book = Books.SingleOrDefault(x => x.Barcode.Equals(Barcode));

            if (book == null)
            {
                return false;
            }

            Books.Remove(book);

            return true;
        }

        /// <summary>
        /// Searches for books that match the specified criteria.
        /// </summary>
        /// <remarks>All criteria are combined using logical AND. Only books that match every specified
        /// (non-null or non-empty) parameter are included in the result.</remarks>
        /// <param name="name">The title or partial title of the book to search for. If null or empty, this criterion is ignored.</param>
        /// <param name="author">The author or partial author name to search for. If null or empty, this criterion is ignored.</param>
        /// <param name="isbn">The ISBN or partial ISBN to search for. If null or empty, this criterion is ignored.</param>
        /// <param name="barcode">The barcode or partial barcode to search for. If null or empty, this criterion is ignored.</param>
        /// <param name="numberOfAvaiableCopies">The exact number of available copies to match. If null, this criterion is ignored.</param>
        /// <returns>A list of books that match all specified search criteria. The list is empty if no books are found.</returns>
        public List<Book> SearchBooks(string name, string author, string isbn, string barcode, int? numberOfAvaiableCopies)
        {
            return Books.Where(Books => 
                (string.IsNullOrEmpty(name) || Books.Title.Contains(name)) &&
                (string.IsNullOrEmpty(author) || Books.Author.Contains(author)) &&
                (string.IsNullOrEmpty(isbn) || Books.ISBN.Contains(isbn)) &&
                (string.IsNullOrEmpty(barcode) || Books.Barcode.Contains(barcode)) &&
                (!numberOfAvaiableCopies.HasValue || Books.NumberOfAvailableCopies == numberOfAvaiableCopies.Value)
            ).ToList();
        }
    }
}
