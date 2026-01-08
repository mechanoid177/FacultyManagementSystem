using FacultyManagementSystem.Faculty;
using FacultyManagementSystem.Library;

namespace FacultyManagementSystem.Database.Interfaces
{
    public interface IDatabaseManager
    {
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