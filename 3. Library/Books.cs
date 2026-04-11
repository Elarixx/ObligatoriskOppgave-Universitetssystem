using Obligatorisk_Oppgave_1_Universitetssystem._1._User;
using System;

namespace Obligatorisk_Oppgave_1_Universitetssystem._3._Library
{
    // ================================================
    //    ----------- LOAN CLASS -----------
    // ================================================
        // Denne klassen lagrer informasjon om et boklån.
    internal class Loan
    {
        // ================================================
        //     ----------- LOAN PROPERTIES -----------
        // ================================================
            public Books Book { get; set; } = null!; // Boken som er lånt.
            public Users Borrower { get; set; } = null!; // Brukeren som låner boken.
            public DateTime BorrowDate { get; set; } // Dato for utlån av boken.
            public DateTime? ReturnDate { get; set; } = null; // Null betyr at boken ikke er levert enda.
            public bool IsReturned => ReturnDate != null; // True hvis boken er levert tilbake.
    }

    // ================================================
    //    ----------- BOOKS CLASS -----------
    // ================================================
        // Denne klassen representerer en bok i biblioteket.
    internal class Books
    {
        // ================================================
        //     ----------- BOOK PROPERTIES -----------
        // ================================================
            public uint BookID { get; init; } = (uint)Random.Shared.Next(1, 9999); // Random ID mellom 1 og 9999.
            public string Title { get; set; } = string.Empty; // Tittel på boken.
            public string Author { get; set; } = string.Empty; // Forfatter til boken.
            public int PublishedYear { get; set; } // Året boken ble utgitt.

            public int TotalCopies { get; set; } = 1; // Totalt antall eksemplarer.
            public int AvailableCopies { get; set; } = 1; // Antall ledige eksemplarer.

            public bool IsAvailable => AvailableCopies > 0; // True hvis minst ett eksemplar er ledig.

    // ================================================
    //    ----------- DEFAULT BOOKS -----------
    // ================================================
        // Denne metoden lager noen standardbøker som finnes når programmet starter.
        public static List<Books> DefaultBooks()
        {
            return new List<Books>
            {
                new Books { Title = "To Kill a Mockingbird", Author = "Harper Lee", PublishedYear = 1960, TotalCopies = 4, AvailableCopies = 4 },
                new Books { Title = "Narnia: The Lion, the Witch and the Wardrobe", Author = "C.S. Lewis", PublishedYear = 1950, TotalCopies = 5, AvailableCopies = 5 },
                new Books { Title = "Fourth Wing", Author = "Rebecca Yarros", PublishedYear = 2023, TotalCopies = 2, AvailableCopies = 2 }
            };
        }

// ================================================================================================
//          ------------------------ SEARCH BOOKS (CASE 5) ------------------------
// ================================================================================================
    // Denne metoden søker etter bøker med tittel eller forfatter.
        public static void SearchBooks(List<Books> allBooks)
        {
            Console.Clear();
            Styles.WriteTitle("--- Search Books ---");

            if (allBooks.Count == 0)
            {
                Console.WriteLine("\nNo books found.");
                return;
            }

            Console.WriteLine("\nSearch by...");
            Console.WriteLine("[1] Title");
            Console.WriteLine("[2] Author");
            Console.Write("\nI choose: ");
            string? choice = Console.ReadLine();

            List<Books> results = new List<Books>(); // Her lagres treffene fra søket.

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the book title: ");
                    string? titleInput = Console.ReadLine();

                    // Søker i tittel uten forskjell på store/små bokstaver.
                    results = allBooks.FindAll(b =>
                        b.Title.Contains(titleInput ?? string.Empty, StringComparison.OrdinalIgnoreCase));
                    break;

                case "2":
                    Console.Write("Enter the book's author: ");
                    string? authorInput = Console.ReadLine();

                    // Søker i forfatter uten forskjell på store/små bokstaver.
                    results = allBooks.FindAll(b =>
                        b.Author.Contains(authorInput ?? string.Empty, StringComparison.OrdinalIgnoreCase));
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    return;
            }

            if (results.Count == 0) // Sjekker om søket ga noen treff.
            {
                Console.WriteLine("No matching books found.");
                return;
            }

            Console.WriteLine("\nMatching Books:"); // Skriver ut alle bøkene som passet søket.
            foreach (Books book in results)
            {
                string availability = book.IsAvailable ? "Available" : "Unavailable"; // Viser om boken er ledig.
                Console.WriteLine($"- {book.Title} by {book.Author} ({book.PublishedYear}), ID: {book.BookID}, Status: {availability}, Copies: {book.AvailableCopies}/{book.TotalCopies}");
            }
        }

// ================================================================================================
//          ------------------------ BORROW BOOKS (CASE 6) ------------------------
// ================================================================================================
    // Denne metoden lar brukeren velge en bok og hvem som skal låne den.
        public static void BorrowBooks(List<Books> allBooks, List<Student> allStudents, List<Employee> allEmployees, List<Loan> allLoans) // Må ha alle listene for å kunne vise bøker, låntakere og lagre lån.
        {
            Console.Clear();
            Styles.WriteTitle("--- Borrow Books ---");

            // Lager en liste med bare bøker som er ledige.
            List<Books> availableBooks = allBooks.FindAll(b => b.IsAvailable);

            if (availableBooks.Count == 0) // Sjekker om det finnes noen ledige bøker før vi prøver å vise dem.
            {
                Console.WriteLine("\nNo available books to borrow.");
                return;
            }

            Console.WriteLine("\nAvailable Books:"); // Skriver ut alle ledige bøker med nummerering.
            for (int i = 0; i < availableBooks.Count; i++) // Går gjennom alle ledige bøker.
            {
                Books book = availableBooks[i]; // Henter bok på plass i listen.
                Console.WriteLine($"[{i + 1}] {book.Title} by {book.Author} ({book.PublishedYear}), ID: {book.BookID}, Copies: {book.AvailableCopies}/{book.TotalCopies}");
            }

            Console.Write("\nEnter the number of the book you want to borrow: ");
            string? numberBorrowInput = Console.ReadLine();

            // Sjekker at brukeren skriver et gyldig tall.
            if (!int.TryParse(numberBorrowInput, out int bookNumberChoice) || bookNumberChoice < 1 || bookNumberChoice > availableBooks.Count)
            {
                Console.WriteLine("Invalid input!");
                return;
            }

            Books selectedBook = availableBooks[bookNumberChoice - 1]; // -1 fordi listen starter på 0.

            // Lager en felles liste med alle brukere som kan låne.
            List<Users> allUsers = new List<Users>();
            allUsers.AddRange(allStudents);
            allUsers.AddRange(allEmployees);

            Console.WriteLine("\nSelect borrower:");
            for (int i = 0; i < allUsers.Count; i++) // Skriver ut alle brukere med nummerering.
            {
                Users u = allUsers[i]; // Henter bruker på plass i listen.
                string type = u is Student ? "Student" : "Employee"; // Viser om brukeren er student eller ansatt.
                Console.WriteLine($"[{i + 1}] {u.Name} ({type})");
            }

            Console.Write("\nEnter borrower number: ");
            string? borrowerInput = Console.ReadLine();

            // Sjekker at brukeren velger en gyldig låner.
            if (!int.TryParse(borrowerInput, out int borrowerChoice) || borrowerChoice < 1 || borrowerChoice > allUsers.Count)
            {
                Console.WriteLine("Invalid input!");
                return;
            }

            Users selectedBorrower = allUsers[borrowerChoice - 1]; // -1 fordi listen starter på 0.

            // Lager et nytt lån og lagrer det i lånelisten.
            allLoans.Add(new Loan
            {
                Book = selectedBook,
                Borrower = selectedBorrower,
                BorrowDate = DateTime.Now
            });

            selectedBook.AvailableCopies--; // Trekker fra én kopi ved utlån.

            Console.WriteLine($"\nYou have borrowed \"{selectedBook.Title}\" (ID: {selectedBook.BookID}) as {selectedBorrower.Name}.");
        }

    // =========================
    //    -- ACTIVE LOANS --
    // =========================
        // Denne metoden viser bare lån som ikke er levert enda.
        public static void ActiveLoans(List<Loan> allLoans)
        {
            Console.Clear();
            Styles.WriteTitle("--- Active Loans ---");

            List<Loan> activeLoans = allLoans.FindAll(l => !l.IsReturned); // Henter kun aktive lån.

            if (activeLoans.Count == 0) // Sjekker om det finnes aktive lån.
            {
                Console.WriteLine("No active loans found.");
                return;
            }

            foreach (Loan loan in activeLoans) // Skriver ut informasjon om hvert aktivt lån.
            {
                Console.WriteLine($"- ID: {loan.Book.BookID} | \"{loan.Book.Title}\" borrowed by {loan.Borrower.Name} on {loan.BorrowDate:dd.MM.yyyy}");
            }
        }

    // -------------------------
    //    -- LOANS HISTORY --
    // -------------------------
        // Denne metoden viser hele lånehistorikken, både aktive og leverte lån.
        public static void LoansHistory(List<Loan> allLoans)
        {
            Console.Clear();
            Styles.WriteTitle("--- Loans History ---");

            if (allLoans.Count == 0) // Sjekker om det finnes noen lån i det hele tatt.
            {
                Console.WriteLine("No loans found.");
                return;
            }

            foreach (Loan loan in allLoans) // Skriver ut informasjon om hvert lån.
            {
                string status = loan.IsReturned
                    ? $"Returned on {loan.ReturnDate:dd.MM.yyyy}"
                    : "Active";

                Console.WriteLine($"- ID: {loan.Book.BookID} | \"{loan.Book.Title}\" borrowed by {loan.Borrower.Name} on {loan.BorrowDate:dd.MM.yyyy} [{status}]");
            }
        }

// ================================================================================================
//          ------------------------ RETURN BOOKS (CASE 7) ------------------------
// ================================================================================================
    // Denne metoden lar brukeren returnere bok eller se lån og historikk.
        public static void ReturnBooks(List<Loan> allLoans)
        {
            Console.Clear();
            Styles.WriteTitle("--- Return Books ---");

            Console.WriteLine("\n[1] Return a book");
            Console.WriteLine("[2] Show active loans");
            Console.WriteLine("[3] Show loan history");
            Console.Write("\nI choose: ");
            string? choiceMenu = Console.ReadLine();

            switch (choiceMenu)
            {
                case "1":
                    // Brukeren valgte å returnere bok, så vi fortsetter under switch.
                    break;

                case "2":
                    ActiveLoans(allLoans);
                    return;

                case "3":
                    LoansHistory(allLoans);
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    return;
            }

            // Lager en liste med bare aktive lån som kan returneres.
            List<Loan> activeLoans = allLoans.FindAll(l => !l.IsReturned);

            if (activeLoans.Count == 0)
            {
                Console.WriteLine("\nNo active loans to return.");
                return;
            }

            Console.WriteLine("\nActive Loans:");
            for (int i = 0; i < activeLoans.Count; i++)
            {
                Loan loan = activeLoans[i]; // Henter lån på plass i listen.
                Console.WriteLine($"[{i + 1}] ID: {loan.Book.BookID} | \"{loan.Book.Title}\" borrowed by {loan.Borrower.Name} ({loan.BorrowDate:dd.MM.yyyy})");
            }

            Console.Write("\nEnter the number of the loan to return: ");
            string? numberReturnInput = Console.ReadLine();

            // Sjekker at brukeren velger et gyldig lån.
            if (!int.TryParse(numberReturnInput, out int choice) || choice < 1 || choice > activeLoans.Count)
            {
                Console.WriteLine("Invalid input!");
                return;
            }

            Loan selectedLoan = activeLoans[choice - 1]; // -1 fordi listen starter på 0.
            selectedLoan.ReturnDate = DateTime.Now; // Setter retur-dato til nå.
            selectedLoan.Book.AvailableCopies++; // Legger tilbake én kopi ved innlevering.

            Console.WriteLine($"\nYou have returned \"{selectedLoan.Book.Title}\" (ID: {selectedLoan.Book.BookID}).");
        }

// ================================================================================================
//          ------------------------ REGISTER BOOKS (CASE 8) ------------------------
// ================================================================================================
    // Denne metoden registrerer en ny bok i biblioteket.
        public static void CreateBook(List<Books> allBooks)
        {
            Console.Clear();
            Styles.WriteTitle("--- Register Books ---");

            Books newBook = new Books(); // Lager en ny bok som fylles ut med input fra brukeren.

            Console.Write("\nEnter the book title: ");
            newBook.Title = Console.ReadLine() ?? string.Empty; // Bruker tom streng hvis input er null.

            Console.Write("Enter the author of the book: ");
            newBook.Author = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the published year: ");
            if (!int.TryParse(Console.ReadLine(), out int publishedYear)) // Sjekker at årstallet er gyldig.
            {
                Console.WriteLine("Invalid year input!");
                return;
            }
            newBook.PublishedYear = publishedYear;

            Console.Write("Enter the total number of copies: ");
            if (!int.TryParse(Console.ReadLine(), out int totalCopies) || totalCopies < 1) // Må være et gyldig tall og minst 1.
            {
                Console.WriteLine("Invalid number of copies!");
                return;
            }

            newBook.TotalCopies = totalCopies;
            newBook.AvailableCopies = totalCopies; // Alle kopiene er ledige ved opprettelse.

            allBooks.Add(newBook); // Legger boken til i listen.

            Console.WriteLine($"\nYou have successfully registered a new book: {newBook.Title} by {newBook.Author}. Book ID: {newBook.BookID}.");
        }
    }
}
