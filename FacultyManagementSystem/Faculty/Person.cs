using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FacultyManagementSystem.Faculty
{
    public abstract class Person
    {
        public Guid Id { get; private set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ParentName { get; set; }

        public string JMBG { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Barcode { get; set; }

        public PersonType Type { get; set; }
    }
}
