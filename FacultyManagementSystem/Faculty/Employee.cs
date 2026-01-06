using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyManagementSystem.Faculty
{
    public class Employee : Person
    {
        public string Role {  get; set; }

        public double Salary { get; set; }
    }
}
