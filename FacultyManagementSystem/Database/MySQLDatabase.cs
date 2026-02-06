using FacultyManagementSystem.Database.Interfaces;
using FacultyManagementSystem.Faculty;
using FacultyManagementSystem.Library;
using FacultyManagementSystem.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace FacultyManagementSystem.Database
{
    public class MySqlDatabase : IMySqlDatabase
    {
        private MySqlConnection _connection;
        private IConfiguration _configuration;
        private ILogger<MySqlDatabase> _logger;

        public event EventHandler<MessengerEventArgs> QueryFailed;

        public MySqlDatabase(IConfiguration configuration, ILogger<MySqlDatabase> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _connection = new MySqlConnection(_configuration.GetSection("DatabaseInfo")["ConnectionString"]);
        }

        #region Common methods

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool isAuthenticated = false;
            try
            {
                string sqlQuery = "SELECT COUNT(*) FROM Employees WHERE Username = @Username AND Password = SHA2(@Password, 256)";
                MySqlCommand command = new MySqlCommand(sqlQuery, _connection);
                command.Parameters.AddWithValue("@Username", credential.UserName);
                command.Parameters.AddWithValue("@Password", credential.Password);
                _connection.Open();
                int userCount = Convert.ToInt32(command.ExecuteScalar());
                isAuthenticated = userCount > 0;
                _connection.Close();

                if (isAuthenticated) return true;

                sqlQuery = "SELECT COUNT(*) FROM Students WHERE Username = @Username AND Password = SHA2(@Password, 256)";
                command = new MySqlCommand(sqlQuery, _connection);
                command.Parameters.AddWithValue("@Username", credential.UserName);
                command.Parameters.AddWithValue("@Password", credential.Password);
                _connection.Open();
                userCount = Convert.ToInt32(command.ExecuteScalar());
                isAuthenticated = userCount > 0;
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return isAuthenticated;
        }

        #endregion

        #region Student methods

        public void CreateNewStudent(Student student)
        {
            try
            {
                string insertSql = "INSERT INTO Students " +
                    "(Name, Surname, ParentName, JMBG, Phone, Address, Email, Barcode, Type) " +
                    "VALUES (@Name, @Surname, @ParentName, @JMBG, @Phone, @Address, @Email, @Barcode)";


                MySqlCommand insertCommand = new MySqlCommand(insertSql, _connection);

                // Parameters
                insertCommand.Parameters.AddWithValue("@Name", student.Name);
                insertCommand.Parameters.AddWithValue("@Surname", student.Surname);
                insertCommand.Parameters.AddWithValue("@ParentName", student.ParentName);
                insertCommand.Parameters.AddWithValue("@JMBG", student.JMBG);
                insertCommand.Parameters.AddWithValue("@Phone", student.Phone);
                insertCommand.Parameters.AddWithValue("@Address", student.Address);
                insertCommand.Parameters.AddWithValue("@Email", student.Email);
                insertCommand.Parameters.AddWithValue("@Barcode", student.Barcode);

                _connection.Open();
                int rowsAffected = insertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public Student FindStudentByBarcode(string barcode)
        {
            Student student = null;
            try
            {
                string selectSql = "SELECT * FROM Students WHERE Barcode = @Barcode";

                MySqlCommand selectCommand = new MySqlCommand(selectSql, _connection);

                selectCommand.Parameters.AddWithValue("@Barcode", barcode);

                _connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    student = new Student
                    {
                        Name = reader.GetString("Name"),
                        Surname = reader.GetString("Surname"),
                        ParentName = reader.GetString("ParentName"),
                        JMBG = reader.GetString("JMBG"),
                        Phone = reader.GetString("Phone"),
                        Address = reader.GetString("Address"),
                        Email = reader.GetString("Email"),
                        Barcode = reader.GetString("Barcode")
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return student;
        }

        public void UpdateStudent(Student student)
        {
            try
            {

                string updateSql = "UPDATE Students SET " +
                    "Name = @Name, " +
                    "Surname = @Surname, " +
                    "ParentName = @ParentName, " +
                    "JMBG = @JMBG, " +
                    "Phone = @Phone, " +
                    "Address = @Address, " +
                    "Email = @Email, " +
                    "WHERE Barcode = @Barcode";

                MySqlCommand updateCommand = new MySqlCommand(updateSql, _connection);

                // Parameters
                updateCommand.Parameters.AddWithValue("@Name", student.Name);
                updateCommand.Parameters.AddWithValue("@Surname", student.Surname);
                updateCommand.Parameters.AddWithValue("@ParentName", student.ParentName);
                updateCommand.Parameters.AddWithValue("@JMBG", student.JMBG);
                updateCommand.Parameters.AddWithValue("@Phone", student.Phone);
                updateCommand.Parameters.AddWithValue("@Address", student.Address);
                updateCommand.Parameters.AddWithValue("@Email", student.Email);
                updateCommand.Parameters.AddWithValue("@Barcode", student.Barcode);

                _connection.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteStudent(Student student)
        {
            try
            {

                string deleteSql = "DELETE FROM Students WHERE Barcode = @Barcode";

                MySqlCommand deleteCommand = new MySqlCommand(deleteSql, _connection);

                deleteCommand.Parameters.AddWithValue("@Barcode", student.Barcode);

                _connection.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion

        #region Employee methods

        public void CreateNewEmployee(Employee employee)
        {
            try
            {
                string insertSql = "INSERT INTO Employees " +
                    "(Name, Surname, ParentName, JMBG, Phone, Address, Email, Barcode) " +
                    "VALUES (@Name, @Surname, @ParentName, @JMBG, @Phone, @Address, @Email, @Barcode)";


                MySqlCommand insertCommand = new MySqlCommand(insertSql, _connection);

                // Parameters
                insertCommand.Parameters.AddWithValue("@Name", employee.Name);
                insertCommand.Parameters.AddWithValue("@Surname", employee.Surname);
                insertCommand.Parameters.AddWithValue("@ParentName", employee.ParentName);
                insertCommand.Parameters.AddWithValue("@JMBG", employee.JMBG);
                insertCommand.Parameters.AddWithValue("@Phone", employee.Phone);
                insertCommand.Parameters.AddWithValue("@Address", employee.Address);
                insertCommand.Parameters.AddWithValue("@Email", employee.Email);
                insertCommand.Parameters.AddWithValue("@Barcode", employee.Barcode);

                _connection.Open();
                int rowsAffected = insertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public Employee FindEmployeeByBarcode(string barcode)
        {
            Employee employee = null;
            try
            {
                string selectSql = "SELECT * FROM Employees WHERE Barcode = @Barcode";

                MySqlCommand selectCommand = new MySqlCommand(selectSql, _connection);

                selectCommand.Parameters.AddWithValue("@Barcode", barcode);

                _connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    employee = new Employee
                    {
                        Name = reader.GetString("Name"),
                        Surname = reader.GetString("Surname"),
                        ParentName = reader.GetString("ParentName"),
                        JMBG = reader.GetString("JMBG"),
                        Phone = reader.GetString("Phone"),
                        Address = reader.GetString("Address"),
                        Email = reader.GetString("Email"),
                        Barcode = reader.GetString("Barcode")
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return employee;
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                string updateSql = "UPDATE Employees SET " +
                    "Name = @Name, " +
                    "Surname = @Surname, " +
                    "ParentName = @ParentName, " +
                    "JMBG = @JMBG, " +
                    "Phone = @Phone, " +
                    "Address = @Address, " +
                    "Email = @Email, " +
                    "WHERE Barcode = @Barcode";

                MySqlCommand updateCommand = new MySqlCommand(updateSql, _connection);

                // Parameters
                updateCommand.Parameters.AddWithValue("@Name", employee.Name);
                updateCommand.Parameters.AddWithValue("@Surname", employee.Surname);
                updateCommand.Parameters.AddWithValue("@ParentName", employee.ParentName);
                updateCommand.Parameters.AddWithValue("@JMBG", employee.JMBG);
                updateCommand.Parameters.AddWithValue("@Phone", employee.Phone);
                updateCommand.Parameters.AddWithValue("@Address", employee.Address);
                updateCommand.Parameters.AddWithValue("@Email", employee.Email);
                updateCommand.Parameters.AddWithValue("@Barcode", employee.Barcode);

                _connection.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteEmployee(Employee employee)
        {
            try
            {

                string deleteSql = "DELETE FROM Employees WHERE Barcode = @Barcode";

                MySqlCommand deleteCommand = new MySqlCommand(deleteSql, _connection);

                deleteCommand.Parameters.AddWithValue("@Barcode", employee.Barcode);

                _connection.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion

        #region Book methods

        public void CreateNewBook(Book book)
        {
            try
            {
                string insertSql = "INSERT INTO Books " +
                    "(Title, Description, Author, ISBN, NumberOfCopies, NumberOfAvailableCopies, Barcode) " +
                    "VALUES (@Title, @Description, @Author, @ISBN, @NumberOfCopies, @NumberOfAvailableCopies, @Barcode)";
                MySqlCommand insertCommand = new MySqlCommand(insertSql, _connection);
                // Parameters
                insertCommand.Parameters.AddWithValue("@Title", book.Title);
                insertCommand.Parameters.AddWithValue("@Description", book.Description);
                insertCommand.Parameters.AddWithValue("@Author", book.Author);
                insertCommand.Parameters.AddWithValue("@ISBN", book.ISBN);
                insertCommand.Parameters.AddWithValue("@NumberOfCopies", book.NumberOfCopies);
                insertCommand.Parameters.AddWithValue("@NumberOfAvailableCopies", book.NumberOfAvailableCopies);
                insertCommand.Parameters.AddWithValue("@Barcode", book.Barcode);
                _connection.Open();
                int rowsAffected = insertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateBook(Book book)
        {
            try
            {
                string updateSql = "UPDATE Books SET " +
                    "Title = @Title, " +
                    "Description = @Description, " +
                    "Author = @Author, " +
                    "ISBN = @ISBN, " +
                    "NumberOfCopies = @NumberOfCopies, " +
                    "NumberOfAvailableCopies = @NumberOfAvailableCopies, " +
                    "Barcode = @Barcode " +
                    "WHERE Barcode = @Barcode";

                MySqlCommand updateCommand = new MySqlCommand(updateSql, _connection);

                // Parameters
                updateCommand.Parameters.AddWithValue("@Title", book.Title);
                updateCommand.Parameters.AddWithValue("@Description", book.Description);
                updateCommand.Parameters.AddWithValue("@Author", book.Author);
                updateCommand.Parameters.AddWithValue("@ISBN", book.ISBN);
                updateCommand.Parameters.AddWithValue("@NumberOfCopies", book.NumberOfCopies);
                updateCommand.Parameters.AddWithValue("@NumberOfAvailableCopies", book.NumberOfAvailableCopies);
                updateCommand.Parameters.AddWithValue("@Barcode", book.Barcode);

                _connection.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteBook(Book book)
        {
            try
            {
                string deleteSql = "DELETE FROM Books WHERE Barcode = @Barcode";

                MySqlCommand deleteCommand = new MySqlCommand(deleteSql, _connection);

                deleteCommand.Parameters.AddWithValue("@Id", book.Barcode);

                _connection.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<Book>? FindMatchingBooks(string searchString)
        {
            List<Book> books = new List<Book>();
            if (searchString.Count() == 0 || searchString == null) return books;

            string[] keywords = searchString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string sqlQuery = "SELECT * FROM Books " +
                "WHERE " +
                "Title LIKE CONCAT('%', @Keyword0, '%') OR " +
                "Author LIKE CONCAT('%', @Keyword0, '%') OR " +
                "ISBN LIKE CONCAT('%', @Keyword0, '%') OR " +
                "Barcode LIKE CONCAT('%', @Keyword0, '%')";

            for (int i = 1; i < keywords.Length; i++)
            {
                sqlQuery += " OR " +
                "Title LIKE CONCAT('%', " + keywords[i] + ", '%') OR " +
                "Author LIKE CONCAT('%', " + keywords[i] + ", '%') OR " +
                "ISBN LIKE CONCAT('%', " + keywords[i] + ", '%') OR " +
                "Barcode LIKE CONCAT('%', " + keywords[i] + ", '%')";
            }

            sqlQuery += ";";

            try
            {
                MySqlCommand command = new MySqlCommand(sqlQuery, _connection);

                for (int i = 0; i < keywords.Length; i++)
                {
                    command.Parameters.AddWithValue("@Keyword" + i, keywords[i]);
                }

                _connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book
                    {
                        Title = reader.GetString("Title"),
                        Description = reader.GetString("Description"),
                        Author = reader.GetString("Author"),
                        ISBN = reader.GetString("ISBN"),
                        NumberOfCopies = reader.GetInt32("NumberOfCopies"),
                        NumberOfAvailableCopies = reader.GetInt32("NumberOfAvailableCopies"),
                        Barcode = reader.GetString("Barcode")
                    };
                    books.Add(book);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return books;
        }

        public Book FindBookByBarcode(string barcode)
        {
            Book book = null;
            try
            {
                string selectSql = "SELECT * FROM Books WHERE Barcode = @Barcode";

                MySqlCommand selectCommand = new MySqlCommand(selectSql, _connection);

                selectCommand.Parameters.AddWithValue("@Barcode", barcode);

                _connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {
                    book = new Book
                    {
                        Title = reader.GetString("Title"),
                        Description = reader.GetString("Description"),
                        Author = reader.GetString("Author"),
                        ISBN = reader.GetString("ISBN"),
                        NumberOfCopies = reader.GetInt32("NumberOfCopies"),
                        NumberOfAvailableCopies = reader.GetInt32("NumberOfAvailableCopies"),
                        Barcode = reader.GetString("Barcode")
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return book;
        }

        #endregion

        #region Transaction methods
        public void CreateNewTransaction(Transaction transaction)
        {
            try
            {
                string insertSql = "INSERT INTO Transactions " +
                    "(Id, BookBarcode, MemberBarcode, BorrowDate, DueDate, ReturnDate) " +
                    "VALUES (@Id, @BookBarcode, @MemberBarcode, @BorrowDate, @DueDate, @ReturnDate)";
                MySqlCommand insertCommand = new MySqlCommand(insertSql, _connection);

                Debug.WriteLine(transaction.Id.ToString());
                // Parameters
                insertCommand.Parameters.AddWithValue("@Id", transaction.Id.ToString());
                insertCommand.Parameters.AddWithValue("@BookBarcode", transaction.BookBarcode);
                insertCommand.Parameters.AddWithValue("@MemberBarcode", transaction.MemberBarcode);
                insertCommand.Parameters.AddWithValue("@BorrowDate", transaction.BorrowDate);
                insertCommand.Parameters.AddWithValue("@DueDate", transaction.DueDate);
                insertCommand.Parameters.AddWithValue("@ReturnDate", transaction.ReturnDate.HasValue ? (object)transaction.ReturnDate.Value : DBNull.Value);

                _connection.Open();
                int rowsAffected = insertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateTransaction(Transaction transaction)
        {
            try
            {
                string updateSql = "UPDATE Transactions SET " +
                    "BookBarcode = @BookBarcode, " +
                    "MemberBarcode = @MemberBarcode, " +
                    "BorrowDate = @BorrowDate, " +
                    "DueDate = @DueDate, " +
                    "ReturnDate = @ReturnDate " +
                    "WHERE Id = @Id";
                MySqlCommand updateCommand = new MySqlCommand(updateSql, _connection);

                // Parameters
                updateCommand.Parameters.AddWithValue("@BookBarcode", transaction.BookBarcode);
                updateCommand.Parameters.AddWithValue("@MemberBarcode", transaction.MemberBarcode);
                updateCommand.Parameters.AddWithValue("@BorrowDate", transaction.BorrowDate);
                updateCommand.Parameters.AddWithValue("@DueDate", transaction.DueDate);
                updateCommand.Parameters.AddWithValue("@ReturnDate", transaction.ReturnDate.HasValue ? (object)transaction.ReturnDate.Value : DBNull.Value);
                updateCommand.Parameters.AddWithValue("@Id", transaction.Id);

                _connection.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public Transaction FindTransaction(string bookBarcode, string memberBarcode)
        {
            Transaction transaction = null;
            try
            {
                string selectSql = "SELECT * FROM Transactions WHERE BookBarcode = @BookBarcode AND MemberBarcode = @MemberBarcode AND ReturnDate IS NULL;";
                MySqlCommand selectCommand = new MySqlCommand(selectSql, _connection);

                selectCommand.Parameters.AddWithValue("@BookBarcode", bookBarcode);
                selectCommand.Parameters.AddWithValue("@MemberBarcode", memberBarcode);

                _connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {
                    transaction = new Transaction
                    {
                        Id = Guid.Parse(reader.GetString("Id")),
                        BookBarcode = reader.GetString("BookBarcode"),
                        MemberBarcode = reader.GetString("MemberBarcode"),
                        BorrowDate = reader.GetDateTime("BorrowDate"),
                        DueDate = reader.GetDateTime("DueDate"),
                        ReturnDate = reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ? null : (DateTime?)reader.GetDateTime("ReturnDate")
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                OnQueryFailed(ex.Message);
                _logger.LogError("A MySql error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return transaction;
        }

        #endregion

        protected void OnQueryFailed(string message)
        {
            QueryFailed?.Invoke(this, new MessengerEventArgs(message));
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
