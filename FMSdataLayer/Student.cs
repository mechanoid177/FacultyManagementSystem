using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Faculty
{
    internal class Student : Person
    {
        public List<Subject> AppointedSubjects {  get; set; }
        public List<Subject> PassedExams { get; set; }
        public int CurrentECTS {  get; set; }
        public DateTime YearOfEnrollment {  get; set; }
        public DateTime YearOfDisenrolment { get; set; }
    }
}
