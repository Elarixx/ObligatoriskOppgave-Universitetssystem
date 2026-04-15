using System;

namespace Obligatorisk_Oppgave_1_Universitetssystem._1._User
{
    internal class Exchange_Student : Student // Exchange_Student-klassen arver fra Student-klassen, som igjen arver fra Users-klassen. Dette betyr at en utvekslingsstudent har alle egenskapene til en vanlig student, i tillegg til spesifikke egenskaper for utvekslingsstudenter.
    {
// =========================================================
//    ----------- EXCHANGE STUDENT PROPERTIES -----------
// =========================================================
        public string homeUniverity { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public DateOnly PeriodFrom { get; set; }
        public DateOnly PeriodTo { get; set; }

        // Navn og Email arves fra Users.cs

// =========================================================
//    ----------- DEFAULT EXCHANGE STUDENTS -----------
// =========================================================
    // Returnerer standard utvekslingsstudenter for oppstart/testing.
        public static List<Exchange_Student> DefaultExchangeStudents() 
        {
            return new List<Exchange_Student> // Lager en liste med to standard utvekslingsstudenter
            {
                new Exchange_Student
                {
                    Name = "Drake Washington",
                    Email = "drake.wash@exhange.edu",
                    homeUniverity = "University of Australia",
                    Country = "Australia",
                    PeriodFrom = new DateOnly(2028, 8, 15),
                    PeriodTo = new DateOnly(2028, 12, 20)
                },
                new Exchange_Student
                {
                    Name = "Sarai Monzon",
                    Email = "sarai.mon@exhance.edu",
                    homeUniverity = "University of Japan",
                    Country = "Japan",
                    PeriodFrom = new DateOnly(2029, 1, 10),
                    PeriodTo = new DateOnly(2029, 5, 30)
                }
            };
        }
    }
}
