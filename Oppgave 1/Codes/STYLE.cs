using System;
using System.Collections.Generic;

namespace Obligatorisk_Oppgave_1_Universitetssystem.Codes
{
    internal static class Styles
    {

        // ANSI-escape-koder gir enkel konsoll-formatering (f.eks. fet skrift)


        // ======================================================================
        // Tittel   ↓
        // ======================================================================

        // Skriver tittel i fet skrift og en underline-linje
        public static void WriteTitle(string title)
        {
            const string boldOn = "\u001b[1m";   // ANSI: slå på fet skrift
            const string boldOff = "\u001b[0m";  // ANSI: slå av/reset format
            Console.WriteLine($"{boldOn}{title}{boldOff}");              // Skriv tittel i fet
            Console.WriteLine(new string('=', Math.Max(title.Length, 30))); // Understrek, min 30 tegn
        }

        // ======================================================================
        // Overskrift   ↓
        // ======================================================================

        // Skriver overskrift i fet skrift og legger til en blank linje
        public static void WriteHeading(string title)
        {
            const string boldOn = "\u001b[1m";   // ANSI: slå på fet skrift
            const string boldOff = "\u001b[0m";  // ANSI: slå av/reset format
            Console.WriteLine($"{boldOn}{title}{boldOff}"); // Overskrift i fet
            Console.WriteLine();                            // Blank linje etter
        }

        // ======================================================================
        // Separator / Pusterom      ↓
        // ======================================================================

        // Skriver en separatorlinje og en blank linje
        public static void WriteSeparator()
        {
            Console.WriteLine("-------------------------------------------"); // Separator
            Console.WriteLine();                                                 // Blank linje
        }

        // ======================================================================
        // Vente på tastetrykk || 'WaitForNext'     ↓
        // ======================================================================

        // Viser prompt for neste øvelse og venter på tastetrykk
        public static void WaitForNext(string nextLabel, bool addLeadingBlank = false)
        {
            if (addLeadingBlank) Console.WriteLine();             // Valgfri ekstra blank linje før
            Console.WriteLine("~~~");                             // Liten markørlinje
            Console.WriteLine($"\nTrykk en tast for å fortsette til {nextLabel}..."); // Prompt-tekst: venter på en tast blir trykket på.
            Console.ReadKey(intercept: true);                     // Vent på tast uten å eko
            Console.WriteLine();                                  // Blank linje etter tastetrykk
        }

        // ================================================================
        //    ------------------- PAUSE BEFORE MENU ------------------------
        // ================================================================
        // Gir brukeren tid til å lese resultat før menyen vises på nytt.
        public static void PauseBeforeMenu(string userChoice, bool running)
        {
            // Ikke pause hvis programmet avsluttes.
            if (!running) return;

            // Case 10 har allerede egen "Press any key..." i ShowDataOverview().
            if (userChoice == "10")
            {
                Console.Clear();
                return;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(intercept: true);
            Console.Clear();
        }

        // ================================================================
        //    ------------------- RETRY CURRENT ACTION ---------------------
        // ================================================================
        // Lar bruker velge om samme menyvalg skal kjøres på nytt.
        public static bool AskToRetryCurrentAction()
        {
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
