using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.Faculty
{
    public class Depatment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Employee> Employees { get; set; }
        public Employee Head {  get; set; }
    }
}
