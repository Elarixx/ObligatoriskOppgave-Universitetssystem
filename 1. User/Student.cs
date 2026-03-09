using Obligatorisk_Oppgave_1_Universitetssystem._2._Course;
using System;

namespace Obligatorisk_Oppgave_1_Universitetssystem._1._User
{
    internal class Student : Users // Student-klassen arver fra Users-klassen.
    {
        // Egenskaper med tilgangsmodifikatorer.
        public uint StudentID { get; init; } = (uint)Random.Shared.Next();
        public List<Course> EnrolledCourses { get; set; } = new List<Course>(); // Liste over kursene/emnene til studenten.
    }
}
