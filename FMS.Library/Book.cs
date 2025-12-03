using FMS.Faculty;

namespace FMS.Library
{
    /// <summary>
    /// Represents a book in a library or catalog, including bibliographic and inventory information.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets the unique identifier for this instance.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the title associated with the object.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description associated with the object.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the author associated with the item.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the International Standard Book Number (ISBN) for the book.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the number of copies in the system.
        /// </summary>
        public int NumberOfCopies { get; set; }

        /// <summary>
        /// Gets or sets the number of copies currently available for borrowing or use.
        /// </summary>
        public int NumberOfAvailableCopies { get; set; }

        /// <summary>
        /// Gets or sets the barcode value associated with the item.
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// Initializes a new instance of the Book class with a unique identifier.
        /// </summary>
        public Book()
        {
            Id = Guid.NewGuid();
        }
    }
}
