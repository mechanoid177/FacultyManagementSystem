using FacultyManagementSystem.Faculty;
using FacultyManagementSystem.Library;

namespace FacultyManagementSystem.Database.Interfaces
{
    public interface IMySQLDatabase : IDisposable
    {
        void DeleteBook(Book book);
        void DeleteMember(Person member);
        Book FindBookByBarcode(string barcode);
        List<Book>? FindMatchingBooks(string searchString);
        Person FindMemberByBarcode(string barcode);
        Transaction FindTransaction(string bookBarcode, string memberBarcode);
        List<Book> GetAllBooks();
        List<Person> GetAllMembers();
        List<Transaction> GetAllTransactions();
        void InsertNewBook(Book book);
        void InsertNewMember(Person member);
        void InsertNewTransaction(Transaction transaction);
        void UpdateBook(Book book);
        void UpdateMember(Person member);
        void UpdateTransaction(Transaction transaction);
        void Dispose();
    }
}