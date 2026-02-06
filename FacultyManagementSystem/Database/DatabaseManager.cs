using FacultyManagementSystem.Database.Interfaces;
using FacultyManagementSystem.Faculty;
using FacultyManagementSystem.Library;
using FacultyManagementSystem.Library.Interfaces;
using FacultyManagementSystem.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace FacultyManagementSystem.Database
{
    public class DatabaseManager : IDatabaseManager
    {
        private IMySqlDatabase _mySqlDatabase;
        private DatabaseContext _dbContext;
        private bool _useMySql;
        private bool _useEFCore;
        private ILogger<DatabaseManager> _logger;

        public event EventHandler<MessengerEventArgs> ActionFailed;

        public DatabaseManager(IMySqlDatabase mySqlDatabase, DatabaseContext dbContext, ILogger<DatabaseManager> logger, IConfiguration configuration)
        {
            _mySqlDatabase = mySqlDatabase;
            _dbContext = dbContext;
            _useMySql = bool.Parse(configuration.GetSection("DatabaseInfo")["UseSQL"]);
            _useEFCore = bool.Parse(configuration.GetSection("DatabaseInfo")["UseEF"]);

            _mySqlDatabase.QueryFailed += (s, e) => OnActionFailed(e.Message);
        }

        #region Common methods

        public bool AuthenticateUser(NetworkCredential credential)
        {
            if (_useMySql)
            {
                return _mySqlDatabase.AuthenticateUser(credential);
            }
            else if (_useEFCore)
            {
                try
                {
                    var userEmployee = _dbContext.Employees.FirstOrDefault(e => e.Username == credential.UserName);
                    if (userEmployee != null)
                    {
                        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(credential.Password));
                        return userEmployee.Password == Convert.ToString(bytes);
                    }

                    var userStudent = _dbContext.Students.FirstOrDefault(e => e.Username == credential.UserName);
                    if (userStudent != null)
                    {
                        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(credential.Password));
                        return userStudent.Password == Convert.ToString(bytes);
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    OnActionFailed($"Error authenticating user: {ex.Message}");
                    _logger.LogError($"Error authenticating user: {ex.Message}");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion

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
                    OnActionFailed($"Error inserting new student: {ex.Message}");
                    _logger.LogError($"Error inserting new student: {ex.Message}");
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
                    OnActionFailed($"Error finding student by barcode: {ex.Message}");
                    _logger.LogError($"Error finding student by barcode: {ex.Message}");
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
                    OnActionFailed($"Error updating student: {ex.Message}");
                    _logger.LogError($"Error updating student: {ex.Message}");
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
                    OnActionFailed($"Error deleting student: {ex.Message}");
                    _logger.LogError($"Error deleting student: {ex.Message}");
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
                    OnActionFailed($"Error inserting new employee: {ex.Message}");
                    _logger.LogError($"Error inserting new employee: {ex.Message}");
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
                    OnActionFailed($"Error finding employee by barcode: {ex.Message}");
                    _logger.LogError($"Error finding employee by barcode: {ex.Message}");
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
                    OnActionFailed($"Error updating employee: {ex.Message}");
                    _logger.LogError($"Error updating employee: {ex.Message}");
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
                    OnActionFailed($"Error deleting employee: {ex.Message}");
                    _logger.LogError($"Error deleting employee: {ex.Message}");
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
                    OnActionFailed($"Error inserting new book: {ex.Message}");
                    _logger.LogError($"Error inserting new book: {ex.Message}");
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
                    OnActionFailed($"Error updating book: {ex.Message}");
                    _logger.LogError($"Error updating book: {ex.Message}");
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
                    OnActionFailed($"Error deleting book: {ex.Message}");
                    _logger.LogError($"Error deleting book: {ex.Message}");
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
                    OnActionFailed($"Error finding book by barcode: {ex.Message}");
                    _logger.LogError($"Error finding book by barcode: {ex.Message}");
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
                    OnActionFailed($"Error finding matching books: {ex.Message}");
                    _logger.LogError($"Error finding matching books: {ex.Message}");
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
                    OnActionFailed($"Error creating new transaction: {ex.Message}");
                    _logger.LogError($"Error creating new transaction: {ex.Message}");

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
                    OnActionFailed($"Error updating transaction: {ex.Message}");
                    _logger.LogError($"Error updating transaction: {ex.Message}");
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
                    OnActionFailed($"Error finding transaction: {ex.Message}");
                    _logger.LogError($"Error finding transaction: {ex.Message}");
                }
            }
            else
            { }

            return transaction;
        }

        #endregion

        protected void OnActionFailed(string message)
        {
            ActionFailed?.Invoke(this, new MessengerEventArgs(message));
        }

        public void Dispose()
        {
            _mySqlDatabase?.Dispose();
            _dbContext?.Dispose();
        }
    }
}
