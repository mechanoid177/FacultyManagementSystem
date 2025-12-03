using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Library
{
    /// <summary>
    /// Represents a record of a book borrowing transaction in the library system, including information about the
    /// borrowed book, the member, and relevant dates.
    /// </summary>
    /// <remarks>A Transaction contains details about when a book was borrowed, by whom, and when it is due or
    /// returned. This class is typically used to track the lending and return of library materials.</remarks>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        public Guid TransactionId { get; }

        /// <summary>
        /// Gets or sets the barcode assigned to the book.
        /// </summary>
        public string BookBarcode { get; set; }

        /// <summary>
        /// Gets or sets the barcode assigned to the member.
        /// </summary>
        public string MemberBarcode { get; set; }

        /// <summary>
        /// Gets or sets the date when the item was borrowed.
        /// </summary>
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the item was returned, or null if it has not been returned.
        /// </summary>
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time by which the item is due.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the Transaction class with the specified book and member barcodes, borrow
        /// date, and due date.
        /// </summary>
        /// <param name="bookBarcode">The barcode that uniquely identifies the borrowed book. Cannot be null or empty.</param>
        /// <param name="memberBarcode">The barcode that uniquely identifies the library member borrowing the book. Cannot be null or empty.</param>
        /// <param name="borrowDate">The date and time when the book is borrowed.</param>
        /// <param name="dueDate">The date and time by which the borrowed book must be returned.</param>
        public Transaction(string bookBarcode, string memberBarcode, DateTime borrowDate, DateTime dueDate)
        {
            TransactionId = Guid.NewGuid();
            BookBarcode = bookBarcode;
            MemberBarcode = memberBarcode;
            BorrowDate = borrowDate;
            DueDate = dueDate;
            ReturnDate = null;
        }
    }
}
