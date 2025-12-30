using FMS.Faculty;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FMS.Library
{
    public class Book
    {
        
        public Guid Id { get; private set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public int NumberOfCopies { get; set; }

        public int NumberOfAvailableCopies { get; set; }

        public string Barcode { get; set; }

        public Book()
        {
            Id = Guid.NewGuid();
        }
    }
}
