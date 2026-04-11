using System;
using System.Linq;
using Obligatorisk_Oppgave_1_Universitetssystem._1._User;
using Obligatorisk_Oppgave_1_Universitetssystem._2._Course;
using Obligatorisk_Oppgave_1_Universitetssystem._3._Library;
//==============================================================


namespace Obligatorisk_Oppgave_1_Universitetssystem
{
    internal class Program
    {

// ================================================================
//     ------------------- PROGRAM LISTS ----------------------
// ================================================================
    // Felles lister som brukes i hele programmet for å lagre data i minnet.
        static List<Course> allCourses = new List<Course>();
        static List<Student> allStudents = new List<Student>();
        static List<Exchange_Student> allExchangeStudents = new List<Exchange_Student>();
        static List<Employee> allEmployees = new List<Employee>();
        static List<Books> allBooks = new List<Books>();
        static List<Loan> allLoans = new List<Loan>();
// |═════════════════<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>═════════════════|


        static void Main(string[] args) // Hovedmetoden der programmet starter. "string[] args" er en parameter som kan ta imot argumenter fra kommandolinjen når program kjøres.
        {
            AddDefaultUsers(); // Laster inn standarddata ved oppstart.

// ================================================================================================
//          ------------------------ INTRODUCTION ------------------------
// ================================================================================================

            Console.Clear();
            Styles.WriteTitle("Welcome to the University System");
            Console.WriteLine("Manage courses, users, and library loans.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(intercept: true);

// ================================================================================================
//          ------------------------ APPLICATION MENU ------------------------
// ================================================================================================

            Console.Clear();
            bool running = true; // Styrer hovedløkken for menyen. Så lenge "running" er true, vil programmet kjøre.
            while (running) 
            {
                Styles.WriteTitle("Choose an option:");
                    Console.WriteLine("\n[1] Create a Course");
                    Console.WriteLine("[2] Sign student up for and off a Course");
                    Console.WriteLine("[3] Write out the course and the student");
                    Console.WriteLine("[4] Search Course");
                    Console.WriteLine("[5] Search Book");
                    Console.WriteLine("[6] Borrow a Book");
                    Console.WriteLine("[7] Return a Book / Show Loans");
                    Console.WriteLine("[8] Register a Book");
                    Console.WriteLine("[9] Register a User");
                    Console.WriteLine("[10] Show data overview");
                    Console.WriteLine("[0] Cancel/End\n");

                Console.Write("What would you like to do?: ");
                string userChoice = Console.ReadLine() ?? string.Empty;

                bool retryCurrentAction; // Styrer om samme menyvalg skal kjøres på nytt eller om brukeren skal tilbake til menyen.
                do // Lar brukeren velge å utføre samme handling på nytt eller gå tilbake til menyen.
                {
                    retryCurrentAction = false;
                    bool canRetryCurrentAction = false;

                // Brukerens valg bestemmer hvilken metode som kalles.
                    switch (userChoice)
                    {
                        case "1 ":
                            Course.CreateCourse(allCourses);
                            canRetryCurrentAction = true;
                            break;
                        case "2":
                            Course.SignStudentUpOrOffCourse(allCourses, allStudents);
                            canRetryCurrentAction = true;
                            break;
                        case "3":
                            Course.PrintCourseAndStudent(allCourses);
                            canRetryCurrentAction = true;
                            break;
                        case "4":
                            Course.SearchCourse(allCourses);
                            canRetryCurrentAction = true;
                            break;
                        case "5":
                            Books.SearchBooks(allBooks);
                            canRetryCurrentAction = true;
                            break;
                        case "6":
                            Books.BorrowBooks(allBooks, allStudents, allEmployees, allLoans);
                            canRetryCurrentAction = true;
                            break;
                        case "7":
                            Books.ReturnBooks(allLoans);
                            canRetryCurrentAction = true;
                            break;
                        case "8":
                            Books.CreateBook(allBooks);
                            canRetryCurrentAction = true;
                            break;
                        case "9":
                            UserActions.CreateUser(allStudents, allExchangeStudents, allEmployees);
                            canRetryCurrentAction = true;
                            break;
                        case "10":
                            ShowDataOverview();
                            break;
                        case "0":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Error");
                            break;
                    }

                    if (running && canRetryCurrentAction)
                    {
                        retryCurrentAction = AskToRetryCurrentAction();

                        if (retryCurrentAction)
                        {
                            Console.Clear();
                        }
                    }
                }
                while (running && retryCurrentAction);

                // Legger til en separator for å skille mellom hver handling
                if (running)
                {
                    Styles.WriteSeparator();
                }

                Styles.PauseBeforeMenu(userChoice, running);
            }
        }

// ================================================================
//    ------------------- DEFAULT DATA ----------------------
// ================================================================
    // Laster inn forhåndsdefinerte kurs, studenter, ansatte og bøker.
        private static void AddDefaultUsers() 
        {
            allCourses.AddRange(Course.DefaultCourses());

            allStudents.AddRange(Student.DefaultStudents());

            List<Exchange_Student> defaultExchangeStudents = Exchange_Student.DefaultExchangeStudents();
            allExchangeStudents.AddRange(defaultExchangeStudents);
            allStudents.AddRange(defaultExchangeStudents);

            allEmployees.AddRange(Employee.DefaultEmployees());

            allBooks.AddRange(Books.DefaultBooks());
        }

// ================================================================================================
//          ------------------------ SHOW DATA OVERVIEW (CASE 10) ------------------------
// ================================================================================================
    // Viser en oversikt over alle studenter, utvekslingsstudenter, ansatte, kurs og bøker i systemet.
        private static void ShowDataOverview()
        {
            Console.Clear();
            Styles.WriteTitle("--- Show Data Overview ---");

            Console.WriteLine("\nStudents:");
            foreach (Student student in allStudents) // Viser vanlige studenter. 
            {
                if (student is Exchange_Student) continue; // Hopper over utvekslingsstudenter (vises i egen seksjon).

                // Lager tekst for studentens kurs (eller "none" hvis ingen kurs).
                string enrolledCoursesText = student.EnrolledCourses.Count == 0
                    ? "none"
                    : string.Join(", ", student.EnrolledCourses.Select(course => $"{course.CourseName} ({course.CourseCode})"));

                Console.WriteLine($"- {student.Name} (ID: {student.StudentID}) | Email: {student.Email} | Enrolled Courses: {enrolledCoursesText}");
            }

            Console.WriteLine("\nExchange Students:");
            foreach (Exchange_Student exchangeStudent in allExchangeStudents) // Viser utvekslingsstudenter.
            {
                // Lager tekst for kurs utvekslingsstudenten er påmeldt.
                string enrolledCoursesText = exchangeStudent.EnrolledCourses.Count == 0
                    ? "none"
                    : string.Join(", ", exchangeStudent.EnrolledCourses.Select(course => $"{course.CourseName} ({course.CourseCode})"));

                Console.WriteLine($"- {exchangeStudent.Name} (ID: {exchangeStudent.StudentID}) | Email: {exchangeStudent.Email}");
                Console.WriteLine($"  Home University: {exchangeStudent.homeUniverity} | Country: {exchangeStudent.Country}");
                Console.WriteLine($"  Period: {exchangeStudent.PeriodFrom:dd.MM.yyyy} - {exchangeStudent.PeriodTo:dd.MM.yyyy} | Enrolled Courses: {enrolledCoursesText}");
            }

            Console.WriteLine("\nEmployees:");
            foreach (Employee employee in allEmployees) // Viser ansatte.
            {
                Console.WriteLine($"- {employee.Name} (ID: {employee.EmployeeID}) | Email: {employee.Email} | Position: {employee.Position} | Department: {employee.Department}");
            }

            Console.WriteLine("\nCourses:");
            foreach (Course course in allCourses) // Viser kurs.
            {
                Console.WriteLine($"- {course.CourseName} ({course.CourseCode}) | Credits: {course.CourseCredits} | Availability: {course.EnrolledStudents.Count}/{course.MaxSeats}");
            }

            Console.WriteLine("\nBooks:");
            foreach (Books book in allBooks) // Viser bøker.
            {
                Console.WriteLine($"- ID: {book.BookID} | {book.Title}");
                Console.WriteLine($"  Author: {book.Author} ({book.PublishedYear}) | Copies: {book.AvailableCopies}/{book.TotalCopies}");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey(intercept: true); // Venter på tastetrykk uten å vise tasten i konsollen.
        }

// ================================================================================================
//          ------------------------ RETRY CURRENT ACTION ------------------------
// ================================================================================================
    // Lar bruker velge om samme menyvalg skal kjøres på nytt eller gå tilbake til meny.
        private static bool AskToRetryCurrentAction()
        {
            Console.WriteLine(); // Litt pusterom før retry-valget.
            Console.Write("Try again? [R]etry / [M]enu: ");

            while (true)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;

                if (key == ConsoleKey.R)
                {
                    Console.WriteLine("R");
                    return true;
                }

                if (key == ConsoleKey.M)
                {
                    Console.WriteLine("M");
                    return false;
                }
            }
        }
    }
}
