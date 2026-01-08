using FacultyManagementSystem.Database.Interfaces;
using FacultyManagementSystem.Faculty;
using FacultyManagementSystem.Library;
using System.Diagnostics;

namespace FacultyManagementSystem.Database
{
    public class DatabaseManager : IDatabaseManager
    {
        private IMySqlDatabase _mySqlDatabase;
        private DatabaseContext _dbContext;
        private bool _useMySql;
        private bool _useEFCore;

        public DatabaseManager(IMySqlDatabase mySqlDatabase, DatabaseContext dbContext)
        {
            _mySqlDatabase = mySqlDatabase;
            _dbContext = dbContext;
            _useMySql = true;
            _useEFCore = false;
        }

        #region Student methods

        public void CreateNewStudent(Student student)
        {
            if (_useMySql)
            {
                _mySqlDatabase.CreateNewStudent(student);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Students.Add(student);
                    _dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error inserting new student: {ex.Message}");
                }
            }
            else
            { }
        }

        public Student FindStudentByBarcode(string barcode)
        {
            if (_useMySql)
            {
                return _mySqlDatabase.FindStudentByBarcode(barcode);
            }
            else if (_useEFCore)
            {
                try
                {
                    return _dbContext.Students.FirstOrDefault(s => s.Barcode == barcode);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error finding student by barcode: {ex.Message}");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void UpdateStudent(Student student)
        {
            if (_useMySql)
            {
                _mySqlDatabase.UpdateStudent(student);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Students.Update(student);
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error updating student: {ex.Message}");
                }
            }
            else
            { }
        }

        public void DeleteStudent(Student student)
        {
            if (_useMySql)
            {
                _mySqlDatabase.DeleteStudent(student);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Students.Remove(student);
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting student: {ex.Message}");
                }
            }
            else
            { }
        }

        #endregion


        #region Employee methods

        public void CreateNewEmployee(Employee employee)
        {
            if (_useMySql)
            {
                _mySqlDatabase.CreateNewEmployee(employee);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Employees.Add(employee);
                    _dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error inserting new employee: {ex.Message}");
                }
            }
            else
            { }
        }

        public Employee FindEmployeeByBarcode(string barcode)
        {
            if (_useMySql)
            {
                return _mySqlDatabase.FindEmployeeByBarcode(barcode);
            }
            else if (_useEFCore)
            {
                try
                {
                    return _dbContext.Employees.FirstOrDefault(e => e.Barcode == barcode);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error finding employee by barcode: {ex.Message}");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            if (_useMySql)
            {
                _mySqlDatabase.UpdateEmployee(employee);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Employees.Update(employee);
                    _dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error updating employee: {ex.Message}");
                }
            }
            else
            { }
        }

        public void DeleteEmployee(Employee employee)
        {
            if (_useMySql)
            {
                _mySqlDatabase.DeleteEmployee(employee);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Employees.Remove(employee);
                    _dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting employee: {ex.Message}");
                }
            }
            else
            { }
        }

        #endregion


        #region Book methods

        public void CreateNewBook(Book book)
        {
            if (_useMySql)
            {
                _mySqlDatabase.CreateNewBook(book);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Books.Add(book);
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error inserting new book: {ex.Message}");
                }
            }
            else
            { }
        }

        public void UpdateBook(Book book)
        {
            if (_useMySql)
            {
                _mySqlDatabase.UpdateBook(book);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Books.Update(book);
                    _dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error updating book: {ex.Message}");
                }
            }
            else
            { }
        }

        public void DeleteBook(Book book)
        {
            if (_useMySql)
            {
                _mySqlDatabase.DeleteBook(book);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Books.Remove(book);
                    _dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deleting book: {ex.Message}");
                }
            }
            else
            { }
        }

        public Book FindBookByBarcode(string barcode)
        {
            Book book = null;
            if (_useMySql)
            {
                book = _mySqlDatabase.FindBookByBarcode(barcode);
            }
            else if (_useEFCore)
            {
                try
                {
                    book = _dbContext.Books.FirstOrDefault(b => b.Barcode == barcode);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error finding book by barcode: {ex.Message}");
                }
            }
            else
            { }

            return book;
        }

        public List<Book>? FindMatchingBooks(string searchString)
        {
            List<Book> books = null;
            if (_useMySql)
            {
                books = _mySqlDatabase.FindMatchingBooks(searchString);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Books
                        .Where(
                            b => b.Title.Contains(searchString) ||
                            b.Author.Contains(searchString) ||
                            b.ISBN.Contains(searchString) ||
                            b.Description.Contains(searchString) ||
                            b.Barcode.Contains(searchString)
                            ).ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error finding matching books: {ex.Message}");
                }
            }
            else
            { }

            return books;
        }

        #endregion


        #region Transaction methods

        public void CreateNewTransaction(Transaction transaction)
        {
            if (_useMySql)
            {
                _mySqlDatabase.CreateNewTransaction(transaction);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Transactions.Add(transaction);
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error creating new transaction: {ex.Message}");

                }
            }
            else
            { }
        }

        public void UpdateTransaction(Transaction transaction)
        {
            if (_useMySql)
            {
                _mySqlDatabase.UpdateTransaction(transaction);
            }
            else if (_useEFCore)
            {
                try
                {
                    _dbContext.Transactions.Update(transaction);
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error updating transaction: {ex.Message}");
                }
            }
            else
            { }
        }

        public Transaction FindTransaction(string bookBarcode, string memberBarcode)
        {
            Transaction transaction = null;
            if (_useMySql)
            {
                transaction = _mySqlDatabase.FindTransaction(bookBarcode, memberBarcode);
            }
            else if (_useEFCore)
            {
                try
                {
                    transaction = _dbContext.Transactions
                        .FirstOrDefault(t => t.BookBarcode == bookBarcode && t.MemberBarcode == memberBarcode);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error finding transaction: {ex.Message}");
                }
            }
            else
            { }

            return transaction;
        }

        #endregion

        public void Dispose()
        {
            _mySqlDatabase?.Dispose();
            _dbContext?.Dispose();
        }
    }
}
