using FacultyManagementSystem.Faculty;
using FacultyManagementSystem.Utility;
using System.Collections.ObjectModel;

namespace FacultyManagementSystem.Library.Interfaces
{
    public interface ILibrary : IDisposable
    {
        event EventHandler<MessengerEventArgs> ActionFailed;

        bool AddBook(string title, string author, string description, string ISBN, string barcode, int numberOfCopies);
        bool IssueBook(string bookBarcode, string memberBarcode);
        bool RemoveBook(string barcode);
        bool RemoveBookCopy(string barcode);
        bool ReturnBook(string bookBarcode, string memberBarcode);
        List<Book> SearchBooks(string searchString);
    }
}