using FacultyManagementSystem.Faculty;
using FacultyManagementSystem.Library;
using FacultyManagementSystem.Utility;
using System.Net;

namespace FacultyManagementSystem.Database.Interfaces
{
    public interface IDatabaseManager
    {
        public event EventHandler<MessengerEventArgs> ActionFailed;

        #region Common methods

        public bool AuthenticateUser(NetworkCredential credential);

        #endregion

        #region Student methods

        void CreateNewStudent(Student student);
        Student FindStudentByBarcode(string barcode);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);

        #endregion

        #region Employee methods

        void CreateNewEmployee(Employee employee);
        Employee FindEmployeeByBarcode(string barcode);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);

        #endregion

        #region Book methods

        void CreateNewBook(Book book);
        void UpdateBook(Book book);
        Book FindBookByBarcode(string barcode);
        List<Book>? FindMatchingBooks(string searchString);
        void DeleteBook(Book book);

        #endregion

        #region Transaction methods

        void CreateNewTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        Transaction FindTransaction(string bookBarcode, string memberBarcode);
        
        #endregion

        void Dispose(); 
    }
}