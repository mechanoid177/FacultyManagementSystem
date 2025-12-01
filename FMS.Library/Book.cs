using FMS.Faculty;

namespace FMS.Library
{
    // Check if number of borrowed copies is the same as number of MembersBorrowed
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int NumberOfCopies { get; set; }
        public int NumberOfAvailableCopies { get; set; }
        public string Barcode { get; set; }
    }
}
