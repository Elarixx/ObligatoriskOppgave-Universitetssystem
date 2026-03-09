using System;
using Obligatorisk_Oppgave_1_Universitetssystem._1._User;

namespace Obligatorisk_Oppgave_1_Universitetssystem._2._Course
{
    internal class Course
    {
        public uint CourseCode { get; init; } = (uint)Random.Shared.Next();
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int AvailableSeats { get; set; }
        public bool plassTilStudent { get; set; } // Nytt navn på denne. om det er plass til studenten som melder seg på (sjekke kapasitet).
        

    }
}
