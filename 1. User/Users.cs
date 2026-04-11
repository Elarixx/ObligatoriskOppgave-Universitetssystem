using System;

namespace Obligatorisk_Oppgave_1_Universitetssystem._1._User
{
    internal abstract class Users
    {
    // ================================================
    //     ----------- USER PROPERTIES -----------
    // ================================================
        public string Name { get; set; } = string.Empty; // Navn på brukeren.
        public string Email { get; set; } = string.Empty; // E-post til brukeren.
    }

    internal static class UserActions
    {
// ================================================================================================
//          ------------------------ CREATE USER ------------------------
// ================================================================================================
    // Viser meny for å opprette student, utvekslingsstudent eller ansatt.
        public static void CreateUser( // Tar inn alle lister for å kunne legge til nye brukere.
            List<Student> allStudents,
            List<Exchange_Student> allExchangeStudents,
            List<Employee> allEmployees)
        {
            Console.Clear();
            Styles.WriteTitle("--- Create a new User ---");
            Console.WriteLine("\n[1] Student");
            Console.WriteLine("[2] Exchange Student");
            Console.WriteLine("[3] Employee");
            Console.Write("What type of user would you like to create?: ");
            string? userChoice = Console.ReadLine(); // Leser inn brukerens valg for hvilken type bruker som skal opprettes.

            switch (userChoice)
            {
                case "1":
                    CreateStudent(allStudents);
                    break;

                case "2":
                    CreateExchangeStudent(allStudents, allExchangeStudents);
                    break;

                case "3":
                    CreateEmployee(allEmployees);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid user type.");
                    break;
            }
        }
// ======================================================================
    //------------------------ CREATE STUDENT ------------------------\\
        private static void CreateStudent(List<Student> allStudents) // Tar inn studentlisten for å kunne legge til nye studenter.
        {
            Student newStudent = new Student(); // Lager nytt studentobjekt.

            Console.Write("Enter the student's name: ");
            newStudent.Name = Console.ReadLine() ?? string.Empty; // Bruker tom streng hvis input er null.
            Console.Write("Enter the student's email: ");
            newStudent.Email = Console.ReadLine() ?? string.Empty; // Bruker tom streng hvis input er null.

            allStudents.Add(newStudent); // Legger studenten til i listen.
            Console.WriteLine($"Student {newStudent.Name} (ID: {newStudent.StudentID}) has successfully been created.");
        }

// ================================================================================
    // ------------------------ CREATE EXCHANGE STUDENT ------------------------\\
        private static void CreateExchangeStudent( // Tar inn både student- og utvekslingsstudentlistene for å kunne legge til i begge.
            List<Student> allStudents,
            List<Exchange_Student> allExchangeStudents)
        {
            Exchange_Student newExchangeStudent = new Exchange_Student(); // Lager nytt utvekslingsstudentobjekt.

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
                return;
            }

            // Validering av datoer for utvekslingsperioden.
            Console.Write("Enter periode 'to' date (dd-mm-yyyy): ");
            if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly periodTo))
            {
                Console.WriteLine("Invalid date format. Please enter the date in dd-mm-yyyy format.");
                return;
            }

            if (periodTo < periodFrom) // Sjekker at sluttdato ikke er før startdato.
            {
                Console.WriteLine("Invalid date range. 'To' date must be after 'From' date.");
                return;
            }

            newExchangeStudent.PeriodFrom = periodFrom; // Setter startdato for utvekslingsperioden.
            newExchangeStudent.PeriodTo = periodTo; // Setter sluttdato for utvekslingsperioden.

            allExchangeStudents.Add(newExchangeStudent); // Legger til i egen liste for utvekslingsstudenter.
            allStudents.Add(newExchangeStudent); // Legger også til i felles studentliste.

            Console.WriteLine($"Exchange Student {newExchangeStudent.Name} (ID: {newExchangeStudent.StudentID}) has successfully been created.");
        }

// ========================================================================
    // ------------------------ CREATE EMPLOYEE ------------------------\\
        private static void CreateEmployee(List<Employee> allEmployees) // Tar inn ansattlisten for å kunne legge til nye ansatte.
        {
            Employee employee = new Employee(); // Lager nytt ansattobjekt.

            Console.Write("Enter the employee's name: ");
            employee.Name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the employee's email: ");
            employee.Email = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the employee's position: ");
            employee.Position = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the employee's department: ");
            employee.Department = Console.ReadLine() ?? string.Empty;

            allEmployees.Add(employee); // Legger ansatte til i listen.
            Console.WriteLine($"Employee {employee.Name} (ID: {employee.EmployeeID}) has successfully been created.");
        }
    }
}
