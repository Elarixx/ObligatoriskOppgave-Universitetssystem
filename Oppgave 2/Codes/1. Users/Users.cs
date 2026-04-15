using Obligatorisk_Oppgave_1_Universitetssystem.Codes;
using System;

// Hva gjør dette? Liste med faste valg.
// <>-<>-<>-- Oppgave 2 --<>-<>-<>
    internal enum UserRole
    {
        Student,
        ExchangeStudent,
        Teacher,
        Librarian
    }
// <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>

namespace Obligatorisk_Oppgave_1_Universitetssystem._1._User
{
    internal abstract class Users
    {
    // ================================================
    //     ----------- USER PROPERTIES -----------
    // ================================================
        public string Name { get; set; } = string.Empty; // Navn på brukeren.
        public string Email { get; set; } = string.Empty; // E-post til brukeren.

        // <>-<>-<>-- Oppgave 2 --<>-<>-<>
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public UserRole Role { get; set; }
        // <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>
    }

    internal static class UserActions
    {
// ================================================================================================
//          ------------------------ CREATE USER ------------------------
// ================================================================================================
// Lager ny bruker og returnerer den, men legger den ikke inn i listene enda.
public static Users? CreateUser()
{
    Console.Clear();
    Styles.WriteTitle("--- Create a new User ---");
    Console.WriteLine("\n[1] Student");
    Console.WriteLine("[2] Exchange Student");
    Console.WriteLine("[3] Employee");
    Console.Write("What type of user would you like to create?: ");
    string? userChoice = Console.ReadLine();

    switch (userChoice)
    {
        case "1":
            return CreateStudent();

        case "2":
            return CreateExchangeStudent();

        case "3":
            return CreateEmployee();

        default:
            Console.WriteLine("Invalid choice. Please select a valid user type.");
            return null;
    }
}

// ======================================================================
    //------------------------ CREATE STUDENT ------------------------\\
        private static Student CreateStudent()
        {
            Student newStudent = new Student();

            Console.Write("Enter the student's name: ");
            newStudent.Name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the student's email: ");
            newStudent.Email = Console.ReadLine() ?? string.Empty;

            Console.Write("Choose username: ");
            newStudent.Username = Console.ReadLine() ?? string.Empty;

            Console.Write("Choose password: ");
            newStudent.Password = Console.ReadLine() ?? string.Empty;

            newStudent.Role = UserRole.Student;

            return newStudent;
        }

// ================================================================================
    // ------------------------ CREATE EXCHANGE STUDENT ------------------------\\
        private static Exchange_Student? CreateExchangeStudent()
        {
            Exchange_Student newExchangeStudent = new Exchange_Student();

            Console.Write("Enter the exchange student's name: ");
            newExchangeStudent.Name = Console.ReadLine() ?? string.Empty; 

            Console.Write("Enter the exchange student's email: ");
            newExchangeStudent.Email = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the exchange student's home university: ");
            newExchangeStudent.homeUniverity = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the exchange student's country: ");
            newExchangeStudent.Country = Console.ReadLine() ?? string.Empty;

            // Validering av datoer for utvekslingsperioden.
            Console.Write("Enter periode 'from' date (dd-mm-yyyy): ");
            if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly periodFrom))
            {
                Console.WriteLine("Invalid date format. Please enter the date in dd-mm-yyyy format.");
                return null;
            }

            // Validering av datoer for utvekslingsperioden.
            Console.Write("Enter periode 'to' date (dd-mm-yyyy): ");
            if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly periodTo))
            {
                Console.WriteLine("Invalid date format. Please enter the date in dd-mm-yyyy format.");
                return null;
            }

            // Sjekker at 'to' datoen er etter 'from' datoen.
            if (periodTo < periodFrom) 
            {
                Console.WriteLine("Invalid date range. 'To' date must be after 'From' date.");
                return null;
            }

            // Sette periodene for utvekslingsstudenten.
            newExchangeStudent.PeriodFrom = periodFrom;
            newExchangeStudent.PeriodTo = periodTo;

            Console.Write("Choose username: ");
            newExchangeStudent.Username = Console.ReadLine() ?? string.Empty;
            
            Console.Write("Choose password: ");
            newExchangeStudent.Password = Console.ReadLine() ?? string.Empty;

            // Sette rollen til ExchangeStudent.
            newExchangeStudent.Role = UserRole.ExchangeStudent;

            return newExchangeStudent;
        }

// ========================================================================
    // ------------------------ CREATE EMPLOYEE ------------------------\\
        private static Employee CreateEmployee()
        {
            Employee employee = new Employee();

            Console.Write("Enter the employee's name: ");
            employee.Name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the employee's email: ");
            employee.Email = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the employee's position: ");
            employee.Position = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the employee's department: ");
            employee.Department = Console.ReadLine() ?? string.Empty;

            // <>-<>-<>-- Oppgave 2 --<>-<>-<>
                Console.Write("Choose username: ");
                employee.Username = Console.ReadLine() ?? string.Empty;
                Console.Write("Choose password: ");
                employee.Password = Console.ReadLine() ?? string.Empty;

                // Valg av rolle for ansatte.
                if (employee.Position.Equals("Librarian", StringComparison.OrdinalIgnoreCase))
                {
                    employee.Role = UserRole.Librarian;
                }
                else
                {
                    employee.Role = UserRole.Teacher; // Antar at alle andre ansatte er lærere.
                }
            // <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>

            return employee;
        }
    }
}
