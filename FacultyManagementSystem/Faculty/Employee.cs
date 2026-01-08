using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.Faculty
{
    public class Employee : Student
    {
        public string Role {  get; set; }

        public double Salary { get; set; }
    }
}
