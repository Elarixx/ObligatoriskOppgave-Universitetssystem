using System;
using System.Collections.Generic;
using System.Text;

namespace Oppgave_2.Codes._1._Users
{
    internal class UserManager
    {
    // ================================================
    //      ----------- USER STORAGE -----------
    // ================================================
        // Lagrer alle brukere i minnet mens programmet kjører.
        private readonly List<User> users = new(); // listen kan kun endres innenfor UserManager.

        // Lesetilgang uten at andre klasser kan endre listen direkte.
        public IReadOnlyList<Users> Users => users;


    // ================================================
    //      ----------- REGISTER USER -----------
    // ================================================
        // Registrerer ny bruker hvis brukernavn ikke finnes fra før.
        public bool RegisterUser(User newUser, out string message)
        {
            if (newUser == null)
            {
                message = "User cannot be empty.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(newUser.UserName) ||
                string.IsNullOrWhiteSpace(newUser.Password))
            {
                message = "Username and password is required.";
                return false;
            }

            bool userNameExists = users.Exists(u =>
                u.UserName.Equals(newUser.UserName, StringComparison.OrdinalIgnoreCase));

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
        // Logger inn bruker hvis brukernavn og passord matcher en eksisterende bruker.
        public User? Login(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return null; // Returnerer null hvis brukernavn eller passord er tomt.
            }

            User? user = users.Find(u =>
                u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            return user; // Returnerer brukeren hvis funnet, ellers null.
        }
    }
}
