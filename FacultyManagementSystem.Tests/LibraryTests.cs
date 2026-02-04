using FacultyManagementSystem.Database.Interfaces;
using FacultyManagementSystem.Faculty;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;

namespace FacultyManagementSystem.Library.Tests
{
    [TestFixture]
    public class LibraryTests
    {
        private Library _library;
        private Moq.Mock<IDatabaseManager> _mockDatabaseManager;
        private Moq.Mock<ILogger<Library>> _mockLogger;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockDatabaseManager = new Moq.Mock<IDatabaseManager>();
            _mockLogger = new Moq.Mock<ILogger<Library>>();



            _library = new Library(_mockDatabaseManager.Object, _mockLogger.Object);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _library.Dispose();
        }

        #region IssueBook Tests
        [Test]
        public void IssueBook_InvalidBookBarcode_ReturnsFalse()
        {
            //Arrange
            Book book = null;
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode("")).Returns(book);

            //Act 
            bool result = _library.IssueBook("", "");

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IssueBook_NumberOfAvailableCopiesIsZero_ReturnsFalse()
        {
            //Arrange
            Book book = new Book
            {
                Barcode = "12345",
                Title = "Test Book",
                Author = "Test Author",
                NumberOfAvailableCopies = 0
            };
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode("12345")).Returns(book);

            //Act 
            bool result = _library.IssueBook("12345", "student1");

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IssueBook_InvalidStudenAndEmployee_ReturnsFalse()
        {
            //Arrange
            Book book = new Book
            {
                Barcode = "123456",
                Title = "Test Book",
                Author = "Test Author",
                NumberOfAvailableCopies = 1
            };
            Student student = null;
            Employee employee = null;
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode("123456")).Returns(book);
            _mockDatabaseManager.Setup(db => db.FindStudentByBarcode("")).Returns(student);
            _mockDatabaseManager.Setup(db => db.FindEmployeeByBarcode("")).Returns(employee);

            //Act
            bool result = _library.IssueBook("123456", "");

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IssueBook_ValidBookAndStudent_ReturnsTrue()
        {
            //Arrange
            Book book = new Book
            {
                Barcode = "123456",
                Title = "Test Book",
                Author = "Test Author",
                NumberOfAvailableCopies = 1
            };
            Student student = new Student
            {
                Barcode = "654321",
                Name = "Test Student"
            };
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode("123456")).Returns(book);
            _mockDatabaseManager.Setup(db => db.FindStudentByBarcode("654321")).Returns(student);

            //Act
            bool result = _library.IssueBook("123456", "654321");

            //Assert
            Assert.That(result, Is.True);
        }
        #endregion

        #region ReturnBook Tests
        [Test]
        public void ReturnBook_InvalidBookBarcode_ReturnsFalse()
        {
            //Arrange
            Book book = null;
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode("")).Returns(book);

            //Act 
            bool result = _library.ReturnBook("", "");

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnBook_InvalidStudenAndEmployee_ReturnsFalse()
        {
            //Arrange
            Book book = new Book
            {
                Barcode = "123456",
                Title = "Test Book",
                Author = "Test Author",
                NumberOfAvailableCopies = 1
            };
            Student student = null;
            Employee employee = null;
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode("123456")).Returns(book);
            _mockDatabaseManager.Setup(db => db.FindStudentByBarcode("")).Returns(student);
            _mockDatabaseManager.Setup(db => db.FindEmployeeByBarcode("")).Returns(employee);

            //Act
            bool result = _library.ReturnBook("123456", "");

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnBook_InvalidTransaction_ReturnsFalse()
        {
            //Arrange
            Book book = new Book
            {
                Barcode = "123456",
                Title = "Test Book",
                Author = "Test Author",
                NumberOfAvailableCopies = 1
            };
            Student student = new Student
            {
                Barcode = "654321",
                Name = "Test Student"
            };
            Transaction transaction = null;
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode("123456")).Returns(book);
            _mockDatabaseManager.Setup(db => db.FindStudentByBarcode("654321")).Returns(student);
            _mockDatabaseManager.Setup(db => db.FindTransaction("123456", "654321")).Returns(transaction);

            //Act
            bool result = _library.ReturnBook("123456", "654321");

            //Assert
            Assert.That(result, Is.False);
        }

        public void ReturnBook_ValidBookAndStudent_ReturnsTrue()
        {
            //Arrange
            Book book = new Book
            {
                Barcode = "123456",
                Title = "Test Book",
                Author = "Test Author",
                NumberOfAvailableCopies = 1
            };
            Student student = new Student
            {
                Barcode = "654321",
                Name = "Test Student"
            };
            Transaction transaction = new Transaction
            {
                BookBarcode = "123456",
                MemberBarcode = "654321",
                BorrowDate = DateTime.Now.AddDays(-5),
                DueDate = DateTime.Now.AddDays(5)
            };
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode("123456")).Returns(book);
            _mockDatabaseManager.Setup(db => db.FindStudentByBarcode("654321")).Returns(student);
            _mockDatabaseManager.Setup(db => db.FindTransaction("123456", "654321")).Returns(transaction);

            //Act
            bool result = _library.ReturnBook("123456", "654321");

            //Assert
            Assert.That(result, Is.True);
        }
        #endregion

        #region AddBook Tests

        [Test]
        public void AddBook_BarcodeIsNullOrEmpty_ReturnsFalse()
        {
            //Arrange
            string barcode = null;

            //Act
            bool result = _library.AddBook("Test Title", "Test Author", "Test Description", "Test ISBN", barcode, 5);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void AddBook_BookExists_ReturnsTrue()
        {
            //Arrange
            string barcode = "123456";
            Book existingBook = new Book { Barcode = barcode };
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode(barcode)).Returns(existingBook);

            //Act
            bool result = _library.AddBook("Test Title", "Test Author", "Test Description", "Test ISBN", barcode, 5);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void AddBook_NewBook_ReturnsTrue()
        {
            //Arrange
            string barcode = "123456";
            Book existingBook = null;
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode(barcode)).Returns(existingBook);

            //Act
            bool result = _library.AddBook("Test Title", "Test Author", "Test Description", "Test ISBN", barcode, 5);

            //Assert
            Assert.That(result, Is.True);
        }
        #endregion

        #region RemoveBook Tests

        [Test]
        public void RemoveBook_BookDoesNotExist_ReturnsFalse()
        {
            //Arrange
            string barcode = "123456";
            Book existingBook = null;
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode(barcode)).Returns(existingBook);

            //Act
            bool result = _library.RemoveBook(barcode);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void RemoveBook_BookExists_ReturnsTrue()
        {
            //Arrange
            string barcode = "123456";
            Book existingBook = new Book { Barcode = barcode };
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode(barcode)).Returns(existingBook);

            //Act
            bool result = _library.RemoveBook(barcode);

            //Assert
            Assert.That(result, Is.True);
        }

        #endregion

        #region RemoveBookCopy Tests
        
        [Test]
        public void RemoveBookCopy_BookDoesNotExist_ReturnsFalse()
        {
            //Arrange
            string barcode = "123456";
            Book existingBook = null;
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode(barcode)).Returns(existingBook);
            //Act
            bool result = _library.RemoveBookCopy(barcode);
            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void RemoveBookCopy_NumberOfAvailableCopiesIsZero_ReturnsFalse()
        {
            //Arrange
            string barcode = "123456";
            Book existingBook = new Book { Barcode = barcode, NumberOfAvailableCopies = 0 };
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode(barcode)).Returns(existingBook);

            //Act
            bool result = _library.RemoveBookCopy(barcode);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void RemoveBookCopy_BookExists_ReturnsTrue()
        {
            //Arrange
            string barcode = "123456";
            Book existingBook = new Book { Barcode = barcode, NumberOfAvailableCopies = 2 };
            _mockDatabaseManager.Setup(db => db.FindBookByBarcode(barcode)).Returns(existingBook);

            //Act
            bool result = _library.RemoveBookCopy(barcode);

            //Assert
            Assert.That(result, Is.True);
        }

        #endregion

        #region FindMatchingBooks Tests 

        [Test]
        public void FindMatchingBooks_NoMatchingBooks_ReturnsEmptyList()
        {
            //Arrange
            string searchTerm = "NonExistingBook";
            List<Book> emptyList = new List<Book>();
            _mockDatabaseManager.Setup(db => db.FindMatchingBooks(searchTerm)).Returns(emptyList);

            //Act
            var result = _library.SearchBooks(searchTerm);

            //Assert
            Assert.That(result, Is.Empty);
        }

        #endregion
    }
}
