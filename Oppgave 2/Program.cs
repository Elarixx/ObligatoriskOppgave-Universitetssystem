using System;
using System.Linq;

using Obligatorisk_Oppgave_1_Universitetssystem._1._User;
using Obligatorisk_Oppgave_1_Universitetssystem._2._Course;
using Obligatorisk_Oppgave_1_Universitetssystem.Codes;
//==============================================================


namespace Obligatorisk_Oppgave_1_Universitetssystem._3._Library
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

        // <>-<>-<>-- Oppgave 2 --<>-<>-<>
            static UserManager userManager = null!; // Hvorfor '!' ?
            static Users? loggedInUser = null; // Holder styr på hvem som er logget inn.
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

            // <>-<>-<>-- Oppgave 2 --<>-<>-<>
                // Oppretter UserManager med alle eksisterende brukere.
                userManager = new UserManager(allStudents, allEmployees);

                while (loggedInUser == null)
                {
                    Console.Clear();
                    Styles.WriteTitle("Login or Register?");
                    Console.WriteLine("\n[1] Login");
                    Console.WriteLine("[2] Register");
                    
                    Console.Write("\nChoose an option: ");
                    string? loginChoice = Console.ReadLine();

                    switch (loginChoice)
                    {
                        case "1":
                            loggedInUser = userManager.StartLoginFlow();
                            break;
                        case "2":
                            Users? newUser = UserActions.CreateUser();

    if (newUser == null)
    {
        Styles.WaitForNext("register");
        break;
    }

    if (userManager.RegisterUser(newUser, out string message))
    {
        AddUserToCorrectList(newUser);
        Console.WriteLine(message);
    }
    else
    {
        Console.WriteLine(message);
    }

    Styles.WaitForNext("register");
    break;
                        default:
                            Console.WriteLine("Invalid choice. Please choose 1 or 2.");
                            Styles.WaitForNext("login/register option"); // Venter på at brukeren skal lese feilmeldingen før den fortsetter løkken for login/register.
                        break;
                    }
                }
            // <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>

// ================================================================================================
//          ------------------------ APPLICATION MENU ------------------------
// ================================================================================================

            Console.Clear();
            bool running = true; // Styrer hovedløken for menyen. Så lenge "running" er true, vil programmet kjøre.
            while (running) 
            {
            // <>-<>-<>-- Oppgave 2 --<>-<>-<>
                ShowRoleMenu(loggedInUser!); // Viser riktig meny basert på roll til innlogget bruker.

                Console.Write("What would you like to do?: ");
                string userChoice = Console.ReadLine() ?? string.Empty;

                bool retryCurrentAction; // Styrer om samme menyvalg skal kjøres på nytt eller om brukeren skal tilbake til menyen.
                do
                {
                    retryCurrentAction = false;
                    bool canRetryCurrentAction = false;

                    // Sender menyvalg til riktig metode basert på brukerens rolle.
                    switch (loggedInUser!.Role)
                    {
                        case UserRole.Student:
                        case UserRole.ExchangeStudent:
                            canRetryCurrentAction = Student.HandleMenuChoice(
                                loggedInUser, userChoice, allCourses, allStudents, allBooks, allEmployees, allLoans, ref running);
                            break;

                        case UserRole.Teacher:
                        case UserRole.Librarian:
                            canRetryCurrentAction = Employee.HandleMenuChoice(
                                loggedInUser, userChoice, allCourses, allStudents, allEmployees, allBooks, allLoans, ref running);
                            break;

                        default:
                            Console.WriteLine("Invalid role.");
                            break;
                    }
                
                    if (running && canRetryCurrentAction) // Hvis handlingen kan kjøres på nytt (f.eks. ved feil input), spør brukeren om de vil prøve igjen eller gå tilbake til menyen.
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
        allCourses.AddRange(Course.DefaultCourses()); // Legger til alle kurs fra Course.DefaultCourses() inn i allCourses-listen.

            allStudents.AddRange(Student.DefaultStudents()); // Legger til alle vanlige studenter fra Student.DefaultStudents() inn i allStudents-listen.

            List<Exchange_Student> defaultExchangeStudents = Exchange_Student.DefaultExchangeStudents(); // Henter utvekslingsstudenter fra Exchange_Student.DefaultExchangeStudents() og lagrer i en midlertidig liste for å kunne legge dem til både i allExchangeStudents og allStudents (siden Exchange_Student arver fra Student).
            allExchangeStudents.AddRange(defaultExchangeStudents); // Legger til alle utvekslingsstudenter i allExchangeStudents-listen.
            allStudents.AddRange(defaultExchangeStudents); // Legger til alle utvekslingsstudenter i allStudents-listen også, siden de også skal regnes som studenter i systemet.

            allEmployees.AddRange(Employee.DefaultEmployees()); // Legger til alle ansatte fra Employee.DefaultEmployees() inn i allEmployees-listen.

            allBooks.AddRange(Books.DefaultBooks()); // Legger til alle bøker fra Books.DefaultBooks() inn i allBooks-listen.

            // ================================================================================================
            //          ------------------------ DEFAULT ENROLLMENTS AND LOANS ------------------------
            // ================================================================================================
            // Legger inn litt standarddata slik at systemet ser mer ferdig ut ved oppstart.
            if (allStudents.Count >= 2 && allCourses.Count >= 2)
        {
            Student firstStudent = allStudents[0];
            Student secondStudent = allStudents[1];

            Course firstCourse = allCourses[0];
            Course secondCourse = allCourses[1];

            // Legger første student til første kurs.
            if (!firstStudent.EnrolledCourses.Contains(firstCourse))
            {
                firstStudent.EnrolledCourses.Add(firstCourse);
                firstCourse.EnrolledStudents.Add(firstStudent);
            }

            // Legger andre student til andre kurs.
            if (!secondStudent.EnrolledCourses.Contains(secondCourse))
            {
                secondStudent.EnrolledCourses.Add(secondCourse);
                secondCourse.EnrolledStudents.Add(secondStudent);
            }

            // Legger inn eksempelkarakter på første student.
            firstCourse.StudentGrades[firstStudent.StudentID] = "B";
        }

        // Lager et aktivt lån og et tidligere lån for å vise i låneoversikten.
        if (allBooks.Count >= 2 && allStudents.Count >= 1 && allEmployees.Count >= 1)
        {
            Books firstBook = allBooks[0];
            Books secondBook = allBooks[1];

            Student firstStudent = allStudents[0];
            Employee firstEmployee = allEmployees[0];

            // Lager ett aktivt lån på student.
            if (firstBook.AvailableCopies > 0)
            {
                allLoans.Add(new Loan
                {
                    Book = firstBook,
                    Borrower = firstStudent,
                    BorrowDate = DateTime.Now.AddDays(-3)
                });

                firstBook.AvailableCopies--;
            }

            // Lager ett tidligere lån på ansatt som allerede er levert tilbake.
            if (secondBook.AvailableCopies > 0)
            {
                allLoans.Add(new Loan
                {
                    Book = secondBook,
                    Borrower = firstEmployee,
                    BorrowDate = DateTime.Now.AddDays(-10),
                    ReturnDate = DateTime.Now.AddDays(-2)
                });
            }
        }
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
    // Lar bruker velge om samme menyvalg skal kjøre på nytt eller gå tilbake til meny.
        private static bool AskToRetryCurrentAction()
        {
            Console.WriteLine(); // Litt pusterom før retry-valget.

            while (true)
            {
                Console.Write("Try again? [R]etry / [M]enu: ");
                string retryChoice = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();

                switch (retryChoice)
                {
                    case "R":
                        return true;

                    case "M":
                        return false;

                    default:
                        Console.WriteLine("Invalid choice. Please choose R or M.");
                        break;
                }
            }
        }

// ================================================================================================
//         ------------------------ SHOW ROLE MENU ------------------------
// ================================================================================================
        private static void ShowRoleMenu(Users user)
        {
            Console.Clear();

            switch (user.Role)
            {
                // Viser samme meny for både vanlige studenter og utvekslingsstudenter, siden de har samme tilgang i systemet.
                case UserRole.Student:
                case UserRole.ExchangeStudent:
                    Styles.WriteTitle($"Student Menu - {user.Name}");
                    Console.WriteLine("\n[1] Sign student up for and off a Course");
                    Console.WriteLine("[2] Show my courses");
                    Console.WriteLine("[3] Show my grades");
                    Console.WriteLine("[4] Search Book");
                    Console.WriteLine("[5] Borrow a Book");
                    Console.WriteLine("[6] Return a Book");
                    Console.WriteLine("[0] Exit\n");
                    break;

                // Viser meny for lærere
                case UserRole.Teacher:
                    Styles.WriteTitle($"Teacher Menu - {user.Name}");
                    Console.WriteLine("\n[1] Create a Course");
                    Console.WriteLine("[2] Search Course");
                    Console.WriteLine("[3] Search Book");
                    Console.WriteLine("[4] Borrow a Book");
                    Console.WriteLine("[5] Return a Book");
                    Console.WriteLine("[6] Register Syllabus");
                    Console.WriteLine("[7] Set Student Grade");
                    Console.WriteLine("[0] Exit\n");
                    break;

                // Viser meny for bibliotekarer
                case UserRole.Librarian:
                    Styles.WriteTitle($"Librarian Menu - {user.Name}");
                    Console.WriteLine("\n[1] Register a Book");
                    Console.WriteLine("[2] Show Active Loans");
                    Console.WriteLine("[3] Show Loan History");
                    Console.WriteLine("[0] Exit\n");
                    break;
            }
        }

// ================================================================
//    ------------------- ADD REGISTERED USER ----------------------
// ================================================================
    // Legger nyregistrert bruker inn i riktig liste etter vellykket registrering.
        private static void AddUserToCorrectList(Users newUser)
        {
            switch (newUser)
            {
                // Siden Exchange_Student arver fra Student, må vi sjekke for Exchange_Student først for å unngå at de bare blir lagt til i allStudents-listen og ikke i allExchangeStudents-listen.
                case Exchange_Student exchangeStudent:
                    allExchangeStudents.Add(exchangeStudent);
                    allStudents.Add(exchangeStudent);
                    break;

                // Vanlige studenter legges bare til i allStudents-listen.
                case Student student:
                    allStudents.Add(student);
                    break;

                // Ansatte legges til i allEmployees-listen.
                case Employee employee:
                    allEmployees.Add(employee);
                    break;
            }
        }
    }
}
