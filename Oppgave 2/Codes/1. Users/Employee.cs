using System;
using Obligatorisk_Oppgave_1_Universitetssystem._2._Course;
using Obligatorisk_Oppgave_1_Universitetssystem._3._Library;

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
                    Department = "Computer Science",

                    // <>-<>-<>-- Oppgave 2 --<>-<>-<>
                        Username = "JadenNora",
                        Password = "Nora_123",
                        Role = UserRole.Teacher
                    // <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>
                },
                new Employee
                {
                    Name = "Marcus Lee",
                    Email = "marcus.lee@employee.com",
                    Position = "Librarian",
                    Department = "Library",

                    // <>-<>-<>-- Oppgave 2 --<>-<>-<>
                        Username = "MarcusLee",
                        Password = "Lee_123",
                        Role = UserRole.Librarian
                    //<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>
                }
            };
        }
// ==================================================================================
//                  ----------- OPPGAVE 2: EMPLOYEE MENU CHOICE -----------                         
// ==================================================================================
    // Håndtere menyvalg for Teacher og Librarian.
        public static bool HandleMenuChoice(
            Users loggedInUser,
            string userChoice,
            List<Course> allCourses,
            List<Student> allStudents,
            List<Employee> allEmployees,
            List<Books> allBooks,
            List<Loan> allLoans,
            ref bool running)
        {
            switch (loggedInUser.Role)
            {
                // Håndtere menyvalg for Teacher.
                case UserRole.Teacher:
                    switch (userChoice)
                    {
                        case "1":
                            Course.CreateCourse(allCourses);
                            return true;

                        case "2":
                            Course.SearchCourse(allCourses);
                            return true;

                        case "3":
                            Books.SearchBooks(allBooks);
                            return true;

                        case "4":
                            Books.BorrowBooks(allBooks, loggedInUser, allLoans);
                            return true;

                        case "5":
                            Books.ReturnBooks(allLoans, loggedInUser);
                            return true;

                        case "6":
                            Course.RegisterSyllabus(allCourses);
                            return true;

                        case "7":
                            Course.SetStudentGrade(allCourses);
                            return true;

                        case "0":
                            running = false;
                            return false;

                        default:
                            Console.WriteLine("Invalid choice.");
                            return false;
                    }

                // Håndtere menyvalg for Librarian.
                case UserRole.Librarian:
                    switch (userChoice)
                    {
                        case "1":
                            Books.CreateBook(allBooks);
                            return true;

                        case "2":
                            Books.ActiveLoans(allLoans);
                            return false;

                        case "3":
                            Books.LoansHistory(allLoans);
                            return false;

                        case "0":
                            running = false;
                            return false;

                        default:
                            Console.WriteLine("Invalid choice.");
                            return false;
                    }

                default:
                    Console.WriteLine("Invalid role.");
                    return false;
            }
        }
    }
}
