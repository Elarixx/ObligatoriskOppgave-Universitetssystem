using System;
using Oppgave_2.Codes._1._Users;
using Oppgave_2.Codes;

namespace Oppgave_2
{
    internal class Program
    {
    // ================================================
    //      ----------- PROGRAM DATA -----------
    // ================================================
        private static readonly UserManager userManager = new();

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Oppgave 2 - Login/Register System ===");
                Console.WriteLine("[1] Log in");
                Console.WriteLine("[2] Register new user");
                Console.WriteLine("[0] Exit");
                Console.Write("\nI choose: ");
                string userChoice = Console.ReadLine() ?? string.Empty;

                bool retryCurrentAction;
                do
                {
                    retryCurrentAction = false;
                    bool canRetryCurrentAction = false;

                    switch (userChoice)
                    {
                        case "1":
                            Login();
                            canRetryCurrentAction = true;
                            break;

                        case "2":
                            Registry();
                            canRetryCurrentAction = true;
                            break;
                        case "0":
                            running = false; // Hva gjør denne?
                            break; // hva menes med 'break'?

                        default:
                            Console.WriteLine("Invalid Choice.");
                            break;
                    }

                    // Hvis bruker kan prøve igjen, spør om det
                    if (running && canRetryCurrentAction)
                    {
                        retryCurrentAction = STYLE.AskToRetryCurrentAction();
                        // Tøm skjermen hvis brukeren vil prøve igjen
                        if (retryCurrentAction) Console.Clear();
                    }

                } while (running && retryCurrentAction); // Gjentar hvis bruker vil prøve igjen

                // Pause før menyen vises igjen
                STYLE.PauseBeforeMenu(running);
            }
        }


    // ================================================
    //     ------- LOGIN USER (CASE 1) -------
    // ================================================
        // Logger inn eksisterende bruker og sender videre til riktig meny basert på brukerens rolle.
        private static void Login()
        {
            Console.Clear();
            Console.WriteLine("=== Login ===");

            Console.Write("Enter username: ");
            string userName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? string.Empty;

            // Prøver å logge inn brukeren.
            User? loggedInUser = userManager.Login(userName, password);

            if (loggedInUser == null)
            {
                Console.WriteLine("Login failed.");
                return;
            }

            Console.WriteLine($"Login successfull!\nWelcome, {loggedInUser.Type}: {loggedInUser.Name}");
            Console.ReadKey(); // Hvor leser denne koden inn fra?

            switch (loggedInUser.Type)
            {
                case UserType.Student:
                    StudentMenu();
                    break;
                case UserType.Teacher:
                    TeacherMenu();
                    break;
                case UserType.Librarian:
                    LibrarianMenu();
                    break;
                default:
                    Console.WriteLine("Unknown user type."); // Annen melding?
                    break;
            }
        }


    // ================================================
    //     ------- REGISTER USER (CASE 2) -------
    // ================================================
        // Registrerer ny bruker med valgt rolle.
        private static void Registry()
        {
            Console.Clear();
            Console.WriteLine("=== Register New User ===");

            Console.WriteLine("Select user type:");
            Console.WriteLine("[1] Student");
            Console.WriteLine("[2] Teacher");
            Console.WriteLine("[3] Librarian");
            Console.Write("\nI choose: ");
            string userTypeChoice = Console.ReadLine() ?? string.Empty;

            // Hva gjør dette?
            User? newUser = userTypeChoice switch
            {
                "1" => new Student(),
                "2" => new Teacher(),
                "3" => new Librarian(),
                _ => null // Hva er dette til? Hvorfor '_'???
            };

            if (newUser == null)
            {
                Console.WriteLine("Invalid user type.");
                return;
            }

            Console.Write("Enter username: ");
            newUser.UserName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter password: ");
            newUser.Password = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter name: ");
            newUser.Name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter email: ");
            newUser.Email = Console.ReadLine() ?? string.Empty;

            bool success = userManager.RegisterUser(newUser, out string message);
            Console.WriteLine(message);
        }

    // ================================================
    //     ------- MENUS FOR EACH USER ROLE -------
    // ================================================

      /* ------ STUDENT MENU ------ */
        private static void StudentMenu()
        {
            bool isLoggedIn = true;

            while (isLoggedIn)
            {
                Console.Clear();
                Console.WriteLine("=== Student Menu ===");
                Console.WriteLine("[1] View my Courses\n" +
                                  "[2] Enroll in Course\n" +
                                  "[3] Withdraw from Course\n" +
                                  "[4] View my Grades\n" +
                                  "[5] Search Books\n" +
                                  "[6] Borrow Books\n" +
                                  "[7] Return Books\n" +
                                  "[0] Logout");
                Console.Write("\nI choose: ");
                string studentUserChoice = Console.ReadLine() ?? string.Empty;

                switch (studentUserChoice)
                {
                    case "1":
                        Console.WriteLine("");
                        break;
                    case "2":
                        Console.WriteLine("");
                        break;
                    case "3":
                        Console.WriteLine("");
                        break;
                    case "4":
                        Console.WriteLine("");
                        break;
                    case "5":
                        Console.WriteLine("");
                        break;
                    case "6":
                        Console.WriteLine("");
                        break;
                    case "7":
                        Console.WriteLine("");
                        break;
                    case "0":
                        isLoggedIn = false; 
                        break;
                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }

      /* ------ TEACHER MENU ------ */
        private static void TeacherMenu()
        {
            bool isLoggedIn = true;

            while (isLoggedIn)
            {
                Console.Clear();
                Console.WriteLine("=== Teacher Menu ===");
                Console.WriteLine("[1] View Courses\n" +
                                  "[2] Create Course\n" +
                                  "[3] Search Course\n" +
                                  "[4] Set Student Grade\n" +
                                  "[5] Search Books\n" +
                                  "[6] Borrow Books\n" +
                                  "[7] Return Books\n" +
                                  "[8] Register Curriculum\n" +
                                  "[0] Logout");
                Console.Write("\nI choose: ");
                string teacherUserChoice = Console.ReadLine() ?? string.Empty;

                switch (teacherUserChoice)
                {
                    case "1":
                        Console.WriteLine("");
                        break;
                    case "2":
                        Console.WriteLine("");
                        break;
                    case "3":
                        Console.WriteLine("");
                        break;
                    case "4":
                        Console.WriteLine("");
                        break;
                    case "5":
                        Console.WriteLine("");
                        break;
                    case "6":
                        Console.WriteLine("");
                        break;
                    case "7":
                        Console.WriteLine("");
                        break;
                    case "8":
                        Console.WriteLine("");
                        break;
                    case "0":
                        isLoggedIn = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }

      /* ------ LIBRARIAN MENU ------ */
        private static void LibrarianMenu()
        {
            bool isLoggedIn = true;

            while (isLoggedIn)
            {
                Console.Clear();
                Console.WriteLine("=== Librarian Menu ===");
                Console.WriteLine("[1] Register Book\n" +
                                  "[2] Search Books\n" +
                                  "[3] View Active Loans\n" +
                                  "[4] View Loan Loans\n" +
                                  "[0] Logout");
                Console.Write("\nI choose: ");
                string librarianUserChoice = Console.ReadLine() ?? string.Empty;

                switch (librarianUserChoice)
                {
                    case "1":
                        Console.WriteLine("");
                        break;
                    case "2":
                        Console.WriteLine("");
                        break;
                    case "3":
                        Console.WriteLine("");
                        break;
                    case "4":
                        Console.WriteLine("");
                        break;
                    case "0":
                        isLoggedIn = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }
    }
}