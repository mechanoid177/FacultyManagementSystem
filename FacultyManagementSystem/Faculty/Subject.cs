using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.Faculty
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Employee HoldingProfessor { get; set; }
        public List<Employee> TeachingEmployees { get; set; }
        public int ECTS { get; set; }
        public Depatment Depatment { get; set; }
    }
}
