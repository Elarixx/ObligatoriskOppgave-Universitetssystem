using Obligatorisk_Oppgave_1_Universitetssystem.Codes;
using Obligatorisk_Oppgave_1_Universitetssystem._3._Library;
using System;
using Obligatorisk_Oppgave_1_Universitetssystem._2._Course;
using System.Linq;

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
                // new Student { Name = "Saylor Sinclair", Email = "Saylor.Sin@mail.com" },
                new Student
                {
                    Name = "Saylor Sinclair",
                    Email = "Saylor.Sin@mail.com",

                    // <>-<>-<>-- Oppgave 2 --<>-<>-<>
                        Username = "SaylorSinclair",
                        Password = "Sinclair_123",
                        Role = UserRole.Student
                    // <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>
                },

                // new Student { Name = "Emerson Reed", Email = "Emerson.Ree@mail.com" }
                new Student
                {
                    Name = "Emerson Reed",
                    Email = "Emerson.Ree@mail.com",

                    // <>-<>-<>-- Oppgave 2 --<>-<>-<>
                        Username = "EmersonReed",
                        Password = "Reed_123",
                        Role = UserRole.Student
                    // <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>
                }
            };
        }

// ==================================================================================
//              ----------- OPPGAVE 2: STUDENT MENU CHOICE -----------
// ==================================================================================
    // Håndterer menyvalg for studentbrukere, inkludert kursregistrering, visning av kurs og karakterer, samt bibliotekfunksjoner.
        public static bool HandleMenuChoice(
            Users loggedInUser,
            string userChoice,
            List<Course> allCourses,
            List<Student> allStudents,
            List<Books> allBooks,
            List<Employee> allEmployees,
            List<Loan> allLoans,
            ref bool running)
        {
            switch (userChoice)
            {
                case "1":
                    Course.SignStudentUpOrOffCourse(allCourses, allStudents, loggedInUser);
                    return true;

                case "2":
                    Console.Clear();
                    Styles.WriteTitle("--- My Courses ---");
                    Student? selectedStudent = allStudents.Find(s => s.Username == loggedInUser.Username);

                    if (selectedStudent == null || selectedStudent.EnrolledCourses.Count == 0)
                        Console.WriteLine("You are not enrolled in any courses.");
                    else
                        foreach (Course course in selectedStudent.EnrolledCourses)
                            Console.WriteLine($"- {course.CourseName} ({course.CourseCode})");
                    return false;

                case "3":
                    ShowMyGrades(allCourses, loggedInUser);
                    return false;

                case "4":
                    Books.SearchBooks(allBooks);
                    return true;

                case "5":
                    Books.BorrowBooks(allBooks, loggedInUser, allLoans);
                    return true;

                case "6":
                    Books.ReturnBooks(allLoans, loggedInUser);
                    return true;

                case "0":
                    running = false;
                    return false;

                default:
                    Console.WriteLine("Invalid choice.");
                    return false;
            }
        }

// ==================================================================================
//                  ----------- SHOW MY GRADES (Oppgave 2)-----------
// ==================================================================================
    // Viser karakterene til innlogget student i kurs der karakter er satt.
        private static void ShowMyGrades(List<Course> allCourses, Users loggedInUser)
        {
            Console.Clear();
            Styles.WriteTitle("--- My Grades ---");

            // Finn den innloggede studenten basert på brukernavn, og sjekk karakterer i alle kurs.
            Student? selectedStudent = allCourses
                .SelectMany(course => course.EnrolledStudents)
                .FirstOrDefault(student => student.Username.Equals(loggedInUser.Username, StringComparison.OrdinalIgnoreCase));

            if (selectedStudent == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            bool foundGrade = false;

            // Gå gjennom alle kurs og sjekk om det finnes en karakter for den innloggede studenten.
            foreach (Course course in allCourses)
            {
                if (course.StudentGrades.TryGetValue(selectedStudent.StudentID, out string? grade))
                {
                    Console.WriteLine($"- {course.CourseName} ({course.CourseCode}): {grade}");
                    foundGrade = true;
                }
            }

            // Hvis ingen karakterer er funnet, informer studenten om at ingen karakterer er registrert ennå.
            if (!foundGrade)
            {
                Console.WriteLine("No grades have been registered yet.");
            }
        }
    }
}
