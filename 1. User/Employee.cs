using System;

namespace Obligatorisk_Oppgave_1_Universitetssystem._1._User
{
    internal class Employee : Users // Employee-klassen arver fra Users-klassen, som inneholder felles egenskaper for alle brukere.
    {
// =========================================================
//      ----------- EMPLOYEE PROPERTIES -----------
// =========================================================
        public uint EmployeeID { get; init; } = (uint)Random.Shared.Next(1, 9999); // Random 1-4-sifret ID
        public string Position { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

        // Navn og Email arves fra Users.cs

// =================================================
//    ----------- DEFAULT Employees -----------
// =================================================
        public static List<Employee> DefaultEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Name = "Jaden Nora",
                    Email = "jadan.nor@employee.com",
                    Position = "Professor",
                    Department = "Computer Science"
                },
                new Employee
                {
                    Name = "Marcus Lee",
                    Email = "marcus.lee@employee.com",
                    Position = "Exchange Coordinator",
                    Department = "International Relations"
                }
            };
        }
    }
}
