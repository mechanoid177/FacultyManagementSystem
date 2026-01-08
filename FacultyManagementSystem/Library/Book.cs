using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FacultyManagementSystem.Library
{
    public class Book
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public int NumberOfCopies { get; set; }

        public int NumberOfAvailableCopies { get; set; }

        public string Barcode { get; set; }

    }
}
