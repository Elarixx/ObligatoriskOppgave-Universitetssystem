using Obligatorisk_Oppgave_1_Universitetssystem._1._User;

namespace Obligatorisk_Oppgave_1_Universitetssystem.Codes
{
    internal class UserManager
    {
    // ================================================
    //      ----------- USER STORAGE -----------
    // ================================================
        // Lagrer alle brukere i minnet mens programmet kjører.
        private readonly List<Users> users = new(); // listen kan kun endres innenfor UserManager.

        // Lesetilgang uten at andre klasser kan endre listen direkte.
        public IReadOnlyList<Users> Users => users;

    // ===================================================================
    //      ----------- LASTER INN EKSISTERENDE BRUKERE -----------    
    // ===================================================================
        // Kjører automatisk når UserManager opprettes i Program.cs.
        // Fyller users-listen med alle eksisterende studenter og ansatte.
        public UserManager(List<Student> students, List<Employee> employees)
        {
            users.AddRange(students); // Legger til alle studenter i user-listen.
            users.AddRange(employees); // Legger til alle ansatte i user-listen.
        }

    // ================================================
    //      ----------- REGISTER USER -----------
    // ================================================
        // Registrerer ny bruker hvis brukernavn ikke finnes fra før.
        public bool RegisterUser(Users newUser, out string message)
        {
            if (newUser == null)
            {
                message = "User cannot be empty.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(newUser.Username) ||
                string.IsNullOrWhiteSpace(newUser.Password))
            {
                message = "Username and password is required.";
                return false;
            }

            bool userNameExists = users.Exists(u =>
                u.Username.Equals(newUser.Username, StringComparison.OrdinalIgnoreCase));

            if (userNameExists)
            {
                message = "Username already exists. Please choose a different username.";
                return false;
            }

            users.Add(newUser);
            message = "User registered successfully.";
            return true;
        }


    // ================================================
    //      ----------- LOGIN USER -----------
    // ================================================
        // Starter innloggingsprosessen ved å be brukeren om å skrive inn brukernavn og passord.
        public Users? StartLoginFlow()
        {
            Console.Clear();
            Console.WriteLine("=== Login ===");

            Console.Write("\nEnter your username: ");
            string username = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter your password: ");
            string password = Console.ReadLine() ?? string.Empty;

            // Sjekker først om feltene er tomme.
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username and password cannot be empty.");
                Console.ReadKey(intercept: true);
                return null;
            }

            // Prøver å finne brukeren i users-listen.
            Users? loggedInUser = Login(username, password);

            // Hvis brukeren ikke finnes, er brukernavn eller passord feil.
            if (loggedInUser == null)
            {
                Console.WriteLine("Invalid username or password.");
                Console.ReadKey(intercept: true);
                return null;
            }

            Console.WriteLine($"\nWelcome, {loggedInUser.Name} ({loggedInUser.Role})!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(intercept: true);

            return loggedInUser;
        }


        // Logger inn bruker hvis brukernavn og passord matcher en eksisterende bruker.
        public Users? Login(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return null; // Returnerer null hvis brukernavn eller passord er tomt.
            }

            Users? user = users.Find(u =>
                u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            return user; // Returnerer brukeren hvis funnet, ellers null.
        }
    }
}
