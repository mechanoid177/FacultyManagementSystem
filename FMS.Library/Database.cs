using FMS.Faculty;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace FMS.Library
{
    public class Database
    {
        private string _connectionString = "Server=localhost;Port=3306;Database=LibraryDatabase;User=root;Password=password;"; // Get from configuration
        private MySqlConnection _connection;

        public Database()
        {
            _connection = new MySqlConnection(_connectionString);
        }

        public void InsertNewMember(Person member)
        {
            try
            {
                string insertSql = "INSERT INTO Members " +
                    "(Name, Surname, ParentName, JMBG, Phone, Address, Email, Barcode) " +
                    "VALUES (@Name, @Surname, @ParentName, @JMBG, @Phone, @Address, @Email, @Barcode)";

                MySqlCommand insertCommand = new MySqlCommand(insertSql, _connection);

                // Parameters
                insertCommand.Parameters.AddWithValue("@Name", "John Doe");
                insertCommand.Parameters.AddWithValue("@Surname", "John Doe");
                insertCommand.Parameters.AddWithValue("@ParentName", "John Doe");
                insertCommand.Parameters.AddWithValue("@JMBG", "John Doe");
                insertCommand.Parameters.AddWithValue("@Phone", "John Doe");
                insertCommand.Parameters.AddWithValue("@Address", "John Doe");
                insertCommand.Parameters.AddWithValue("@Email", "John Doe");
                insertCommand.Parameters.AddWithValue("@Barcode", "John Doe");

                _connection.Open();
                int rowsAffected = insertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateMember(Person member)
        {
            try
            {
                string updateSql = "UPDATE Members SET " +
                    "Name = @Name, " +
                    "Surname = @Surname, " +
                    "ParentName = @ParentName, " +
                    "JMBG = @JMBG, " +
                    "Phone = @Phone, " +
                    "Address = @Address, " +
                    "Email = @Email, " +
                    "Barcode = @Barcode " +
                    "WHERE Id = @Id";
                MySqlCommand updateCommand = new MySqlCommand(updateSql, _connection);

                // Parameters
                updateCommand.Parameters.AddWithValue("@Name", member.Name);
                updateCommand.Parameters.AddWithValue("@Surname", member.Surname);
                updateCommand.Parameters.AddWithValue("@ParentName", member.ParentName);
                updateCommand.Parameters.AddWithValue("@JMBG", member.JMBG);
                updateCommand.Parameters.AddWithValue("@Phone", member.Phone);
                updateCommand.Parameters.AddWithValue("@Address", member.Address);
                updateCommand.Parameters.AddWithValue("@Email", member.Email);
                updateCommand.Parameters.AddWithValue("@Barcode", member.Barcode);
                updateCommand.Parameters.AddWithValue("@Id", member.Id);

                _connection.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteMember(Person member)
        {
            try
            {
                string deleteSql = "DELETE FROM Members WHERE Id = @Id";

                MySqlCommand deleteCommand = new MySqlCommand(deleteSql, _connection);

                deleteCommand.Parameters.AddWithValue("@Id", member.Id);

                _connection.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void InsertNewBook(Book book)
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
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
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
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
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
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void InsertNewTransaction(Transaction transaction)
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
                Debug.WriteLine("An error occurred: " + ex.Message);
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
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
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
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
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return book;
        }

        public Person FindMemberByBarcode(string barcode)
        {
            Person member = null;
            try
            {
                string sqlQuery = "SELECT * FROM Members WHERE Barcode = @Barcode";

                MySqlCommand command = new MySqlCommand(sqlQuery, _connection);

                command.Parameters.AddWithValue("@Barcode", barcode);

                _connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string Name = reader.GetString("Name");
                    string Surname = reader.GetString("Surname");
                    string ParentName = reader.GetString("ParentName");
                    string JMBG = reader.GetString("JMBG");
                    string Phone = reader.GetString("Phone");
                    string Address = reader.GetString("Address");
                    string Email = reader.GetString("Email");
                    string Barcode = reader.GetString("Barcode");
                    PersonType type;
                    _ = Enum.TryParse(reader.GetString("Type"), out type); // DO a check
                    switch (type)
                    {
                        case PersonType.Student:
                            member = new Student
                            {
                                Name = Name,
                                Surname = Surname,
                                ParentName = ParentName,
                                JMBG = JMBG,
                                Phone = Phone,
                                Address = Address,
                                Email = Email,
                                Barcode = Barcode,
                                Type = PersonType.Student
                            };
                            break;
                        case PersonType.Employee:
                            member = new Employee
                            {
                                Name = Name,
                                Surname = Surname,
                                ParentName = ParentName,
                                JMBG = JMBG,
                                Phone = Phone,
                                Address = Address,
                                Email = Email,
                                Barcode = Barcode,
                                Type = PersonType.Employee
                            };
                            break;
                        default:
                            // Handle unknown type
                            break;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return member;
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
                Debug.WriteLine("An error occurred: " + ex.Message);
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return transaction;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                string selectSql = "SELECT * FROM Books";
                MySqlCommand selectCommand = new MySqlCommand(selectSql, _connection);

                _connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();

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
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return books;
        }

        public List<Person> GetAllMembers()
        {
            List<Person> members = new List<Person>();
            try
            {
                string selectSql = "SELECT * FROM Members";
                MySqlCommand selectCommand = new MySqlCommand(selectSql, _connection);
                _connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    string Name = reader.GetString("Name");
                    string Surname = reader.GetString("Surname");
                    string ParentName = reader.GetString("ParentName");
                    string JMBG = reader.GetString("JMBG");
                    string Phone = reader.GetString("Phone");
                    string Address = reader.GetString("Address");
                    string Email = reader.GetString("Email");
                    string Barcode = reader.GetString("Barcode");
                    PersonType type;
                    _ = Enum.TryParse(reader.GetString("Type"), out type); // DO a check

                    Person member;

                    switch(type)
                    {
                        case PersonType.Student:
                            member = new Student();
                            member.Name = Name;
                            member.Surname = Surname;
                            member.ParentName = ParentName;
                            member.JMBG = JMBG;
                            member.Phone = Phone;
                            member.Address = Address;
                            member.Email = Email;
                            member.Barcode = Barcode;
                            member.Type = PersonType.Student;
                            break;
                        case PersonType.Employee:
                            member = new Employee();
                            member.Name = Name;
                            member.Surname = Surname;
                            member.ParentName = ParentName;
                            member.JMBG = JMBG;
                            member.Phone = Phone;
                            member.Address = Address;
                            member.Email = Email;
                            member.Barcode = Barcode;
                            member.Type = PersonType.Employee;
                            break;
                        default:
                            // Handle unknown type
                            continue;
                    }

                    members.Add(member);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return members;
        }

        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            try
            {
                string selectSql = "SELECT * FROM Transactions";
                MySqlCommand selectCommand = new MySqlCommand(selectSql, _connection);
                _connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    string Id = reader.GetString("Id");
                    string BookBarcode = reader.GetString("BookBarcode");
                    string MemberBarcode = reader.GetString("MemberBarcode");
                    DateTime BorrowDate = reader.GetDateTime("BorrowDate");
                    DateTime DueDate = reader.GetDateTime("DueDate");
                    DateTime? ReturnDate = reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ? null : (DateTime)reader.GetDateTime("ReturnDate");

                    Transaction transaction = new Transaction
                    {
                        Id = Guid.Parse(Id),
                        BookBarcode = BookBarcode,
                        MemberBarcode = MemberBarcode,
                        BorrowDate = BorrowDate,
                        DueDate = DueDate,
                        ReturnDate = ReturnDate
                    };

                    transactions.Add(transaction);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return transactions;
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
                "Title LIKE CONCAT('%', "+ keywords[i] +", '%') OR " +
                "Author LIKE CONCAT('%', "+ keywords[i] +", '%') OR " +
                "ISBN LIKE CONCAT('%', "+ keywords[i] +", '%') OR " +
                "Barcode LIKE CONCAT('%', "+ keywords[i] +", '%')";
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
                Debug.WriteLine("An error occurred: " + ex.Message);
                // Handle exception (e.g., log it)
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return books;
        }
    }
}
