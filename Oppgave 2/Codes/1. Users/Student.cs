using Obligatorisk_Oppgave_1_Universitetssystem._2._Course;
using System;

namespace Obligatorisk_Oppgave_1_Universitetssystem._1._User
{
    internal class Student : Users // Student-klassen arver fra Users-klassen, som inneholder felles egenskaper for alle brukere.
    {
// ================================================
//    ----------- STUDENT PROPERTIES -----------
// ================================================
        public uint StudentID { get; init; } = (uint)Random.Shared.Next(1, 9999);
        public List<Course> EnrolledCourses { get; set; } = new List<Course>();

        // navn og e-post arves fra Users-klassen, så de trenger ikke å defineres her igjen.

// ================================================
//    ----------- DEFAULT STUDENTS -----------
// ================================================
        public static List<Student> DefaultStudents()
        {
            return new List<Student>
            {
                new Student { Name = "Saylor Sinclair", Email = "Saylor.Sin@mail.com" },
                new Student { Name = "Emerson Reed", Email = "Emerson.Ree@mail.com" }
            };
        }
    }
}
